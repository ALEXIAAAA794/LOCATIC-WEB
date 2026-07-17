# Déploiement Kubernetes

## Ressources utilisées

Le déploiement Kubernetes de Locatic doit contenir au minimum :

- un Deployment pour l’application ASP.NET Core
- un Deployment ou configuration dédiée pour Nginx
- un Service qui expose l’entrée utilisateur
- un volume persistant pour la base SQLite
- des probes de santé pour contrôler l’état du pod
- des variables d’environnement configurables

## Services exposés

L’accès utilisateur passe par Nginx. L’application Locatic reste donc derrière le reverse proxy, ce qui correspond à l’architecture attendue.

## Stockage SQLite

Le fichier SQLite doit être monté depuis un PersistentVolumeClaim dans le pod applicatif. Cela permet de conserver l’état métier même si le pod est redémarré ou remplacé.

## Configuration Nginx

Nginx est chargé de recevoir les requêtes HTTP et de les transmettre à l’application ASP.NET Core. Sa configuration doit rester simple, lisible et adaptée à un environnement local de test.

## Choix d’architecture

Le déploiement vise à séparer clairement :

- l’entrée utilisateur : Nginx
- l’application métier : Locatic
- la persistance : SQLite sur volume
- la supervision : Prometheus et Grafana
