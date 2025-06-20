using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

namespace PaymentAPI.Data
{
    public class PaymentAppDbContext:DbContext
    {
        public PaymentAppDbContext(DbContextOptions<PaymentAppDbContext> options) : base(options) { }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.CardNumber).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Balance).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.TransactionId).IsRequired();
                entity.Property(p => p.RefundCode).IsRequired();
                entity.Property(p => p.Amount).HasColumnType("decimal(18,2)");
                entity.Property(p => p.Status).IsRequired().HasMaxLength(20);

                entity.HasOne(p => p.Card)
                      .WithMany()
                      .HasForeignKey(p => p.CardId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

