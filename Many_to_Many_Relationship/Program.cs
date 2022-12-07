using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

Console.WriteLine("Hello, World!");

#region Default Convention

//İki entity arasındaki ilişkiyi navigation propertyler üzerinden çoğul olarak kurmalıyız. (ICollection, List)

//Default Convention'da cross table'ı manuel oluşturmak zorunda değiliz. EF Cor tasarımı uygun bir şekilde cross table'ı kendisi otomatik basacak ve generate edecektir.
//Ve oluşturulan cross table'ın içerisinde compasite primary key'i de otomatik oluşturmuş olacaktır.
//class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }
//    public ICollection<Yazar> Yazarlar { get; set; }
//}

//class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    public ICollection<Kitap> Kitaplar { get; set; }
//}

#endregion

#region Data Annotations
//Cross table manuel olarak oluşturulmak zorundadır.
//Entity'leri oluşturduğumuz cross table entity si ile bire çok bir ilişki kurulmalı.
//CT'da compasite primary key'i data annotation(Attributes)lar ile manuel kuramıyoruz. Bunun için de Fluent API'da çalışma paymamız gerekiyor.
//Cross Table'a karşılık bir entity modeli oluşturuyorsak eğer bunu context sınıfı içerisinde DbSet property'si olarak bildirmek mecburiyetinde değiliz.
//class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }
//    public ICollection<KitapYazar> Yazarlar { get; set; }

//}
//class KitapYazar
//{
//    [Key]
//    public int KitapId { get; set; }
//    [Key]
//    public int YazarId { get; set; }
//    public Kitap Kitap { get; set; }
//    public Yazar Yazar { get; set; }
//}

//class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    public ICollection<KitapYazar> Kitaplar { get; set; }

//}
#endregion

#region Fluent API
//Cross Table manuel oluşturulmalı.
//DbSet olarak eklenmesine gerek yok.
//Composite PK Haskey metodu ile kurulmalı.
class Kitap
{
    public int Id { get; set; }
    public string KitapAdi { get; set; }
    public ICollection<KitapYazar> Yazarlar { get; set; }

}

class KitapYazar
{
    public int KitapId { get; set; }
    public int YazarId { get; set; }
    public Kitap Kitap { get; set; }
    public Yazar Yazar { get; set; }
}

class Yazar
{
    public int Id { get; set; }
    public string YazarAdi { get; set; }
    public ICollection<KitapYazar> Kitaplar { get; set; }

}
#endregion



class EKitapDbContext : DbContext
{
    public DbSet<Kitap> Kitaplar { get; set; }
    public DbSet<Yazar> Yazarlar { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=OKUCUKYAMAC\\SQLEXPRESS;database=EfKitapDb;integrated security=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KitapYazar>()
            .HasKey(ky => new { ky.KitapId, ky.YazarId });

        modelBuilder.Entity<KitapYazar>()
           .HasOne(ky => ky.Kitap)
           .WithMany(k => k.Yazarlar)
           .HasForeignKey(ky=>ky.KitapId);

        modelBuilder.Entity<KitapYazar>()
            .HasOne(ky=> ky.Yazar)
            .WithMany(y=>y.Kitaplar)
            .HasForeignKey(ky=>ky.YazarId);
    }


    //Data Annotations
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<KitapYazar>()
    //        .HasKey(ky => new { ky.KitapId, ky.YazarId });
    //}
}