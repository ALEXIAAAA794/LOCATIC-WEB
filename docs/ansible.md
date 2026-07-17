# Documentation Ansible

## Rôle du playbook

Ansible orchestre le déploiement local entre Terraform et Kubernetes. Il rend la séquence plus reproductible que des commandes manuelles.

## Étapes orchestrées

Le playbook doit :

1. vérifier que minikube et kubectl sont disponibles
2. récupérer les outputs Terraform
3. préparer les variables de déploiement
4. appliquer ou mettre à jour les ressources Kubernetes

## Dépendance aux outputs Terraform

Le playbook s’appuie sur les informations fournies par Terraform, comme :

- le namespace
- le nom du PVC SQLite
- d’autres paramètres de configuration

## Intérêt

Ansible permet d’automatiser la chaîne de déploiement et de limiter les erreurs de saisie manuelle.
