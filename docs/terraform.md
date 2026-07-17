# Terraform

Terraform prépare les ressources Kubernetes nécessaires :

- namespace
- volume persistant SQLite

Les principales commandes sont :

```
terraform init
terraform validate
terraform plan
terraform apply
```

Le fichier terraform.tfstate n'est pas versionné.