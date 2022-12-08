using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("Hello, World!");



string text = "dene med ir ";
string text1 = text.Trim().Replace(" ", string.Empty);

Console.WriteLine(text1);

#region Default Convention

//HDefault convention yönteminde bire çok ilişkiyi kurarken foreign key kolonuna karşılık gelen bir property tanımlamak mecburiyetinde değiliz. Eğer tanımlamazsak EF Core bunu kendisi tamamlayacak yok eğer tanımlarsak, tanımladığımızı baz alacaktır.

//class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }
//    public Departman Departman { get; set; }
//}

//class Departman
//{
//    public int Id { get; set; }
//    public string DepartmanAdi { get; set; }
//    public ICollection<Calisan> Calisanlar { get; set; }
//}

#endregion



#region Data Annotations
//Navigation Propertyler tanımlanmalıdır
//Foreign kolonunun ismi default convention'un dışında bir kolon olacaksa eğer ForeignKey attribute ile bunu bildirebiliriz.
//Foreign Key kolonu oluşturulmak zorunda değildir.
//Default convention yönteminde foreign key kolonuna karşılık gelen property’i tanımladığımızda bu property ismi temel geleneksel entity tanımlama kurallrına uymuyorsa eğer Data Annotations’lar ile müdahalede bulunabiliriz.

//class Calisan
//{
//    public int Id { get; set; }
//    [ForeignKey(nameof(Departman))]
//    public int DId { get; set; }
//    public string Adi { get; set; }
//    public Departman Departman { get; set; }
//}

//class Departman
//{
//    public int Id { get; set; }
//    public string DepartmanAdi { get; set; }
//    public ICollection<Calisan> Calisanlar { get; set; }
//}

#endregion

#region Fluent API
//Model'lerin (entity) veritabanında generate edilecek yapıları bu fonksiyon içerisinde konfigure edilir.

//Navigation Propertyler tanımlanmalı
//Fluent API yönteminde entity'ler arasındaki ilişki context sınıfı içerisinde OnModelCreating fonksiyonunun override edilerek metotlar aracılığıyla tasarlanması gerekmektedir. Yani tüm sorumluluk bu fonksiyon içerisindeki çalışmalardadır.

//class Calisan
//{
//    public int Id { get; set; }
//    public int DId { get; set; }
//    public string Adi { get; set; }
//    public Departman Departman { get; set; }
//}

//class Departman
//{
//    public int Id { get; set; }
//    public string DepartmanAdi { get; set; }
//    public ICollection<Calisan> Calisanlar { get; set; }
//}

#endregion





//class ESirketDbContext : DbContext
//{
//    public DbSet<Calisan> Calisanlar { get; set; }
//    public DbSet<Departman> Departmanlar { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("server=OKUCUKYAMAC\\SQLEXPRESS;database=EfRelationshipDb;integrated security=true;");
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Calisan>()
//            .HasOne(c => c.Departman)
//            .WithMany(d => d.Calisanlar)
//            .HasForeignKey(c => c.DId);
//    }
//}