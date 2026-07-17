# Locatic — application de location de voitures

Ce dépôt contient Locatic, une application ASP.NET Core MVC avec SQLite, conçue pour une validation DevOps locale et un déploiement Kubernetes minimal.

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
.github/workflows/ci.yml
Dockerfile
terraform/
ansible/
kubernetes/
monitoring/
```

## Démarrage rapide

```bash
cd Locatic.Web
dotnet restore
dotnet run
```

## Build Docker local

```bash
docker build -t locatic-web:latest .
```

## CI/CD

Le pipeline GitHub Actions est défini dans `.github/workflows/ci.yml` et exécute :

- checkout du code
- installation de .NET 8
- restauration avec `dotnet restore Locatic.sln`
- compilation avec `dotnet build Locatic.sln --configuration Release --no-restore`
- publication de `Locatic.Web/Locatic.Web.csproj`
- construction de l’image Docker `locatic-web:latest`

## Infrastructure

- `terraform/` : ressources Kubernetes gérées par Terraform
- `ansible/` : playbook et rôles de vérification
- `kubernetes/` : manifests Kubernetes déclaratifs
- `monitoring/` : scripts de supervision

## Documentation

- docs/architecture.md
- docs/ci-cd.md
- docs/deploiement-local.md
- docs/terraform.md
- docs/ansible.md
- docs/kubernetes.md
- docs/monitoring.md
- docs/helm.md
- docs/exploitation.md
- docs/preuves/README.md
