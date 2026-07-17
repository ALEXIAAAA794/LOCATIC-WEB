# Déploiement local sur minikube

## Objectif

Cette procédure décrit le déploiement local de Locatic avec les ressources présentes dans le dépôt.

## Pré-requis

- Docker
- minikube
- kubectl
- Terraform
- Ansible
- image Docker `locatic-web:latest` construite localement

## Étapes réelles

1. Construire l’image Docker :

```bash
docker build -t locatic-web:latest .
```

2. Démarrer minikube :

```bash
minikube start
minikube status
```

3. Charger l’image dans minikube si nécessaire :

```bash
minikube image load locatic-web:latest
```

ou utiliser le daemon Docker de minikube :

```bash
eval $(minikube docker-env)
docker build -t locatic-web:latest .
```

4. Initialiser Terraform :

```bash
cd terraform
terraform init
terraform validate
terraform plan
terraform apply
```

5. Appliquer les manifests Kubernetes complémentaires (si utilisés) :

```bash
kubectl apply -f kubernetes/namespace.yaml
kubectl apply -f kubernetes/pvc.yaml
kubectl apply -f kubernetes/deployment.yaml
kubectl apply -f kubernetes/service.yaml
```

6. Vérifier l’état du déploiement :

```bash
kubectl get pods -n locatic
kubectl get svc -n locatic
kubectl get pvc -n locatic
```

7. Accéder à l’application :

```bash
kubectl port-forward svc/locatic-web 8080:80 -n locatic
```

Puis ouvrir `http://localhost:8080`.

## Note importante

Le dépôt contient deux approches :

- ressources Kubernetes gérées par Terraform dans `terraform/main.tf`
- manifests Kubernetes déclarés dans `kubernetes/`

Le `kubernetes/deployment.yaml` utilise l’image `locatic-web:test1` tandis que Terraform utilise `locatic-web:latest`. Il faut aligner ces deux références avant un déploiement complet.
