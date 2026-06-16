# Locatic — Agence de location de voitures

ASP.NET Core 8 MVC · SQLite · EF Core

## Démarrage rapide

```bash
cd Locatic.Web
dotnet run
```

La base SQLite est créée et migrée automatiquement au premier lancement.

## Générer une migration

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add NomDeLaMigration
dotnet ef database update
```

## Architecture

```
Models/       → entités du domaine
Data/         → AppDbContext
Repositories/ → accès base de données
Services/     → logique métier (règles)
ViewModels/   → formulaires / affichage
Controllers/  → orchestration
Views/        → Razor
```

## Partage des tâches

| Membre A | Membre B |
|---|---|
| Marques, Modèles, CRUD Voitures | CRUD Clients, Réservations, règles métier |
