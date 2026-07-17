# Monitoring Prometheus et Grafana

## Services monitorés

Le déploiement de Locatic doit permettre de surveiller au moins :

- l’application web Locatic
- Nginx
- le stockage SQLite
- les composants de monitoring eux-mêmes

## Métriques suivies

Prometheus doit collecter des métriques permettant de savoir si :

- les pods sont prêts
- les services répondent
- l’application est disponible
- un problème de santé ou de performance apparaît

Grafana sert ensuite à visualiser ces éléments dans un tableau de bord lisible.

## Accès à Prometheus

Prometheus doit être accessible localement depuis la machine de déploiement. Il concentre les données collectées sur le cluster et sert de source de truth pour la supervision.

## Accès à Grafana

Grafana permet de transformer ces métriques en indicateurs visuels. Le dashboard doit être pensé pour montrer rapidement si l’application, Nginx et le stockage fonctionnent correctement.

## Lecture du dashboard

Un observateur doit pouvoir répondre rapidement à ces questions :

- l’interface Locatic est-elle accessible ?
- Nginx route-t-il correctement les requêtes ?
- le volume SQLite est-il bien monté ?
- le monitoring collecte bien les métriques attendues ?
