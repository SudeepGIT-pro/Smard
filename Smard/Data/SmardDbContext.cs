using Microsoft.EntityFrameworkCore;
using Smard.Models;

namespace Smard.Data
{
    public class SmardDbContext : DbContext
    {
        public SmardDbContext(DbContextOptions<SmardDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<DeleteRequest> DeleteRequests { get; set; }

        // 18 Masters and Cost Details
        public DbSet<WallPanelSystemMaster> WallPanelSystemMasters { get; set; }
        public DbSet<WallPanelSystemCostDetail> WallPanelSystemCostDetails { get; set; }

        public DbSet<CeilingSystemMaster> CeilingSystemMasters { get; set; }
        public DbSet<CeilingSystemCostDetail> CeilingSystemCostDetails { get; set; }

        public DbSet<PeripheralLightMaster> PeripheralLightMasters { get; set; }
        public DbSet<PeripheralLightCostDetail> PeripheralLightCostDetails { get; set; }

        public DbSet<LaminarFlowMaster> LaminarFlowMasters { get; set; }
        public DbSet<LaminarFlowCostDetail> LaminarFlowCostDetails { get; set; }

        public DbSet<AVSUMaster> AVSUMasters { get; set; }
        public DbSet<AVSUCostDetail> AVSUCostDetails { get; set; }

        public DbSet<DoorsMaster> DoorsMasters { get; set; }
        public DbSet<DoorsCostDetail> DoorsCostDetails { get; set; }

        public DbSet<StorageCabinetMaster> StorageCabinetMasters { get; set; }
        public DbSet<StorageCabinetCostDetail> StorageCabinetCostDetails { get; set; }

        public DbSet<SinkMaster> SinkMasters { get; set; }
        public DbSet<SinkCostDetail> SinkCostDetails { get; set; }

        public DbSet<WorkStationMaster> WorkStationMasters { get; set; }
        public DbSet<WorkStationCostDetail> WorkStationCostDetails { get; set; }

        public DbSet<BHUMaster> BHUMasters { get; set; }
        public DbSet<BHUCostDetail> BHUCostDetails { get; set; }

        public DbSet<ReturnAirSystemMaster> ReturnAirSystemMasters { get; set; }
        public DbSet<ReturnAirSystemCostDetail> ReturnAirSystemCostDetails { get; set; }

        public DbSet<WindowsMaster> WindowsMasters { get; set; }
        public DbSet<WindowsCostDetail> WindowsCostDetails { get; set; }

        public DbSet<CountersMaster> CountersMasters { get; set; }
        public DbSet<CountersCostDetail> CountersCostDetails { get; set; }

        public DbSet<LGSPanelSystemMaster> LGSPanelSystemMasters { get; set; }
        public DbSet<LGSPanelSystemCostDetail> LGSPanelSystemCostDetails { get; set; }

        public DbSet<FixedFurnituresMaster> FixedFurnituresMasters { get; set; }
        public DbSet<FixedFurnituresCostDetail> FixedFurnituresCostDetails { get; set; }

        public DbSet<LooseFurnituresMaster> LooseFurnituresMasters { get; set; }
        public DbSet<LooseFurnituresCostDetail> LooseFurnituresCostDetails { get; set; }

        public DbSet<CSSDFurnituresMaster> CSSDFurnituresMasters { get; set; }
        public DbSet<CSSDFurnituresCostDetail> CSSDFurnituresCostDetails { get; set; }

        public DbSet<JobWorksMaster> JobWorksMasters { get; set; }
        public DbSet<JobWorksCostDetail> JobWorksCostDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.ToTable("AspNetRoles");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).HasMaxLength(450);
                entity.Property(r => r.Name).HasMaxLength(256);
                entity.Property(r => r.NormalizedName).HasMaxLength(256);

                entity.HasData(
                    new AspNetRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = null },
                    new AspNetRole { Id = "2", Name = "Manager", NormalizedName = "MANAGER", ConcurrencyStamp = null },
                    new AspNetRole { Id = "3", Name = "User", NormalizedName = "USER", ConcurrencyStamp = null }
                );
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.ToTable("AspNetUserRoles");
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
                entity.Property(ur => ur.UserId).HasMaxLength(450);
                entity.Property(ur => ur.RoleId).HasMaxLength(450);

                entity.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(ur => ur.RoleId);

                entity.HasData(
                    new AspNetUserRole { UserId = "61336a0f-403b-4371-831e-8a843571f4db", RoleId = "1" },
                    new AspNetUserRole { UserId = "053a9fc0-9f36-4d46-8b1d-a5fec686348e", RoleId = "2" }
                );
            });

            modelBuilder.Entity<UserNotification>(entity =>
            {
                entity.ToTable("UserNotifications");
                entity.HasKey(n => n.Id);
                entity.Property(n => n.UserId).IsRequired();
                entity.Property(n => n.Title).IsRequired();
                entity.Property(n => n.Message).IsRequired();
                entity.Property(n => n.NotifType).IsRequired();
                entity.Property(n => n.IsRead).IsRequired();
                entity.Property(n => n.CreatedOn).HasColumnType("datetime2(7)");
                entity.Property(n => n.ActionUrl).IsRequired(false);
            });

            modelBuilder.Entity<DeleteRequest>(entity =>
            {
                entity.ToTable("DeleteRequests");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.RequestedByUserId).IsRequired();
                entity.Property(r => r.RequestedByUserName).IsRequired();
                entity.Property(r => r.MasterName).IsRequired();
                entity.Property(r => r.ControllerName).IsRequired();
                entity.Property(r => r.TypeName).IsRequired();
                entity.Property(r => r.Status).IsRequired().HasMaxLength(50);
                entity.Property(r => r.RequestedOn).HasColumnType("datetime2(7)");
                entity.Property(r => r.ReviewedOn).HasColumnType("datetime2(7)").IsRequired(false);
            });

            // Configure 1-to-many relationship with cascade delete for all 18 masters
            modelBuilder.Entity<WallPanelSystemCostDetail>()
                .HasOne(d => d.WallPanelSystemMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.WallPanelSystemMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CeilingSystemCostDetail>()
                .HasOne(d => d.CeilingSystemMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.CeilingSystemMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PeripheralLightCostDetail>()
                .HasOne(d => d.PeripheralLightMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.PeripheralLightMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LaminarFlowCostDetail>()
                .HasOne(d => d.LaminarFlowMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.LaminarFlowMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AVSUCostDetail>()
                .HasOne(d => d.AVSUMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.AVSUMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DoorsCostDetail>()
                .HasOne(d => d.DoorsMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.DoorsMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StorageCabinetCostDetail>()
                .HasOne(d => d.StorageCabinetMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.StorageCabinetMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SinkCostDetail>()
                .HasOne(d => d.SinkMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.SinkMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkStationCostDetail>()
                .HasOne(d => d.WorkStationMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.WorkStationMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BHUCostDetail>()
                .HasOne(d => d.BHUMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.BHUMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReturnAirSystemCostDetail>()
                .HasOne(d => d.ReturnAirSystemMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.ReturnAirSystemMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WindowsCostDetail>()
                .HasOne(d => d.WindowsMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.WindowsMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CountersCostDetail>()
                .HasOne(d => d.CountersMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.CountersMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LGSPanelSystemCostDetail>()
                .HasOne(d => d.LGSPanelSystemMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.LGSPanelSystemMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FixedFurnituresCostDetail>()
                .HasOne(d => d.FixedFurnituresMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.FixedFurnituresMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LooseFurnituresCostDetail>()
                .HasOne(d => d.LooseFurnituresMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.LooseFurnituresMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CSSDFurnituresCostDetail>()
                .HasOne(d => d.CSSDFurnituresMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.CSSDFurnituresMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobWorksCostDetail>()
                .HasOne(d => d.JobWorksMaster)
                .WithMany(m => m.CostDetails)
                .HasForeignKey(d => d.JobWorksMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure standard decimal precision for all decimal properties across entities
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                    {
                        property.SetColumnType("decimal(18,2)");
                    }
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;
            foreach (var entry in ChangeTracker.Entries<GenericMasterBase>())
            {
                if (entry.State == EntityState.Added)
                {
                    if (!entry.Entity.CreatedOn.HasValue)
                    {
                        entry.Entity.CreatedOn = now;
                    }
                    if (string.IsNullOrWhiteSpace(entry.Entity.CreatedBy))
                    {
                        entry.Entity.CreatedBy = "Admin";
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedOn = now;
                    // Preserve original creation details
                    entry.Property(m => m.CreatedBy).IsModified = false;
                    entry.Property(m => m.CreatedOn).IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
