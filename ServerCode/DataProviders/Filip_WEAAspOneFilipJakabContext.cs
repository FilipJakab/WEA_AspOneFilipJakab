using DataProviders.Models;
using Microsoft.EntityFrameworkCore;

namespace DataProviders
{
	public partial class Filip_WEAAspOneFilipJakabContext : DbContext
	{
		public Filip_WEAAspOneFilipJakabContext(DbContextOptions options) : base(options) { }

		public virtual DbSet<Place> Place { get; set; }
		public virtual DbSet<Roles> Roles { get; set; }
		public virtual DbSet<Tag> Tag { get; set; }
		public virtual DbSet<Transaction> Transaction { get; set; }
		public virtual DbSet<TransactionCategory> TransactionCategory { get; set; }
		public virtual DbSet<TransactionTags> TransactionTags { get; set; }
		public virtual DbSet<User> User { get; set; }

		// Unable to generate entity type for table 'dbo.UserRoles'. Please see the warning messages.

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Roles>(entity =>
			{
				entity.HasKey(e => e.RoleId);

				entity.Property(e => e.Name)
						.IsRequired()
						.HasMaxLength(50);
			});

			modelBuilder.Entity<Tag>(entity =>
			{
				entity.Property(e => e.Name)
						.IsRequired()
						.HasMaxLength(50);

				entity.HasOne(d => d.User)
						.WithMany(p => p.Tag)
						.HasForeignKey(d => d.UserId)
						.HasConstraintName("FK_Tag_User");
			});

			modelBuilder.Entity<Transaction>(entity =>
			{
				entity.HasKey(e => e.TransactionCode);

				entity.Property(e => e.TransactionCode).HasDefaultValueSql("(newid())");

				entity.Property(e => e.Amount).HasColumnType("money");

				entity.Property(e => e.Date).HasColumnType("date");

				entity.Property(e => e.Description).HasColumnType("text");

				entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

				entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");

				entity.Property(e => e.Name)
						.IsRequired()
						.HasMaxLength(50);

				entity.HasOne(d => d.Category)
						.WithMany(p => p.Transaction)
						.HasForeignKey(d => d.CategoryId)
						.OnDelete(DeleteBehavior.Cascade)
						.HasConstraintName("FK_Transaction_TransactionCategory1");

				entity.HasOne(d => d.User)
						.WithMany(p => p.Transaction)
						.HasForeignKey(d => d.UserId)
						.HasConstraintName("FK_Transaction_User");
			});

			modelBuilder.Entity<TransactionCategory>(entity =>
			{
				entity.HasKey(e => e.CategoryId);

				entity.ToTable("TransactionCategory", "CONST");

				entity.Property(e => e.Name)
						.IsRequired()
						.HasMaxLength(50);
			});

			modelBuilder.Entity<TransactionTags>(entity =>
			{
				entity.HasOne(d => d.Tag)
						.WithMany(p => p.TransactionTags)
						.HasForeignKey(d => d.TagId)
						.OnDelete(DeleteBehavior.ClientSetNull)
						.HasConstraintName("FK_TransactionTags_Tag");

				entity.HasOne(d => d.TransactionCodeNavigation)
						.WithMany(p => p.TransactionTags)
						.HasForeignKey(d => d.TransactionCode)
						.HasConstraintName("FK_TransactionTags_Transaction1");
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity.Property(e => e.Balance).HasColumnType("money");

				entity.Property(e => e.Birthdate).HasColumnType("date");

				entity.Property(e => e.Email)
						.IsRequired()
						.HasMaxLength(50);

				entity.Property(e => e.FirstName)
						.IsRequired()
						.HasMaxLength(50);

				entity.Property(e => e.LastName)
						.IsRequired()
						.HasMaxLength(50);

				entity.Property(e => e.Password)
						.IsRequired()
						.HasMaxLength(100);
			});
		}
	}
}
