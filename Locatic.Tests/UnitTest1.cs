using Microsoft.VisualStudio.TestTools.UnitTesting;
using Locatic.Web.Models;

namespace Locatic.Tests;

[TestClass]
public class ModelTests
{
    [TestMethod]
    public void Client_Should_Store_Properties()
    {
        // Arrange
        var client = new Client
        {
            Nom = "Dupont",
            Prenom = "Jean",
            Email = "jean.dupont@test.com",
            Telephone = "0612345678"
        };

        // Assert
        Assert.AreEqual("Dupont", client.Nom);
        Assert.AreEqual("Jean", client.Prenom);
        Assert.AreEqual("jean.dupont@test.com", client.Email);
        Assert.AreEqual("0612345678", client.Telephone);
    }

    [TestMethod]
    public void Marque_Should_Store_Properties()
    {
        // Arrange
        var marque = new Marque
        {
            Nom = "Peugeot",
            PaysOrigine = "France"
        };

        // Assert
        Assert.AreEqual("Peugeot", marque.Nom);
        Assert.AreEqual("France", marque.PaysOrigine);
    }

    [TestMethod]
    public void Voiture_Should_Store_Properties()
    {
        // Arrange
        var voiture = new Voiture
        {
            Immatriculation = "AA-123-BB",
            Annee = 2024,
            TarifJournalier = 49.99m,
            NombrePlaces = 5,
            Carburant = "Essence"
        };

        // Assert
        Assert.AreEqual("AA-123-BB", voiture.Immatriculation);
        Assert.AreEqual(2024, voiture.Annee);
        Assert.AreEqual(49.99m, voiture.TarifJournalier);
        Assert.AreEqual(5, voiture.NombrePlaces);
        Assert.AreEqual("Essence", voiture.Carburant);
    }
}