using Microsoft.EntityFrameworkCore;

QueryContext context = new QueryContext();

#region Sorgu Sonucu Dönüşüm Fonksiyonları

//Bu fonksiyonlar ile sorgu neticesinde elde edilen verileri isteğimiz doğrultusunda farklı türlerde projecsiyon edebiliyoruz.

#region ToDictionary

//Sorgu neticesinde gelecek olan veriyi bir dictionary olarak elde etmek/tutmak/karşılamak istiyorsak kullanılır.

//ToList ile aynı amaca hizmet etmektedir. Yani, oluşturulan sorguyu execute edip neticesini alırlar.

//ToList: Gelen sorgu neticesini entity türünde bir koleksiyone (List<Entity>) dönüştürmekteyken.

//ToDictionary ise gelen sorgu neticesini Dictionary türünden bir koleksiyona dönüştürecektir.

//var toDictionary = await context.Urunler.ToDictionaryAsync(u => u.UrunAdi, u => u.Fiyat);

#endregion

#region ToArray

//Oluşturulan sorguyu dizi olarak elde eder.

//ToList ile muadil amaca hizmet eder. Yani sorguyu execute eder Lakin gelen sonucu entity dizisi olarak elde eder.

//var toArray = await context.Urunler.ToArrayAsync();

#endregion

#region Select

//Select fonksiyonunun işlevsel olarak birden fazla davranışı söz konusudur.

//Select fonksiyonu generate edilecek sorgunun çekilecek kolonlarını ayarlamamızı sağlamaktadır.


//var select = await context.Urunler.Select(u => new Urun{Id = u.Id,Fiyat = u.Fiyat}).ToListAsync();

//Select fonksiyonu, gelen verileri farklı türlerde karşılamamızı sağlar.

//var select = await context.Urunler.Select(u => new
//{
//    Id=u.Id,
//    Fiyat=u.Fiyat
//}).ToListAsync();

#endregion

#region SelectMany

//Select ile aynı amaca hizmet eder. Lakin, ilişkisel tablolar neticesinde gelen koleksiyonel verileri de tekilleştirip projeksiyon etmemizi sağlar.

//var urunler = await context.Urunler.Include(u => u.Parcalar).SelectMany(u=>u.Parcalar,(u,p)=> new
//{
//    u.Id,
//    u.Fiyat,
//    u.ParcaAdi
//});

#endregion

#endregion

#region GroupBy Fonksiyonu

//Gruplama yapmamızı sağlayan fonksiyondur.

#region MethodSyntax

//var datas = await context.Urunler.GroupBy(x => x.Fiyat).Select(group => new
//{
//    Count = group.Count(),
//    Fiyat = group.Key
//}).ToListAsync();

#endregion

#region QuerySyntax

//var datas = await (from urun in context.Urunler
//                   group urun by urun.Fiyat
//            into @group
//                   select new
//                   {
//                       Fiyat = @group.Key,
//                       Count = @group.Count()
//                   }).ToListAsync();

#endregion


#endregion

#region Foreach Fonksiyonu

//Bir sorgulama fonksiyonu değildir.
//Sorgulama neticesinde elde edilen koleksiyonel veriler üzerinde iterasyonel olarak dönmemizi ve teker teker verileri elde edip işlemler yapabilmemizi sağlayan bir fonksiyondur. Foreach döngüsünün metot halidir!

//var datas = await context.Urunler.ToListAsync();

//foreach (var item in datas)
//{

//}

//datas.ForEach(x =>
//{

//});

#endregion


Console.WriteLine();

public class QueryContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=OKUCUKYAMAC\\SQLEXPRESS;database=EfCoreQueryingDb;integrated security=true;");
    }


}

public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }

}
