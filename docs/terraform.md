# Documentation Terraform

## Objectif

Terraform est utilisé pour préparer l’infrastructure locale nécessaire au déploiement de Locatic sur minikube. Son rôle est de créer des composants de base qui seront ensuite consommés par Ansible et par les manifests Kubernetes.

## Ressources que Terraform doit gérer

La configuration Terraform doit pouvoir gérer au minimum :

- un namespace Kubernetes dédié à l’application Locatic
- un PersistentVolumeClaim pour la base SQLite
- des variables de configuration utiles au déploiement
- des outputs qui seront repris par Ansible

## Variables attendues

Les variables Terraform doivent permettre de personnaliser au moins :

- le nom du namespace
- le nom du stockage persistant
- la taille du volume
- l’image à déployer et son tag si vous choisissez de les injecter depuis Terraform

## Outputs utiles

Les outputs Terraform doivent exposer les informations qui seront utiles à la suite du déploiement. Par exemple :

- le namespace à utiliser
- le nom du PVC SQLite
- les valeurs de configuration partagées avec les manifests

## Gestion de l’état

L’état Terraform ne doit pas être versionné. Il doit rester local à la machine de déploiement. Cela évite de committer des données sensibles ou des informations propres à la machine locale.

Le workflow recommandé est :

```bash
terraform init
terraform validate
terraform plan
terraform apply
```

L’état généré doit donc être ignoré par Git et conservé localement.
