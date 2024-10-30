using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace SqlLab3
{
    public class Worker
    {

        public string surname { get; set; }
        public int salary { get; set; }
        public string position { get; set; }
        public int children_count { get; set; }
        public int experiance { get; set; }

        public Worker()
        { }
        public Worker(string name, int salary, string position, int childrens, int experiance)
        {
            this.surname = name;
            this.salary = salary;
            this.position = position;
            this.children_count = childrens;
            this.experiance = experiance;
        }
    }
    public class SolutionContext : DbContext
    {
        string connectionString = @"Server=ASUSVIVOBOOK\SQLEXPRESS;Database=Buhalteriya;Integrated Security=True;TrustServerCertificate=True;";
        public DbSet<Worker> Workers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>().HasKey(w => w.surname);
            modelBuilder.Entity<Worker>().ToTable("Buhalteria_db", "dbo");

        }
        public static void GetAthletsBySport(string position)
        {
            using (var context = new SolutionContext())
            {
                var workersFiltred = context.Workers.Where(worker => worker.position == position).Select(worker=>worker);

                if (workersFiltred != null)
                {
                    foreach (var worker in workersFiltred)
                    {
                        Console.WriteLine($"{worker.surname}, {worker.position}, {worker.experiance}, {worker.children_count}");

                    }
                }
                else
                {
                    Console.WriteLine("Athlets could not be found");
                }
            }

        }
    }

}