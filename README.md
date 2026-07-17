# Locatic — application de location de voitures

Ce dépôt reprend l’application Locatic, une application ASP.NET Core MVC de gestion d’agence de location de voitures. Le projet est déjà structuré autour de plusieurs concepts métier : marques, modèles, voitures, clients et réservations.

## Ce qui existe vraiment dans ce dépôt

Le cœur applicatif se trouve dans [Locatic.Web](Locatic.Web). On y retrouve :

- des contrôleurs MVC pour les voitures et les réservations
- des services métier qui encapsulent la logique
- un contexte Entity Framework Core pour SQLite
- des vues Razor pour l’interface de gestion

La logique métier la plus visible aujourd’hui est celle des réservations :

- la date de fin ne peut pas être antérieure à la date de début
- une voiture ne peut pas être réservée sur une période déjà prise

## Fonctionnalités principales

L’application permet de gérer :

- les marques de véhicules
- les modèles de véhicules
- les voitures disponibles
- les clients
- les réservations

## Prérequis

Pour travailler sur le projet localement, il faut :

- .NET 8 SDK
- Docker si l’on veut construire l’image de l’application
- minikube, kubectl, Terraform et Ansible si l’on veut aller jusqu’au déploiement local complet

## Structure du dépôt

```text
Locatic.sln
README.md
Locatic.Web/
  Controllers/      -> écrans MVC et actions HTTP
  Data/             -> AppDbContext et migrations EF Core
  Models/           -> entités métier
  Repositories/     -> abstractions d’accès aux données
  Services/         -> logique métier
  ViewModels/       -> objets utilisés par les vues
  Views/            -> pages Razor
  Program.cs        -> point d’entrée ASP.NET Core
```

## Démarrage rapide

```bash
cd Locatic.Web
dotnet restore
dotnet run
```

Au lancement, l’application applique les migrations EF Core et crée la base SQLite si nécessaire. Le fichier de base est utilisé localement sous forme de fichier SQLite, ce qui est important pour la suite du déploiement Kubernetes.

## Ce que cette documentation couvre

Cette documentation ne se limite pas à une liste de commandes. Elle explique :

- comment le projet est organisé
- quelle place prend l’application Locatic dans la chaîne DevOps attendue
- comment préparer la conteneurisation, la CI, l’infrastructure locale et le déploiement sur minikube

## Documentation associée

- [docs/architecture.md](docs/architecture.md)
- [docs/ci-cd.md](docs/ci-cd.md)
- [docs/deploiement-local.md](docs/deploiement-local.md)
- [docs/terraform.md](docs/terraform.md)
- [docs/ansible.md](docs/ansible.md)
- [docs/kubernetes.md](docs/kubernetes.md)
- [docs/helm.md](docs/helm.md)
- [docs/monitoring.md](docs/monitoring.md)
- [docs/exploitation.md](docs/exploitation.md)
- [docs/preuves/README.md](docs/preuves/README.md)
