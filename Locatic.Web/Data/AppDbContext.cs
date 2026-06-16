using Microsoft.EntityFrameworkCore;
using Locatic.Web.Models;

namespace Locatic.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Marque> Marques => Set<Marque>();
    public DbSet<Modele> Modeles => Set<Modele>();
    public DbSet<Voiture> Voitures => Set<Voiture>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Marques
        modelBuilder.Entity<Marque>().HasData(
            new Marque { Id = 1, Nom = "Renault", PaysOrigine = "France" },
            new Marque { Id = 2, Nom = "Peugeot", PaysOrigine = "France" },
            new Marque { Id = 3, Nom = "BMW",     PaysOrigine = "Allemagne" }
        );

        // Seed Modèles
        modelBuilder.Entity<Modele>().HasData(
            new Modele { Id = 1, Nom = "Clio",   MarqueId = 1 },
            new Modele { Id = 2, Nom = "Megane", MarqueId = 1 },
            new Modele { Id = 3, Nom = "208",    MarqueId = 2 },
            new Modele { Id = 4, Nom = "Serie 3",MarqueId = 3 }
        );

        // Seed Voitures
        modelBuilder.Entity<Voiture>().HasData(
            new Voiture { Id = 1, Immatriculation = "AB-123-CD", Annee = 2021, TarifJournalier = 45m, NombrePlaces = 5, Carburant = "Essence",  ModeleId = 1 },
            new Voiture { Id = 2, Immatriculation = "EF-456-GH", Annee = 2022, TarifJournalier = 55m, NombrePlaces = 5, Carburant = "Diesel",   ModeleId = 3 },
            new Voiture { Id = 3, Immatriculation = "IJ-789-KL", Annee = 2023, TarifJournalier = 90m, NombrePlaces = 5, Carburant = "Hybride",  ModeleId = 4 }
        );

        // Seed Clients
        modelBuilder.Entity<Client>().HasData(
            new Client { Id = 1, Nom = "Dupont", Prenom = "Alice", Email = "alice@example.com", Telephone = "0600000001" },
            new Client { Id = 2, Nom = "Martin", Prenom = "Bob",   Email = "bob@example.com",   Telephone = "0600000002" }
        );
    }
}
