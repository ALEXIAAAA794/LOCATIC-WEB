# Documentation Terraform

## Objectif

Terraform crée les ressources Kubernetes nécessaires à Locatic sur l’environnement local.

## Fichiers réels

- `terraform/main.tf`
- `terraform/variables.tf`
- `terraform/outputs.tf` (actuellement vide)

## Ressources déclarées dans `terraform/main.tf`

- `kubernetes_namespace.locatic`
- `kubernetes_persistent_volume_claim.sqlite_pvc`
- `kubernetes_deployment.locatic`
- `kubernetes_service.locatic`

## Détails de la configuration

### Namespace

Le namespace est défini via la variable `namespace` et sa valeur par défaut est `locatic`.

### Volume SQLite

Le PVC `sqlite-pvc` demande une capacité de `1Gi` en `ReadWriteOnce`.

### Deployment Locatic

Le déploiement déploie un seul pod `locatic-web` avec :

- image : valeur de `var.image`
- `image_pull_policy = "Never"`
- port : `8080`
- variable d’environnement `ASPNETCORE_URLS=http://+:8080`
- volume mount : `/data`
- readiness et liveness probes sur `/`

### Service

Le service expose l’application sur le port `80` et cible `8080`. Il est de type `NodePort`.

## Variables

`terraform/variables.tf` définit :

- `namespace` par défaut `locatic`
- `image` par défaut `locatic-web:latest`

## Outputs

Le fichier `terraform/outputs.tf` existe mais n’a pas encore de définitions d’outputs.

## Commandes à exécuter

```bash
cd terraform
terraform init
terraform validate
terraform plan
terraform apply
```

## Alignement nécessaire

Le dépôt contient également des manifests Kubernetes sous `kubernetes/`. Il faut veiller à harmoniser l’image et le déploiement entre `terraform/main.tf` et ces manifests.
