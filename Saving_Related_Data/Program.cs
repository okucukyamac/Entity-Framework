using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Metadata;

ApplicationDbContext context = new ApplicationDbContext();

Console.WriteLine("Hello, World!");

#region One to One İlişkisel Senaryolarda Veri Ekleme
#region 1. Yöntem - Principal Entity Üzerinden Dependent Entity Verisi Ekleme
//Person person = new();
//person.Name = "Gençay";
//person.Address = new() { PersonAddress = "Yenimahalle/Ankara" };

//await context.AddAsync(person);
//await context.SaveChangesAsync();

#endregion

//Eğer ki principal entity üzerinden ekleme gerçekleştiriliyorsa dependent entity nesnesi verilmek zorunda değildir! Fakat dependent entity üzerinden ekleme yapılıyorsa burada principal entity'nin nesnesine ihtiyacımız zaruridir.

#region 2. Yöntem - Dependent Entity Üzerinden Principal Entity Verisi Ekleme
//Address address = new()
//{
//    PersonAddress = "Sungurlu/Çorum",
//    Person = new() { Name = "Turgay" }
//};

//await context.AddAsync(address);
//await context.SaveChangesAsync();
#endregion

//class Person
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public Address Address { get; set; }
//}

//class Address
//{
//    public int Id { get; set; }
//    public string PersonAddress { get; set; }
//    public Person Person { get; set; }
//}

#endregion

#region One to Many İlişkisel Senaryolarda Veri Ekleme
#region 1. Yöntem - Principal Entity Üzerinden Dependent Entity Verisi Ekleme
#region Nesne Referansı Üzerinden Ekleme

//Blog blog = new() { Name = "evdeyokuz.com Blog" };
//blog.Posts.Add(new() { Title = "Post 1" });
//blog.Posts.Add(new() { Title = "Post 2" });
//blog.Posts.Add(new() { Title = "Post 3" });

//await context.AddRangeAsync(blog);
//await context.SaveChangesAsync();

#endregion
#region Object Initializer Üzerinden Ekleme

//Blog blog2= new Blog()
//{
//    Name="A Blog",
//    Posts = new HashSet<Post>() { new() { Title = "Post 4" }, new() { Title="Post 5"} }
//};

//await context.AddRangeAsync(blog2);
//await context.SaveChangesAsync();

#endregion
#endregion
#region 2. Yöntem - Dependent Entity Üzerinden Principal Entity Verisi Ekleme
//Post post = new()
//{
//    Title = "Post 6",
//    Blog = new() { Name = "B Blog" }
//};

#endregion
#region 3. Yöntem - Foreign Key Kolonu Üzerinden Veri Ekleme

//1. ve 2. yöntemler hiç olmayan verilerin ilişkisel olarak eklenmesini sağlarken, bu 3. yöntem önceden eklenmiş olan bir principal entity verisiyle yeni dependent entitylerin ilişkisel olarak eşleştirilmesini sağlamaktadır.

//Post post = new()
//{
//    BlogId = 2,
//    Title = "Test"
//};

#endregion

//class Blog
//{
//    public Blog()
//    {
//        Posts = new HashSet<Post>();
//    }
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Post> Posts { get; set; }
//}

//class Post
//{
//    public int Id { get; set; }
//    public int BlogId { get; set; }
//    public string Title { get; set; }
//    public Blog Blog { get; set; }
//}


#endregion

#region Many to Many İlişkisel Senaryolarda Veri Ekleme

#region 1. Yöntem
//n t n ilişkisi eğer ki default convention üzerinden tasarlanmışsa kullanılan bir yöntemdir.

//Book book = new()
//{
//    BookName = "A Kitabı",
//    Authors = new HashSet<Author>()
//    {
//        new (){AuthorName="Hilmi"},
//        new (){AuthorName="Ayşe"},
//        new (){AuthorName="Fatma"},

//    }
//};

#endregion
#region 2. Yöntem
//n t n ilişkisi eğer ki fluent api üzerinden tasarlanmışsa kullanılan bir yöntemdir.

Author author= new Author()
{
    AuthorName = "Mustafa",
    Books = new HashSet<BookAuthor>()
    {
        new(){BookId=1},
        new(){Book=new(){BookName = "B Kitap"} }

    }
};

#endregion

class Book
{
    public Book()
    {
        Authors = new HashSet<BookAuthor>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }
    public ICollection<BookAuthor> Authors { get; set; }
}

class BookAuthor
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }
    public Book Book { get; set; }
    public Author Author { get; set; }
}

class Author
{
    public Author()
    {
        Books = new HashSet<BookAuthor>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public ICollection<BookAuthor> Books { get; set; }
}

#endregion

class ApplicationDbContext : DbContext
{
    // One to One
    //public DbSet<Person> Persons  { get; set; }
    //public DbSet<Address> Addresses { get; set; }

    // One to Many
    //public DbSet<Post> Posts { get; set; }
    //public DbSet<Blog> Blogs { get; set; }

    // Many to Many
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=OKUCUKYAMAC\\SQLEXPRESS;database=EfRelationshipDb;integrated security=true;");
    }

    // Many to Many
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.AuthorId, ba.BookId });

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.Authors)
            .HasForeignKey(ba => ba.BookId);
        
        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(b => b.Books)
            .HasForeignKey(ba => ba.AuthorId);

    }

    // One to One
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Address>()
    //        .HasOne(a => a.Person)
    //        .WithOne(a => a.Address)
    //        .HasForeignKey<Address>(a => a.Id);
    //}


}