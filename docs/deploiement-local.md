# Déploiement local

## Prérequis

- Docker Desktop
- Minikube
- kubectl
- Helm
- Terraform
- Ansible

## Étapes

1. Démarrer Minikube

```
minikube start
```

2. Appliquer Terraform

```
terraform init
terraform apply
```

3. Exécuter Ansible

```
ansible-playbook playbook.yml
```

4. Vérifier

```
kubectl get all -n locatic
```

L'application est accessible via Nginx.