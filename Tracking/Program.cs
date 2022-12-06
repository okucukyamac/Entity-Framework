



using Microsoft.EntityFrameworkCore;
TrackingContext context= new TrackingContext();

#region AsNoTracking Metodu

//Context üzerinden gelen tüm datalar Change Tracker mekanizması tarafından takip edilmektedir.
//Change Tracker, takipp ettiği nesnelerin sayısıyla doğru orantılı olacak şekilde bir maliyete sahiptir. O yüzden üzerinden işlem yapılmayacak verilerin takip edilmesi bizlere lüzumsuz yere bir maliyet ortaya çıkaracaktır.

//AsNoTracking metodu, context üzerinden sorgu neticesinde gelecek olan verilerin ChangeTracker tarafından takip edilmesini engeller.

//AsNoTracking metodu ile Change Tracker'ın ihtiyaç olmayan verilerdeki maliyetini törpülemiş oluruz.

//AsNoTracking fonksiyonu ile yapılan sorgulamalarda, verileri elde edebilir, bu verileri istenilen noktalarda kullanabilir lakin veriler üzerinde herhangi bir değişiklik işlemi yapamayız

//var kullanicilar = await context.Kullanicilar.AsNoTracking().ToListAsync();
//foreach (var item in kullanicilar)
//{
//    Console.WriteLine();
//}

#endregion

#region  AsNoTrackingWithIdentityResolution

//ChangeTracker mekanizması sayesinde yinelenen veriler aynı instanceleri kullanırlar. 
//AsNoTracking metodu ile yapılan sorgularda yinelenen datalar farkli instancalarda karşılanırlar ve maliyet artar. Özellikle ilişkisel sorgularda bu durumla karşılaşılmaktadır.
//CT mekanizması yinelenen verileri tekil instance olarak getirir. Buradan eksradan bir performans kazancı söz konusudr.
//Bizler yaptığımız sorgularda takip mekanizmasının AsNoTracking metodu ile bazen maliyete sebebiyet verebiliriz. (Özellikle ilişkisel tablolar sorgularken buduruma dikkat etmemiz gerekiyor).
//AsNoTracking ile elde edilen veriler takip edilmeyeceğinden dolayı yinelenen verilerin ayrı instancelarda olmasına sebebiyet veriyoruz. Çünkü CT mekanizması takip ettiği nesneden bellekte varsa eğer aynı nesneden bir daha oluşturma gereği duymaksızın o nesneye ayrı noktalardaki ihtiyacı aynı instance üzerinden gidermektedir.

//Böyle bir durumda hem takip mekanizmasının maliyetini düşürmek hem de yinelenen dataların tek bir instance üzerinde karşılamak için AsNoTrackingWithIdentityResolution fonksiyonunu kullanabiliriz.

//var kitaplar = await context.Kitaplar.Include(k => k.Yazarlar).AsNoTrackingWithIdentityResolution().ToListAsync();

//AsNoTrackingWithIdentityResolution fonksiyonu AsNoTracking fonksiyonuna nazaran görece yavaştır/maliyetlidir lakin CT'a nazaran daha performanslı ve az ve maliyetlidir. 

#endregion

#region AsTracking

//ChangeTrackerin default halinin iradeli halidir. Oluşturduğun sorgunun takip edilmesini istiyorsanız AsTracking kullanılabilir.
//Context üzerinden gelen datalaraın CT tarafından takip edilmesini iradeli bir şekilde ifade etmemizi sağlayan fonksiyondur.
//UseQueryTrackingBehavior metodunun davranışı gereği uygulama seviyesinde CT'ın default olarak devrede olup olmamasını ayarlyor olacağır. Eğer ki default olarak pasif hale getirilirse böyle durumda takip mekanizmasının ihtiyaç olduğu sorgularda AsTracking fonksiyonunu kullanabilir ve böylece takip mekanizmasını iradeli bir şekilde devreye sokmuş oluruz.

//var kullanicilar = await context.Kullanicilar.Include(k => k.Roller).AsTracking().ToListAsync();

#endregion

#region UseQueryTrackingBehavior
//Ef Core seviyesinde / uygulama seviyesinde ilgili contextten gelen verilerin üzerinde CT mekanizmasının davranışı temel seviyede belirlememizi sağlayan fonksiyondur. Yani konfigürasyon fonksiyonudur.

//Context sınıfı OnConfiguring üzerinden yapılıyor.
#endregion

Console.WriteLine("Hello, World!");



public class TrackingContext : DbContext
{
    public DbSet<Kullanici> Kullanicilar { get; set; }
    public DbSet<Rol> Roller { get; set; }
    public DbSet<Yazar> Yazarlar { get; set; }
    public DbSet<Kitap> Kitaplar { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=OKUCUKYAMAC\\SQLEXPRESS;database=EfCoreQueryingDb;integrated security=true;");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }


}


public class Kullanici
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Rol> Roller { get; set; }
}

public class Rol
{
    public int Id { get; set; }
    public string RolAdi { get; set; }
    public ICollection<Kullanici> Kullanicilar { get; set; }
}

public class Yazar
{
    public Yazar() => Console.WriteLine("Yazar nesnesi oluşturuldu.");

    public int Id { get; set; }
    public string YazarAdi { get; set; }
    public ICollection<Kitap> Kitaplar { get; set; }
}

public class Kitap
{
    public Kitap() => Console.WriteLine("Kitap nesnesi oluşturuldu.");
    public int Id { get; set; }
    public string KitapAdi { get; set; }
    public int SayfaSayisi { get; set; }
    public ICollection<Yazar> Yazarlar { get; set; }
}