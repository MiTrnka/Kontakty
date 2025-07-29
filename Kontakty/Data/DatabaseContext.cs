using Microsoft.EntityFrameworkCore;
using System;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Tento způsob je v pořádku pro začátek.
        // Do budoucna je lepší cestu k souboru databáze skládat dynamicky,
        // aby fungovala na všech platformách (Android, iOS...).
        optionsBuilder.UseSqlite("Data Source=contacts.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().ToTable("Contacts");
    }
}
