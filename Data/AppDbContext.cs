using Microsoft.EntityFrameworkCore;
using WebSocket_API.Entities;

namespace WebSocket_API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }
        public DbSet<PriceInfo> PriceInfos { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<InstrumentMapping> InstrumentMappings { get; set; }
        public DbSet<InstrumentProfile> InstrumentProfiles { get; set; }
        public DbSet<TradingHours> TradingHours { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<ProviderExchange> ProviderExchanges { get; set; }
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<GicsClassification> GicsClassifications { get; set; }
        public DbSet<GicsItem> GicsItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>()
                .HasOne(a => a.LatestPrice)
                .WithOne()
                .HasForeignKey<Asset>(a => a.LatestPriceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PriceInfo>()
                .HasOne(p => p.Asset)
                .WithMany(a => a.PriceHistory)
                .HasForeignKey(p => p.AssetId)
                .OnDelete(DeleteBehavior.Restrict);


            // Instrument → Asset
            modelBuilder.Entity<Asset>()
                .HasOne(a => a.Instrument)
                .WithMany()
                .HasForeignKey(a => a.InstrumentId);

            modelBuilder.Entity<Asset>()
                .HasOne(a => a.Provider)
                .WithMany()
                .HasForeignKey(a => a.ProviderId);


            // Instrument → Kind
            modelBuilder.Entity<Instrument>()
                .HasOne(i => i.Kind)
                .WithMany()
                .HasForeignKey(i => i.KindId)
                .OnDelete(DeleteBehavior.SetNull);

            // Instrument → Exchange
            modelBuilder.Entity<Instrument>()
                .HasOne(i => i.Exchange)
                .WithMany()
                .HasForeignKey(i => i.ExchangeId)
                .OnDelete(DeleteBehavior.SetNull);

            // Instrument → InstrumentProfile
            modelBuilder.Entity<Instrument>()
                .HasOne(i => i.InstrumentProfile)
                .WithMany()
                .HasForeignKey(i => i.InstrumentProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // InstrumentProfile → GicsClassification
            modelBuilder.Entity<InstrumentProfile>()
                .HasOne(p => p.GicsClassification)
                .WithMany()
                .HasForeignKey(p => p.GicsClassificationId);

            // InstrumentMapping
            modelBuilder.Entity<InstrumentMapping>()
                .HasOne(m => m.Instrument)
                .WithMany(i => i.InstrumentMappings)
                .HasForeignKey(m => m.InstrumentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InstrumentMapping>()
                .HasOne(m => m.Provider)
                .WithMany(p => p.InstrumentMappings)
                .HasForeignKey(m => m.ProviderId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<InstrumentMapping>()
                .HasOne(m => m.Exchange)
                .WithMany(e => e.InstrumentMappings)
                .HasForeignKey(m => m.ExchangeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<InstrumentMapping>()
                .HasOne(m => m.TradingHours)
                .WithOne(th => th.InstrumentMapping)
                .HasForeignKey<TradingHours>(th => th.InstrumentMappingId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProviderExchange - composite key
            modelBuilder.Entity<ProviderExchange>()
                .HasKey(pe => new { pe.ProviderId, pe.ExchangeId });

            modelBuilder.Entity<ProviderExchange>()
                .HasOne(pe => pe.Provider)
                .WithMany(p => p.ProviderExchanges)
                .HasForeignKey(pe => pe.ProviderId);

            modelBuilder.Entity<ProviderExchange>()
                .HasOne(pe => pe.Exchange)
                .WithMany(e => e.ProviderExchanges)
                .HasForeignKey(pe => pe.ExchangeId);

            // GicsItem - tree structure
            modelBuilder.Entity<GicsItem>()
                .HasOne(i => i.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(i => i.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
