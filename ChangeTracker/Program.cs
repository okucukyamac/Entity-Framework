


using ChangeTracker;
using Microsoft.EntityFrameworkCore;

ChangeTrackerContext context = new();

#region Change Tracking Nedir?

//Context nesnesi ğzerinden gelen tüm nesneler/veriler otomatik olarak bir takip mekanizması tarafından izlenirler. İşte bu takip mekanizmasına Change Tracker denir. Change Tracker ile nesneler üzerindeki değişiklikler / işlemler takip edilerek netice itibariyle bu işlemlerin fıtratına uygun sql sorguları generate edilir. İşte bu işleme de Change Tracking denir. 

#endregion

#region ChangeTracker Propertysi

//Takip edilen nesnelere erişebilmemizi ve gerektiğinde işlemler yapmamızı sağlayan propertydir.

//Context sınıfının base class’ı olan DbContext sınıfının bir member’ıdır.

//var urunler = await context.Urunlers.ToListAsync();

//urunler[6].Fiyat = 19;//update
//context.Urunlers.Remove(urunler[7]);//delete
//urunler[8].UrunAdi = "asdadas";//update


//var datas = context.ChangeTracker.Entries();


#region DetectChanges Property'si

//EF Core, context nesnesi tarafından izlenen tüm nesnelerdeki değiklikleri Change Tracker sayesinde takip edebilmekte ve nesnelerde olan verisel değişiklikler yakalanarak bunların anlık görüntülerini oluşturabilir.
//Yapılan değişikliklerin veritabanına gönderilmeden önce algılandığından emin olmak gerekir. SaveChanges fonksiyonu çağrıldığı anda nesneler EF Core tarafından otomatik kontrol edilirler.
//Ancak, yapılan operasyonlarda güncel tracking verilerinden emin olabilmek için değişikliklerin algolanmasını opsiyonel olarak gerçekleştirmek isteyebiliriz. İşte bunun için DetectChanges fonksiyonu kullanılabilir ve her ne kadar EF Core değişiklikleri otomatik algılıyor olsa da siz yine de iradenizle kontole zorlayabilirsiniz.

//var urun = await context.Urunlers.FirstOrDefaultAsync(u => u.Id == 3);
//urun.Fiyat = 123;

//context.ChangeTracker.DetectChanges();
//await context.SaveChangesAsync();

#endregion

#region AutoDetectChangesEnabled Property'si

//İlgili metotlar (SaveChanges, Entries) tarafından DetectChanges otomatik olarak tetiklenmesinin konfigürasyonunu yapmamızı sağlayan propertydir.
//SaveChanges fonksiyonu tetikklendiğinde DetectChanges metodunu içerisinde default olrak çağırmaktadır. Bu durumda DetectChanges fonksiyonunun kullanımını irademizle yönetmek ve maliyet / persormans optimizasyonu yapmak istediğimiz durumlarda AutoDetectChangesEnabled özelliğini kapatabiliriz.

#endregion

#region Entries Metodu

//Context'te ki Entry metodunun koleksiyonel versiyonudur. 
//Change Tracker mekanizması tarafından izlenen her entity nesnesinin bilgisini EntityEntry  türünden elde etmemizi sağlar ve belirli işlemler yapabilmemize olanak tanır.
//Entries metodu, DetechtChanges metodunu tetikler. Bu durumda tıpkı SaveChanges'da olduğu gibi bir maliyettir. Buradaki maliyetten kaçınmak için AutoDetectChangesEnabled özelliğine false değeri verilebilir.

//var urunler = await context.Urunlers.ToListAsync();

//urunler.FirstOrDefault(u => u.Id == 7).Fiyat = 123;
//urunler.FirstOrDefault(u => u.Id == 9).UrunAdi = "ldskjfa";
//context.Urunlers.Remove(urunler.FirstOrDefault(urunler=> urunler.Id ==8));

//context.ChangeTracker.Entries().ToList().ForEach(entry =>
//{
//	if (entry.State == EntityState.Unchanged)
//	{

//	}
//	else if (entry.State == EntityState.Deleted)
//	{

//	}
//});

#endregion

#region AcceptAllChanges Metodu

//SaveChanges() veya SaveChanges(true) tetiklendiğinde EFCore herşeyin yolunda olduğunu varsayarak track ettiği verilerin takibini kerser yeni değişikliklerin takip edilmesini bekler. Böyle bir durumda beklenmeyen bir durum olabsı bir hata söz konusu olursa eğer EF Core takip etiiği nesneleri bırakacağı için bir düzeltme mevzu bahis olamayacaktır.

//Haliyle bu durumda devreye SaveChanges(false) ve AcceptAllChanges metotları girecektir.

//SaveChanges(false), EF Core'a gerekli veritabanı konutlarını yürütmesni söyler ancak gerektiğinde yeniden oynatılabilmesi için değişiklikleri beklemeye / nesneleri takip etmeye devam eder. Taa ki AcceptAllChanges metodunu irademizle çağırana kadar!

//SaveChanges(false) ile işlemin başarılı olduğundan emin olursanız AcceptAllChanges metodu ile nesnelerden takibi kesebilirsiniz.

//var urunler = await context.Urunlers.ToListAsync();

//urunler.FirstOrDefault(u => u.Id == 7).Fiyat = 123;
//urunler.FirstOrDefault(u => u.Id == 9).UrunAdi = "ldskjfa";
//context.Urunlers.Remove(urunler.FirstOrDefault(urunler=> urunler.Id ==167));

//await context.SaveChangesAsync();

//var daas = context.ChangeTracker.Entries();

//context.ChangeTracker.AcceptAllChanges();

#endregion

#region HasChanges Metodu

//Takip Edilen nesneler arasından değişiklik yapılanların olup olmadığının bilgisini verir.
//Arkaplanda DetectChanges metodunu tetikler.

//var result = context.ChangeTracker.HasChanges();

#endregion

#endregion

#region Entity States

#region Detached

//Nesnenin Change tracker mekanizması tarafından takip edilmediğini ifade eder.

//Urunler urun = new();

//Console.WriteLine(context.Entry(urun).State);//Detached

#endregion

#region Added

//Veritabanına eklenecek nesneyi ifade eder. Added henüz veritabanına işlenmeyen veriyi ifade eder. SaveChanges fonksiyonu çağrıldığında insert sorgusu oluşturulacağı anlamına gelir.

//Urunler urun = new() { Fiyat = 123, UrunAdi = "Ürün 1001" };
//Console.WriteLine(context.Entry(urun).State);//Detached
//await context.Urunlers.AddAsync(urun);
//Console.WriteLine(context.Entry(urun).State);//Added
//await context.SaveChangesAsync();

#endregion

#region UnChanged

//Veritabanından sorgulandığından beri nesne üzerinde herhangi bir değişiklik yapılmadığını ifade eder. Sorgu neticesinde elde edilen tüm nesneler başlangıçta bu state değerindedir.

//var data = context.Urunlers.FirstOrDefault(u => u.Id == 232);
//Console.WriteLine(context.Entry(data).State);//Unchanged

#endregion

#region Modified

//Nesne üzerinde değişiklik yani güncelleme yapıldığını ifade eder. SaveChanges fonksiyonu çağrıldığında update sorgusu oluşturulacağı anlamına gelir.

//var data = context.Urunlers.FirstOrDefault(u => u.Id == 232);
//data.UrunAdi = "deneme";
//Console.WriteLine(context.Entry(data).State);//Modified

#endregion

#region Deleted

//Nesnenin silindğini ifade eder. SaveChanges fonksiyonu çağıldığında delete sorgusu oluşturulacağı anlamına gelir.

//var data = context.Urunlers.FirstOrDefault(u => u.Id == 232);
//context.Urunlers.Remove(data);
//Console.WriteLine(context.Entry(data).State);//Deleted

#endregion

#endregion

#region Context Nesnesi Üzerinde Change Tracker

//var urun = await context.Urunlers.FirstOrDefaultAsync(u => u.Id == 55);
//urun.Fiyat = 123;
//urun.UrunAdi = "silgi";

#region Entry Medodu
#region OriginalValues Propery'si

//var fiyat = context.Entry(urun).OriginalValues.GetValue<float>(nameof(Urunler.Fiyat));
//var urunADi = context.Entry(urun).OriginalValues.GetValue<string>(nameof(Urunler.UrunAdi));

#endregion

#region CurrentValues Property'si
//silgi
//var urunAdi = context.Entry(urun).CurrentValues.GetValue<string>(nameof(urun.UrunAdi));

#endregion

#region GetDatabaseValues Metodu

//var _urun = await context.Entry(urun).GetDatabaseValuesAsync();


#endregion

#endregion
#endregion

#region Change Tracker'ın Interceptor Olarak Kullanılması



#endregion

Console.WriteLine("Hello, World!");
Console.WriteLine("Hello, World!");


//var urunler = await context.Urunlers.ToListAsync();

//urunler.FirstOrDefault(u => u.Id == 7).Fiyat = 123;
//urunler.FirstOrDefault(u => u.Id == 9).UrunAdi = "ldskjfa";
//context.Urunlers.Remove(urunler.FirstOrDefault(urunler=> urunler.Id ==8));