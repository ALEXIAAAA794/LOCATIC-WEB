# Documentation Ansible

## Rôle du playbook

Ansible est utilisé comme couche d’orchestration locale entre Terraform et Kubernetes. Son rôle est de rendre le déploiement de Locatic plus reproductible et moins manuel.

## Étapes orchestrées

Le playbook doit idéalement :

1. vérifier la présence de minikube et de kubectl
2. vérifier que le namespace de déploiement existe
3. récupérer les informations produites par Terraform
4. préparer les variables nécessaires à l’application
5. appliquer ou mettre à jour les ressources Kubernetes de Locatic

## Dépendance aux outputs Terraform

Le playbook dépend directement des outputs Terraform. Par exemple, il peut avoir besoin de :

- le namespace à utiliser
- le nom du PVC SQLite
- les paramètres de configuration de l’application

C’est pourquoi Terraform est exécuté avant Ansible dans la séquence de déploiement.

## Intérêt du choix

Ansible permet d’éviter de lancer à la main une suite de commandes Kubernetes. Pour un projet comme Locatic, cela rend le déploiement plus lisible et surtout plus facile à reproduire lors d’une nouvelle installation.
