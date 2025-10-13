using Microsoft.EntityFrameworkCore;
using ApiInescafe.Models;

namespace ApiInescafe.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Usuarios { get; set; }
    public DbSet<NewsletterSubscription> NewsletterSubscricoes { get; set; }
    public DbSet<NewsletterContact> Contatos { get; set; }
    public DbSet<Product> Produtos { get; set; }
    public DbSet<Course> Cursos { get; set; }
    public DbSet<CourseClass> AulasCursos { get; set; }
    public DbSet<CourseEnrolled> InscricoesCursos { get; set; }
    public DbSet<Review> Avaliacoes { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<BlogLikes> CurtidasBlog { get; set; }
    public DbSet<SignaturePlan> PlanosAssinatura { get; set; }
    public DbSet<UserSubscription> AssinaturasUsuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
    
            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.PasswordHash).IsRequired();
            });
    
            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            });
    
            // Course
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
    
                entity.HasMany(c => c.Classes)
                    .WithOne(cc => cc.Course)
                    .HasForeignKey(cc => cc.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
    
            // CourseClass
            modelBuilder.Entity<CourseClass>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
            });
    
            // Review
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(r => r.UserId).WithMany().HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(r => r.Product).WithMany(p => p.Reviews).HasForeignKey(r => r.ProductId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(r => r.Course).WithMany(c => c.Reviews).HasForeignKey(r => r.CourseId).OnDelete(DeleteBehavior.Cascade);
            });
    
            // Blog
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.HasOne(b => b.User).WithMany().HasForeignKey(b => b.UserId).OnDelete(DeleteBehavior.Restrict);
                entity.Ignore(b => b.IsLiked); // Ignorar propriedades calculadas
                entity.Ignore(b => b.LikesCount);
            });
    
            // BlogLikes (Many-to-Many: User <-> Blog)
            modelBuilder.Entity<BlogLikes>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(bl => new { bl.BlogId, bl.UserId }).IsUnique(); // Um usuário só pode curtir um post uma vez
                entity.HasOne(bl => bl.Blog).WithMany(b => b.Likes).HasForeignKey(bl => bl.BlogId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(bl => bl.User).WithMany().HasForeignKey(bl => bl.UserId).OnDelete(DeleteBehavior.Cascade);
            });
    
            // SignaturePlan
            modelBuilder.Entity<SignaturePlan>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            });
    
            // UserSubscription (Many-to-Many: User <-> SignaturePlan)
            modelBuilder.Entity<UserSubscription>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(us => us.User).WithMany().HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(us => us.SignaturePlan).WithMany().HasForeignKey(us => us.SignaturePlanId).OnDelete(DeleteBehavior.Restrict);
            });
    
            // CourseEnrolled (Many-to-Many: User <-> Course)
            modelBuilder.Entity<CourseEnrolled>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(ce => new { ce.CourseId, ce.UserId }).IsUnique(); // Um usuário só pode se inscrever uma vez em um curso
                entity.HasOne(ce => ce.User).WithMany().HasForeignKey(ce => ce.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ce => ce.Course).WithMany().HasForeignKey(ce => ce.CourseId).OnDelete(DeleteBehavior.Cascade);
            });
    
            // NewsletterSubscription
            modelBuilder.Entity<NewsletterSubscription>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(ns => ns.User).WithMany().HasForeignKey(ns => ns.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(ns => ns.UserId).IsUnique(); // Um usuário só pode se inscrever uma vez
            });

            // NewsletterContact
            modelBuilder.Entity<NewsletterContact>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Topic).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(2000);
            });
        }
}