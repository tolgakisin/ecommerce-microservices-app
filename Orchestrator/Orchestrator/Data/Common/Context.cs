using Microsoft.EntityFrameworkCore;
using Orchestrator.Data.Entities;
using Orchestrator.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchestrator.Data.Common
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }

        public DbSet<EventLog> EventLogs { get; set; }
        public DbSet<EventProcess> EventProcesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventProcess>().HasData(
                new EventProcess { Id = 1, Name = "Event1", PreviousId = null },
                new EventProcess { Id = 2, Name = "Event2", PreviousId = 1 },
                new EventProcess { Id = 3, Name = "Event3", PreviousId = null },
                new EventProcess { Id = 4, Name = "Event4", PreviousId = 3 },
                new EventProcess { Id = 5, Name = "Event5", PreviousId = 4 },
                new EventProcess { Id = 6, Name = "Event6", PreviousId = null }
            );

            base.OnModelCreating(modelBuilder);
        }

        public EventProcess GetEventByName(string eventName)
        {
            return this.EventProcesses.FirstOrDefault(x => x.Name == eventName);
        }

        public EventProcess GetPreviousEvent(string eventName)
        {
            var @event = this.EventProcesses.FirstOrDefault(x => x.Name == eventName);

            if (@event == null) return null;

            return this.EventProcesses.FirstOrDefault(x => x.Id == @event.PreviousId);
        }

        public EventProcess GetNextEvent(string eventName)
        {
            var @event = this.EventProcesses.FirstOrDefault(x => x.Name == eventName);

            if (@event == null) return null;

            return this.EventProcesses.FirstOrDefault(x => x.PreviousId == @event.Id);
        }

        public TEntity Save<TEntity>(TEntity eventProcess) where TEntity : BaseEntity
        {
            if (eventProcess == null) return null;

            try
            {
                this.Entry(eventProcess).State = eventProcess.Id == default ? EntityState.Added : EntityState.Modified;

                if (this.SaveChanges() < 1) return null;
            }
            catch (Exception)
            {
                throw;
            }

            return eventProcess;
        }
    }
}
