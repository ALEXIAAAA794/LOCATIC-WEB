# Documentation Terraform

## Objectif

Terraform prépare l’infrastructure locale nécessaire au déploiement de Locatic sur minikube.

## Ressources gérées

La configuration doit gérer au minimum :

- le namespace Kubernetes
- un PersistentVolumeClaim pour SQLite
- les variables de déploiement
- les outputs utiles pour Ansible

## Variables attendues

Les variables doivent inclure :

- le nom du namespace
- le nom du storage
- la taille du volume
- l’image et le tag si utilisé

## Outputs utiles

Les outputs doivent exposer :

- le namespace
- le nom du PVC
- d’autres paramètres de configuration

## Gestion de l’état

L’état Terraform ne doit pas être versionné et doit rester local.

## Commandes

```bash
terraform init
terraform validate
terraform plan
terraform apply
```
