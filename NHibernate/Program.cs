using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Dialect.Function;
using NHibernate.Driver;
using NHibernateExample.Models;

namespace NHibernateExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\learning; Initial Catalog=learning; Integrated Security=true";
            var config = new Configuration();

            config.DataBaseIntegration(d =>
            {
                d.ConnectionString = connectionString;
                d.Dialect<MsSql2012Dialect>();
                d.Driver<SqlClientDriver>();
            });

            config.AddAssembly(Assembly.GetExecutingAssembly());
            var sessionFactory = config.BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                // add
                //var car = new Car() { Brand = "Audi", Model = "R8" };
                //session.Save(car); // every time creates new car

                // delete
                //var getCar = session.Get<Car>(2);
                //session.Delete(getCar);
                //session.Flush();

                //update
                //session.BeginTransaction(); // without transaction it doesn't save
                //var updateCar = new Car() { Id = 3, Brand = "Audi", Model = "R7" };
                //session.SaveOrUpdate(updateCar);
                //session.Transaction.Commit();

                // read
                var cars = session.CreateCriteria<Car>().List<Car>();
                cars.ToList().ForEach(x => Console.WriteLine(x.Model));
            }
        }
    }
}