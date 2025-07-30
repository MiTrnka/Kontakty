using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontakty;

public class DatabaseContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    // Konstruktor, který zajistí, že se databáze vytvoří
    public DatabaseContext()
    {
        // Zajistí, že databáze a tabulka existují.
        // Pokud ne, vytvoří je podle modelu.
        Database.EnsureCreated();
    }
    ava
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "contacts.db");

        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().ToTable("Contacts");
    }
}
