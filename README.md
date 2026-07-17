# Locatic — application de location de voitures

Ce dépôt contient le projet Locatic, une application ASP.NET Core MVC avec une base SQLite. Le but est de préparer cette application pour une chaîne DevOps locale comprenant Docker, GitHub Actions, Terraform, Ansible et Kubernetes.

## Prérequis

- .NET 8 SDK
- Docker
- minikube
- kubectl
- Terraform
- Ansible

## Structure du dépôt

```text
Locatic.sln
README.md
Locatic.Web/
  Controllers/
  Data/
  Models/
  Repositories/
  Services/
  ViewModels/
  Views/
  Program.cs
```

## Démarrage rapide

```bash
cd Locatic.Web
dotnet restore
dotnet run
```

L’application applique les migrations EF Core et crée la base SQLite au démarrage.

## Documentation

- docs/architecture.md
- docs/ci-cd.md
- docs/deploiement-local.md
- docs/terraform.md
- docs/ansible.md
- docs/kubernetes.md
- docs/helm.md
- docs/monitoring.md
- docs/exploitation.md
- docs/preuves/README.md
