using Microsoft.EntityFrameworkCore;
using ApiInescafe.Models;

namespace ApiInescafe.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<blogLikesModel>()
        .HasOne(like => like.User)
        .WithMany()
        .HasForeignKey(like => like.UserId)
        .OnDelete(DeleteBehavior.NoAction); 


    modelBuilder.Entity<blogLikesModel>()
        .HasOne(like => like.Blog)
        .WithMany(post => post.Likes)
        .HasForeignKey(like => like.BlogId)
        .OnDelete(DeleteBehavior.Cascade);

}

    public DbSet<BlogModel> BlogPosts { get; set; } = null!;
    public DbSet<blogLikesModel> BlogLikes { get; set; } = null!;
    public DbSet<CourseModel> Courses { get; set; } = null!;
    public DbSet<CourseClassModel> CourseClasses { get; set; } = null!;
    public DbSet<CourseEnrolledModel> CourseEnrolleds { get; set; } = null!;
    public DbSet<ProductModel> Products { get; set; } = null!;
    public DbSet<ReviewModel> Reviews { get; set; } = null!;
    public DbSet<SignaturePlanModel> SignaturePlans { get; set; }
    public DbSet<SignaturePlanMembersModel> SignaturePlanMembers { get; set; } = null!;
    public DbSet<UserModel> Users { get; set; } = null!;
}