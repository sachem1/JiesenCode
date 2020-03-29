using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Jiesen.Core.Model;

namespace Jiesen.EntityFramework
{
    public class JiesenDbContext : DbContext
    {
        public JiesenDbContext() : base("Data Source =.; Initial Catalog = Develop; User ID = sa; Password=sa1230.;Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<JiesenDbContext>());
        }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties().Where(x => x.Name == "Id").Configure(s => s.IsKey());
            modelBuilder.Conventions.Add(new Datetime2Convention());

            modelBuilder.Properties()
                .Where(x => x.GetCustomAttributes(false).OfType<IsUnicode>().Any())
                .Configure(c => c.IsUnicode(c.ClrPropertyInfo.GetCustomAttribute<IsUnicode>().Unicode));

            modelBuilder.Types().Configure(x => x.ToTable(GetTable(x.ClrType)));
        }

        private string GetTable(Type type)
        {
            var pluralizationService = DbConfiguration.DependencyResolver.GetService<IPluralizationService>();
            var result = pluralizationService.Pluralize(type.Name);
            result = Regex.Replace(result, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);

            return result.ToLower();
        }
    }



    public class Datetime2Convention : Convention
    {
        public Datetime2Convention()
        {
            this.Properties<DateTime>().Configure(d => d.HasColumnType("datetime2"));
        }
    }
}


