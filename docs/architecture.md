# Architecture du projet

## Vue d’ensemble

Locatic est une application web ASP.NET Core MVC dédiée à la gestion d’une agence de location de voitures. Son cœur fonctionnel est centré sur les entités métier suivantes :

- Marques
- Modèles
- Voitures
- Clients
- Réservations

L’application actuelle est déjà orientée autour d’un modèle de consultation et de création de données. Les contrôleurs principaux, comme les contrôleurs de voitures et de réservations, mettent en œuvre une logique CRUD et une logique métier spécifique aux réservations.

## Composition applicative réelle

Le projet est structuré de façon claire en couches :

- Controllers : réception des requêtes HTTP, validation de formulaire et redirection
- Services : logique métier, notamment pour les réservations
- Data : contexte Entity Framework Core et migrations SQLite
- Models : entités métier
- ViewModels : objets utilisés pour les vues de création et d’édition
- Views : pages Razor affichées à l’utilisateur

Par exemple, les réservations ne se contentent pas d’être stockées : elles sont soumises à deux règles métier visibles dans le service de réservation :

- la date de fin ne peut pas être antérieure à la date de début
- une voiture ne peut pas être réservée deux fois sur une même période

## Pourquoi cette application est adaptée à un déploiement DevOps

Le projet est intéressant à déployer parce qu’il possède déjà :

- un cœur applicatif simple à isoler dans un conteneur
- une base SQLite facile à externaliser via un volume
- une interface web MVC qui peut être exposée derrière un reverse proxy
- des données métier cohérentes à maintenir malgré les redémarrages

## Rôle de GitHub Actions

GitHub Actions doit servir à valider le dépôt automatiquement avant toute intégration. Dans la logique du mini-projet, il devra :

- récupérer le code
- restaurer les dépendances
- compiler l’application
- exécuter les éventuels tests
- construire une image Docker
- préparer la publication de cette image depuis la branche main

Le pipeline ne doit pas remplacer le déploiement local : il doit uniquement valider l’état du dépôt.

## Rôle de Terraform

Terraform est utilisé pour préparer l’infrastructure locale nécessaire au déploiement. Dans ce projet, il doit permettre de définir :

- un namespace Kubernetes dédié à Locatic
- un stockage persistant pour la base SQLite
- des variables de configuration réutilisables par Ansible
- un état local propre, non versionné

L’intérêt est de garder l’infrastructure déclarative et reproductible sur la machine de l’étudiant.

## Rôle d’Ansible

Ansible sert d’orchestrateur entre la préparation Terraform et le déploiement Kubernetes. Il permet de :

- vérifier la présence de minikube et des outils nécessaires
- récupérer les valeurs générées par Terraform
- préparer les manifests ou variables de déploiement
- appliquer les ressources Kubernetes de façon automatisée

## Rôle du déploiement Kubernetes

Kubernetes est utilisé pour exécuter l’application dans un environnement conteneurisé. Le but est de garantir que :

- l’application tourne de façon stable
- l’accès utilisateur passe par un point d’entrée défini
- les données persistantes restent disponibles en cas de redémarrage de pod
- les modifications peuvent être déployées de façon maîtrisée

## Rôle de Nginx

Nginx joue ici le rôle de reverse proxy. Il est le point d’entrée que l’utilisateur voit, tandis que l’application ASP.NET Core reste derrière lui.

Ce choix est cohérent avec l’architecture demandée :

- séparation nette entre l’interface d’entrée et l’application métier
- meilleure lisibilité de l’architecture
- parcours utilisateur plus simple à superviser

## Rôle du volume SQLite

La base SQLite est un élément central du projet. Puisqu’elle est utilisée localement aujourd’hui par EF Core, elle doit être rendue persistante dans le déploiement Kubernetes via un volume monté dans le pod applicatif.

Le volume permet de garantir que :

- les données ne sont pas perdues lors d’un redémarrage du pod
- l’application conserve son état métier
- le chemin du fichier SQLite peut être défini proprement dans le conteneur

## Rôle du monitoring

Prometheus et Grafana complètent l’architecture en donnant une visibilité sur l’état du système. Ils permettent de suivre :

- la disponibilité de l’application
- la santé de Nginx
- l’état du stockage SQLite
- l’activité des composants déployés

Le monitoring est ici un outil de diagnostic important, surtout quand l’application est déplacée vers un environnement Kubernetes local.
