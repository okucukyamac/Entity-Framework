using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("Hello, World!");

#region Default Convention

//Her iki entity'de navigation property ile birbirlerini tekil olarak referans ederek fiziksel bir ilişkinin olacağı ifade edilir.
//One to One ilişki türünde dependent entity'nin hangisi olduğunu default olarak belirleyebilmek pek kolay değildir. Bu durumda fiziksel olarak bir foreign key'e karşılık property/kolon tanımlayarak çözüm getirebiliyoruz.
//Böylece foreign key'e karşılık property tanımlayarak lüzumsuz bir kolon oluşturmuş oluyoruz.

//class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }
//    public CalisanAdresi CalisanAdresi { get; set; }
//}

//class CalisanAdresi
//{
//    public int Id { get; set; }
//    public int CalisanId { get; set; }
//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; }
//}

#endregion

#region Data Annotations
//Navigation Propertyler tanımlanmalıdır
//Foreign kolonunun ismi default convention'un dışında bir kolon olacaksa eğer ForeignKey attribute ile bunu bildirebiliriz.
//Foreign Key kolonu oluşturulmak zorunda değildir.
//1'e 1 ilişkide ekstradan foreign key kolonuna ihtiyaç olmayağından dolayı dependent entity'deki id kolonunun hem foreign key hem de primary key olarak kullanmayı tercih ediyoruz ve bu duruma özen gösterilmelidir diyoruz.
//class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }
//    public CalisanAdresi CalisanAdresi { get; set; }
//}

//class CalisanAdresi
//{
//    [Key,ForeignKey(nameof(Calisan))]
//    public int Id { get; set; }
//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; }
//}

#endregion

#region Fluent API
//Model'lerin (entity) veritabanında generate edilecek yapıları bu fonksiyon içerisinde konfigure edilir.

//Navigation Propertyler tanımlanmalı
//Fluent API yönteminde entity'ler arasındaki ilişki context sınıfı içerisinde OnModelCreating fonksiyonunun override edilerek metotlar aracılığıyla tasarlanması gerekmektedir. Yani tüm sorumluluk bu fonksiyon içerisindeki çalışmalardadır.
class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public CalisanAdresi CalisanAdresi { get; set; }
}

class CalisanAdresi
{
    public int Id { get; set; }
    public string Adres { get; set; }
    public Calisan Calisan { get; set; }
}

#endregion


class ESirketDbContext :DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<CalisanAdresi> CalisanAdresleri { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=OKUCUKYAMAC\\SQLEXPRESS;database=EfRelationshipDb;integrated security=true;");
    }


    //Model'lerin (entity) veritabanında generate edilecek yapıları bu fonksiyon içerisinde konfigure edilir.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalisanAdresi>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.CalisanAdresi)
            .WithOne(c => c.Calisan)
            .HasForeignKey<CalisanAdresi>(c => c.Id);
    }
}