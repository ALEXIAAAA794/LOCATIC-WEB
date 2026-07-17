# Déploiement local sur minikube

## Objectif

Cette procédure décrit le parcours pour déployer Locatic sur minikube avec Nginx en reverse proxy, SQLite persistant et monitoring.

## Pré-requis

- Docker
- minikube
- kubectl
- Terraform
- Ansible
- image Docker Locatic disponible localement ou dans une registry

## Étapes

1. Vérifier l’application localement :

```bash
cd Locatic.Web
dotnet restore
dotnet run
```

2. Construire l’image Docker :

```bash
docker build -t locatic:local .
```

3. Démarrer minikube :

```bash
minikube start
minikube status
```

4. Préparer Terraform :

```bash
cd infra/terraform
terraform init
terraform plan
terraform apply
```

5. Exécuter Ansible :

```bash
cd ../ansible
ansible-playbook playbook.yml
```

6. Vérifier Kubernetes :

```bash
kubectl get all -n locatic
kubectl get pvc -n locatic
kubectl get svc -n locatic
```

7. Tester l’accès via Nginx :

```bash
kubectl port-forward svc/nginx 8080:80 -n locatic
```

Ouvrir ensuite http://localhost:8080 pour vérifier l’application.
