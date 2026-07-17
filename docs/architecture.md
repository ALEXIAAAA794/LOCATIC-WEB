# Architecture

## Présentation

Le projet reprend l'application ASP.NET Core MVC **Locatic** développée dans le cadre du projet POO.

L'objectif est de mettre en place une chaîne DevOps complète permettant :

- le développement via GitHub
- la validation automatique par GitHub Actions
- la publication d'une image Docker
- le déploiement local sur Minikube
- le monitoring avec Prometheus et Grafana

## Architecture

GitHub
↓
GitHub Actions
↓
Docker Image
↓
Terraform
↓
Ansible
↓
Kubernetes (Minikube)
├── Nginx
├── Locatic
├── SQLite (PVC)
├── Prometheus
└── Grafana

## Composants

### GitHub

Gestion du code source et des Pull Requests.

### GitHub Actions

Exécute :

- build
- tests
- construction Docker
- publication de l'image

### Terraform

Prépare l'infrastructure Kubernetes.

### Ansible

Orchestre le déploiement.

### Kubernetes

Exécute l'application.

### Nginx

Reverse proxy devant Locatic.

### SQLite

Stockage persistant grâce à un PersistentVolumeClaim.

### Prometheus

Collecte les métriques.

### Grafana

Affiche les tableaux de bord.