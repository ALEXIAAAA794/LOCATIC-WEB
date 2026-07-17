# Déploiement local sur minikube

## Objectif

Cette procédure décrit le parcours concret à suivre pour transformer l’application Locatic, déjà fonctionnelle localement, en une application déployée sur minikube avec une architecture proche de celle attendue par le mini-projet : Nginx en reverse proxy, application ASP.NET Core derrière lui, base SQLite persistante et monitoring local.

## Pré-requis locaux

Avant de commencer, il faut disposer de :

- Docker fonctionnel
- minikube démarré
- kubectl configuré pour le cluster local
- Terraform installé
- Ansible installé
- une image Docker de Locatic déjà construite et publiée, ou au moins disponible localement pour le déploiement

## Étapes de déploiement

### 1. Vérifier l’application localement

Avant toute mise en conteneur, il faut s’assurer que l’application Locatic démarre correctement :

```bash
cd Locatic.Web
dotnet restore
dotnet run
```

Cette étape sert à confirmer que le cœur applicatif est stable avant son déploiement dans Kubernetes.

### 2. Construire l’image Docker

Une fois l’application validée localement, l’image est construite à partir du Dockerfile du projet :

```bash
docker build -t locatic:local .
```

L’image doit ensuite être accessible au cluster minikube, soit localement, soit via une registry.

### 3. Démarrer le cluster local

```bash
minikube start
minikube status
```

### 4. Préparer l’infrastructure avec Terraform

```bash
cd infra/terraform
terraform init
terraform plan
terraform apply
```

Cette étape prépare le namespace, le stockage persistant et les paramètres nécessaires au déploiement.

### 5. Orchestrer le déploiement avec Ansible

```bash
cd ../ansible
ansible-playbook playbook.yml
```

Le playbook applique ensuite les ressources Kubernetes attendues : Nginx, l’application Locatic, la persistance SQLite et les composants de monitoring.

### 6. Vérifier les ressources Kubernetes

```bash
kubectl get all -n locatic
kubectl get pvc -n locatic
kubectl get svc -n locatic
```

### 7. Vérifier l’accès via Nginx

L’accès utilisateur doit passer par Nginx, pas directement par l’application. Un port-forward local permet de tester le résultat :

```bash
kubectl port-forward svc/nginx 8080:80 -n locatic
```

Ensuite, il faut ouvrir l’URL locale et confirmer que l’interface de Locatic est bien visible via le reverse proxy.

## Pourquoi ce parcours est cohérent

Ce flux suit l’architecture du projet :

- l’application Locatic reste le cœur métier
- Docker permet de l’isoler et de la rendre portable
- Terraform prépare l’environnement local
- Ansible orchestre l’installation finale
- Kubernetes exécute l’ensemble avec une logique plus proche d’un environnement de production
