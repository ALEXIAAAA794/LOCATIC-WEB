# Architecture du projet

## Vue d'ensemble

Locatic est une application web développée avec **ASP.NET Core MVC** permettant la gestion d'une agence de location de voitures.

L'application permet de gérer les principales entités métier :

- Marques
- Modèles
- Voitures
- Clients
- Réservations

Elle est conçue selon une architecture en couches afin de séparer les responsabilités et de faciliter la maintenance.

---

## Architecture applicative

Le projet est organisé autour des composants suivants :

- **Controllers** : réception des requêtes HTTP et gestion des actions utilisateur.
- **Models** : représentation des entités métier.
- **ViewModels** : modèles utilisés par les vues pour la saisie et l'affichage des données.
- **Services** : implémentation de la logique métier.
- **Data** : contexte Entity Framework Core, accès aux données et migrations SQLite.
- **Views** : interfaces utilisateur développées avec Razor.

Les règles métier principales sont implémentées dans les services, notamment :

- une réservation ne peut pas se terminer avant sa date de début ;
- une voiture ne peut pas être réservée plusieurs fois sur une période identique.

---

## Architecture DevOps

Le projet met en œuvre une chaîne DevOps complète composée des outils suivants.

### GitHub Actions

GitHub Actions assure l'intégration continue du projet.

Le pipeline automatise :

- la restauration des dépendances ;
- la compilation du projet ;
- l'exécution des tests unitaires ;
- la publication de l'application ;
- la construction de l'image Docker ;
- le scan de sécurité avec Trivy ;
- la publication de l'image Docker dans GitHub Container Registry (GHCR).

---

### Docker

L'application est conteneurisée grâce à Docker.

L'image Docker est construite automatiquement par le pipeline GitHub Actions puis publiée dans GitHub Container Registry.

---

### Terraform

Terraform prépare l'infrastructure Kubernetes locale.

Il permet notamment de créer :

- le namespace Kubernetes ;
- les volumes persistants nécessaires à SQLite ;
- les ressources utilisées par le déploiement.

---

### Ansible

Ansible orchestre le déploiement de l'application.

Le playbook permet notamment de :

- récupérer les informations générées par Terraform ;
- appliquer les ressources Kubernetes ;
- déployer l'application Locatic ;
- automatiser le processus de déploiement.

---

### Kubernetes

Kubernetes assure l'exécution de l'application conteneurisée.

Le cluster héberge :

- l'application Locatic ;
- Nginx ;
- le stockage persistant SQLite ;
- la stack de monitoring.

---

### Nginx

Nginx est utilisé comme reverse proxy.

Il reçoit les requêtes des utilisateurs puis les redirige vers l'application ASP.NET Core.

---

### Stockage SQLite

La base de données SQLite est stockée sur un **PersistentVolumeClaim** afin de conserver les données même après le redémarrage des pods.

---

### Monitoring

Le monitoring est assuré par :

- **Prometheus**, qui collecte les métriques de l'application et du cluster Kubernetes ;
- **Grafana**, qui affiche les tableaux de bord de supervision.

Le dashboard Grafana permet notamment de suivre :

- l'utilisation CPU ;
- l'utilisation mémoire ;
- les pods Kubernetes ;
- les requêtes HTTP ;
- l'état général de l'application.

---

## Schéma de fonctionnement

```
Développeur
      │
      ▼
 GitHub Repository
      │
      ▼
 GitHub Actions
      │
      ├── Compilation
      ├── Tests unitaires
      ├── Build Docker
      ├── Scan Trivy
      └── Publication GHCR
              │
              ▼
      GitHub Container Registry
              │
              ▼
        Terraform
              │
              ▼
          Ansible
              │
              ▼
        Kubernetes (Minikube)
              │
      ┌───────┴────────┐
      ▼                ▼
   Locatic          Nginx
      │
      ▼
 PersistentVolume (SQLite)

      ▼
 Prometheus → Grafana
```