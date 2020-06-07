using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using TEP.Domain.Entities;
using TEP.Infra.Data.Mappings;

namespace TEP.Infra.Data.Contexto
{
    public class Context : DbContext
    {
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<TrainningSession> TrainningSessions { get; set; }

        public IDbContextTransaction Transaction { get; private set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            if (Database.GetPendingMigrations().Count() > 0)
                Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
        }

        public IDbContextTransaction InitTransacao()
        {
            if (Transaction == null) 
                Transaction = this.Database.BeginTransaction();

            return Transaction;
        }

        private void RollBack()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
            }
        }

        private void Save()
        {
            try
            {
                ChangeTracker.DetectChanges();
                SaveChanges();
            }
            catch (Exception ex)
            {
                RollBack();
                throw new Exception(ex.Message);
            }
        }

        private void Commit()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
                Transaction.Dispose();
                Transaction = null;
            }
        }

        public void SendChanges()
        {
            Save();
            Commit();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new InteractionMap());
            modelBuilder.ApplyConfiguration(new ProcedureMap());
            modelBuilder.ApplyConfiguration(new StepMap());
            modelBuilder.ApplyConfiguration(new TrainningSessionMap());
            modelBuilder.ApplyConfiguration(new WorkerMap());
        }
    }
}
