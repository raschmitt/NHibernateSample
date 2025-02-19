using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

namespace NHibernateSample
{
    internal abstract class Program
    {
        static void Main(string[] args)
        {
            CreateSessionFactory();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .Driver<MicrosoftDataSqlClientDriver>()
                    .ConnectionString("Server=localhost,1433;Database=master;User Id=sa;Password=sa@@2022;TrustServerCertificate=True")
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .BuildSessionFactory();
        }
    }

    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class UserMap : FluentNHibernate.Mapping.ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
        }
    }
}