# Architecture du projet

## Vue d’ensemble

Locatic est une application web ASP.NET Core MVC dédiée à la gestion d’une agence de location de voitures. Son cœur fonctionnel est centré sur les entités métier suivantes :

- Marques
- Modèles
- Voitures
- Clients
- Réservations

## Composition applicative réelle

Le projet est structuré de façon claire en couches :

- Controllers : réception des requêtes HTTP, validation de formulaire et redirection
- Services : logique métier, notamment pour les réservations
- Data : contexte Entity Framework Core et migrations SQLite
- Models : entités métier
- ViewModels : objets utilisés pour les vues de création et d’édition
- Views : pages Razor affichées à l’utilisateur

Les réservations sont soumises à des règles métier visibles dans le service de réservation :

- la date de fin ne peut pas être antérieure à la date de début
- une voiture ne peut pas être réservée deux fois sur une même période

## Rôle de GitHub Actions

GitHub Actions valide automatiquement le dépôt avant toute intégration. Il doit :

- récupérer le code
- restaurer les dépendances
- compiler l’application
- exécuter les tests
- construire une image Docker
- préparer la publication de l’image depuis main

## Rôle de Terraform

Terraform prépare l’infrastructure locale nécessaire au déploiement. Il définit :

- un namespace Kubernetes dédié à Locatic
- un stockage persistant pour la base SQLite
- des variables réutilisables par Ansible
- un état local non versionné

## Rôle d’Ansible

Ansible orchestre le déploiement local entre Terraform et Kubernetes. Il permet de :

- vérifier la présence de minikube
- récupérer les outputs Terraform
- préparer les variables de déploiement
- appliquer ou mettre à jour les ressources Kubernetes

## Rôle de Kubernetes

Kubernetes exécute l’application conteneurisée. L’objectif est de :

- faire tourner Locatic de façon stable
- exposer l’interface via Nginx
- conserver la base SQLite sur un volume persistant
- mettre à jour les ressources de façon contrôlée

## Rôle de Nginx

Nginx joue le rôle de reverse proxy. L’accès utilisateur passe par Nginx, tandis que l’application reste derrière ce point d’entrée.

## Rôle du volume SQLite

Le fichier SQLite doit être monté depuis un PersistentVolumeClaim dans le pod applicatif afin de préserver les données entre les redémarrages.

## Rôle du monitoring

Prometheus et Grafana fournissent une visibilité sur l’état de l’architecture : disponibilité, santé du service et état du stockage.
