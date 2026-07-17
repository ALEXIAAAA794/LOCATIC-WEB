# Monitoring Prometheus et Grafana

## Services monitorés

Le déploiement doit surveiller :

- l’application Locatic
- Nginx
- le stockage SQLite
- les composants de monitoring

## Métriques suivies

Prometheus doit collecter des métriques sur :

- la disponibilité des pods
- la disponibilité des services
- l’état de l’application
- la santé du stockage

## Accès

Prometheus et Grafana doivent être accessibles localement via port-forward ou via les services exposés.

## Dashboard

Le dashboard Grafana doit permettre de vérifier rapidement :

- si Locatic est disponible
- si Nginx route correctement
- si le volume SQLite est monté
- si la collecte de métriques fonctionne
