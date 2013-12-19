using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulator.Models
{
    public class SettingsDb : DbContext
    {
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Settings> Settings { get; set; }

        public DbSet<MapItem> MapItems { get; set; }
    }
}
