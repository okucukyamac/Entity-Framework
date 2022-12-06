
using Microsoft.EntityFrameworkCore;

#region Relationships(İlişkiler) Terimleri
#region Pricipal Entity (Asıl Entity)

//Kendi başına var olabilen tabloyu modelleyen entity'e denir.
//CalisanEntity(Id,CalisanAdi,DepartmanId)
//DepartmanEntity(Id,DepartmanAdi)
//Departmanlar tablosunu modelleyen 'Departman' entity'sidir.

#endregion

#region Dependent Entity (Bağımlı Entity)

//Kendi başına var olamayan, bir başka tabloya (ilişkisel olarak bağımlı) olan tabloyu modelleyen entity'e denir.
//CalisanEntity(Id,CalisanAdi,DepartmanId)
//DepartmanEntity(Id,DepartmanAdi)
//Calisanlar tablosunu modelleyen 'Calisan' entity'sidir.

#endregion

#region Foreign Key

//Principal Entity ile Dependent Entity arasındaki ilişkiyi sağlayan key’dir.
//CalisanEntity(Id,CalisanAdi,DepartmanId)
//DepartmanEntity(Id,DepartmanAdi)
//CalisanEntity elemanı olan DepartmanId Foreign Key dir.
//Dependent Entity'de tanımlanır.
//Karşılık olarak Principal Entity deki Principal Key'i tutar.

#endregion

#region Principal Key

//Principal Entity'deki Id'nin kendisidir.
//Principal Entity'nin kimliği olan kolonu ifade eden propertydir.
//CalisanEntity(Id,CalisanAdi,DepartmanId)
//DepartmanEntity(Id,DepartmanAdi)
//DepartmanEntity elemanı olan Id, Pricipal Key dir.

#endregion

#endregion

#region Navigation Property Nedir?

//İlişkisel tablolar arasındaki fiziksel erişimi entity class'ları üzerinden sağlayan property'lerdir.
//Bir property'nin Navigation Property olabilmesi için kesinlikle entity türünden olması gerekiyor.
//Navigation Propertyler entitylerdeki tanımlarına göre n'e n ya da 1'e n şeklinde ilişti türlerini ifade etmektedir. İlişkisel yapılar tam teferruatlı pratikte incelerken navigation property'lerin bu özelliklerinden istifade edilgiği görülecektir

#endregion

#region EF Core'da İlişki Yapılandırma Yöntemleri
#region Default Convventions
//Varsayılan Entity kurallarını kullanarak yapılan ilişki yapılandırma yöntemleridir.
//Navigation property’leri kullanarak ilişki şablonlarını çıkarmaktadır.
#endregion

#region Data Annotatons Attributes
//Entity'nin niteliklerine göre ince ayarlar yapmamızı sağlayan attribute'lardır.
//[Key],[ForeignKey]
#endregion

#region Fluent API
//Entity modellerindeki ilişkileri yapılandırırken daha detaylı çalışmamızı saplayan yöntemdir.

#region HasOne
//İlgili entity'nin ilişkisel entity'ye birebir ya da bire çok olacak şekilde ilişkisini yapılandırmaya başlayan metottur.
#endregion

#region HasMany
//İlgili entity'nin ilişkisel entity'e çoka bir ya da çoka çok olacak şekilde ilişkisini yapılandırmaya başlayan metottur.
#endregion

#region WithOne
//HasOne ya da HasMany'den sonra bire bir ya da çoka bir olacak şekilde ilişki yapılandırmasını tamamlayan metottur.
#endregion

#region WithMany
//HasOne ya da HasMany'den sonra bire çok ya da çoka çok olacak şekilde ilişki yapılandırmasını tamamlayan metottur.

#endregion

#endregion
#endregion

Console.WriteLine("denemedir");

class Calisan
{
    public int Id { get; set; }
    public string CalisanAdi { get; set; }
    public int DepartmanId { get; set; }
    public Departman departman{ get; set; }
}

class Departman
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }
    public ICollection<Calisan> Calisanlar { get; set; }
}





