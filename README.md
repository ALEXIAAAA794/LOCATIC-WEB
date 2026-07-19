# Locatic — Application de location de voitures

## Présentation

Locatic est une application web développée avec **ASP.NET Core MVC** permettant la gestion d'une agence de location de voitures.

Le projet met en œuvre une chaîne DevOps complète reposant sur Docker, GitHub Actions, Terraform, Ansible, Kubernetes, Prometheus et Grafana.

---

# Fonctionnalités

L'application permet de gérer :

- les marques
- les modèles
- les voitures
- les clients
- les réservations

Les principales règles métier sont :

- une réservation ne peut pas se terminer avant sa date de début ;
- une voiture ne peut pas être réservée plusieurs fois sur une même période.

---

# Technologies utilisées

- ASP.NET Core 8 MVC
- Entity Framework Core
- SQLite
- MSTest
- Docker
- GitHub Actions
- GitHub Container Registry (GHCR)
- Trivy
- Terraform
- Ansible
- Kubernetes (Minikube)
- Nginx
- Prometheus
- Grafana

---

# Prérequis

Avant de lancer le projet, installer :

- .NET 8 SDK
- Docker Desktop
- Git
- Minikube
- kubectl
- Terraform
- Ansible

---

# Structure du projet

```text
Locatic.sln
README.md

Locatic.Web/
├── Controllers/
├── Data/
├── Models/
├── Repositories/
├── Services/
├── ViewModels/
├── Views/
└── Program.cs

Locatic.Tests/

.github/
Dockerfile

terraform/
ansible/
kubernetes/
monitoring/
docs/
```

---

# Exécution de l'application

Restaurer les dépendances :

```bash
dotnet restore
```

Lancer l'application :

```bash
dotnet run --project Locatic.Web
```

L'application est ensuite accessible sur :

```
http://localhost:5000
```

ou

```
https://localhost:5001
```

(selon la configuration ASP.NET Core).

---

# Tests unitaires

Exécuter les tests :

```bash
dotnet test
```

Les tests sont également exécutés automatiquement par GitHub Actions.

---

# Construction de l'image Docker

Construire l'image Docker :

```bash
docker build -t locatic-web:latest .
```

---

# Pipeline CI/CD

Le pipeline GitHub Actions automatise les étapes suivantes :

- récupération du dépôt ;
- installation du SDK .NET 8 ;
- restauration des dépendances ;
- compilation du projet ;
- exécution des tests unitaires ;
- publication de l'application ;
- construction de l'image Docker ;
- scan de sécurité avec Trivy ;
- publication de l'image dans GitHub Container Registry (GHCR) lors d'un `push` sur la branche `main`.

---

# Déploiement

Le déploiement local repose sur :

- Terraform pour préparer l'infrastructure Kubernetes ;
- Ansible pour vérifier l'environnement de déploiement ;
- Kubernetes (Minikube) pour exécuter l'application ;
- Nginx comme reverse proxy ;
- SQLite avec un volume persistant.

---

# Monitoring

Le monitoring est assuré grâce à :

- Prometheus pour la collecte des métriques ;
- Grafana pour la visualisation des métriques.

Les métriques de l'application sont exposées via l'endpoint :

```
/metrics
```

---

# Documentation

La documentation du projet est disponible dans le dossier `docs/` :

- architecture.md
- ansible.md
- ci-cd.md
- deploiement-local.md
- exploitation.md
- kubernetes.md
- monitoring.md
- terraform.md
- preuves/

---

# Auteur

Projet réalisé dans le cadre d'un mini-projet DevOps autour de l'application **Locatic**.