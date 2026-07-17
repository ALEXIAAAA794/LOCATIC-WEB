# Déploiement Kubernetes

## Ressources utilisées

Le déploiement Kubernetes de Locatic doit inclure :

- un Deployment pour l’application ASP.NET Core
- un Deployment pour Nginx
- un Service exposant Nginx
- un PersistentVolumeClaim pour SQLite
- des probes de santé
- des variables d’environnement configurables

## Services exposés

L’accès utilisateur doit passer par Nginx. L’application Locatic reste derrière le reverse proxy.

## Stockage SQLite

La base SQLite est stockée dans un volume persistant monté dans le pod applicatif.

## Configuration Nginx

Nginx reçoit les requêtes HTTP et les redirige vers l’application ASP.NET Core.

## Architecture

Le déploiement sépare :

- l’entrée utilisateur : Nginx
- l’application métier : Locatic
- la persistance : SQLite
- la supervision : Prometheus et Grafana
