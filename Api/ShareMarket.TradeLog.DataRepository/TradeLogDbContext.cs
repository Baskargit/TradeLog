using System;
using Microsoft.EntityFrameworkCore;
using ShareMarket.TradeLog.DataEntities;

namespace ShareMarket.TradeLog.DataRepository
{
    public class TradeLogDbContext : DbContext
    {
        public TradeLogDbContext()
        {
        }

        public TradeLogDbContext(DbContextOptions<TradeLogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CloseTrade> CloseTrade { get; set; }
        public virtual DbSet<Market> Market { get; set; }
        public virtual DbSet<OpenTrade> OpenTrade { get; set; }
        public virtual DbSet<Symbol> Symbol { get; set; }
        public virtual DbSet<SymbolType> SymbolType { get; set; }
        public virtual DbSet<TradeResult> TradeResult { get; set; }
        public virtual DbSet<TradeStatus> TradeStatus { get; set; }
        public virtual DbSet<TradeType> TradeType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=TradeLog", x => x.ServerVersion("8.0.25-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CloseTrade>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.OpenTradeId, e.TradeTypeId, e.TradeResultId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity.HasIndex(e => e.OpenTradeId)
                    .HasName("fk_CloseTrade_OpenTrade1_idx");

                entity.HasIndex(e => e.TradeResultId)
                    .HasName("fk_CloseTrade_TradeResult1_idx");

                entity.HasIndex(e => e.TradeTypeId)
                    .HasName("fk_CloseTrade_TradeType1_idx");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.OpenTradeId).HasColumnName("OpenTrade_Id");

                entity.Property(e => e.TradeTypeId).HasColumnName("TradeType_Id");

                entity.Property(e => e.TradeResultId).HasColumnName("TradeResult_Id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(6,4)");

                entity.HasOne(d => d.OpenTrade)
                    .WithMany(p => p.CloseTrade)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.OpenTradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CloseTrade_OpenTrade1");

                entity.HasOne(d => d.TradeResult)
                    .WithMany(p => p.CloseTrade)
                    .HasForeignKey(d => d.TradeResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CloseTrade_TradeResult1");

                entity.HasOne(d => d.TradeType)
                    .WithMany(p => p.CloseTrade)
                    .HasForeignKey(d => d.TradeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CloseTrade_TradeType1");
            });

            modelBuilder.Entity<Market>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<OpenTrade>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.SymbolId, e.TradeTypeId, e.TradeStatusId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SymbolId)
                    .HasName("fk_OpenTrade_Symbol1_idx");

                entity.HasIndex(e => e.TradeStatusId)
                    .HasName("fk_OpenTrade_TradeStatus1_idx");

                entity.HasIndex(e => e.TradeTypeId)
                    .HasName("fk_OpenTrade_TradeType1_idx");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.SymbolId).HasColumnName("Symbol_Id");

                entity.Property(e => e.TradeTypeId).HasColumnName("TradeType_Id");

                entity.Property(e => e.TradeStatusId).HasColumnName("TradeStatus_Id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(6,4)");

                entity.HasOne(d => d.Symbol)
                    .WithMany(p => p.OpenTrade)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.SymbolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_OpenTrade_Symbol1");

                entity.HasOne(d => d.TradeStatus)
                    .WithMany(p => p.OpenTrade)
                    .HasForeignKey(d => d.TradeStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_OpenTrade_TradeStatus1");

                entity.HasOne(d => d.TradeType)
                    .WithMany(p => p.OpenTrade)
                    .HasForeignKey(d => d.TradeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_OpenTrade_TradeType1");
            });

            modelBuilder.Entity<Symbol>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.SymbolTypeId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SymbolTypeId)
                    .HasName("fk_Symbol_SymbolType1_idx");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.SymbolTypeId).HasColumnName("SymbolType_Id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Price).HasColumnType("decimal(6,4)");

                entity.HasOne(d => d.SymbolType)
                    .WithMany(p => p.Symbol)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.SymbolTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Symbol_SymbolType1");
            });

            modelBuilder.Entity<SymbolType>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.MarketId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.MarketId)
                    .HasName("fk_SymbolType_Market_idx");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.MarketId).HasColumnName("Market_Id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Market)
                    .WithMany(p => p.SymbolType)
                    .HasForeignKey(d => d.MarketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_SymbolType_Market");
            });

            modelBuilder.Entity<TradeResult>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TradeStatus>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TradeType>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

        }

    }
}
