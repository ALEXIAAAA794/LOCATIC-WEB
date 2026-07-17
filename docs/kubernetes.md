# Déploiement Kubernetes

## Ressources présentes

Le dépôt contient des manifests Kubernetes dans le dossier `kubernetes/` :

- `kubernetes/namespace.yaml`
- `kubernetes/pvc.yaml`
- `kubernetes/deployment.yaml`
- `kubernetes/service.yaml`

Il contient aussi une version Terraform de ces ressources dans `terraform/main.tf`.

## Ressources déclarées

### Namespace

- `locatic`

### PersistentVolumeClaim

- `sqlite-pvc`
- accessModes : `ReadWriteOnce`
- storage request : `1Gi`

### Deployment

Nom : `locatic-web`

Conteneur :

- image : `locatic-web:test1` dans `kubernetes/deployment.yaml`
- port : `8080`
- env `ASPNETCORE_URLS=http://+:8080`
- mount `/data` sur `sqlite-storage`
- readiness/liveness probes sur `/`

### Service

Nom : `locatic-web`

- selector : `app: locatic-web`
- port `80` cible `8080`
- type : `NodePort`

## Points d’attention

- Le manifest `kubernetes/deployment.yaml` utilise l’image `locatic-web:test1`.
- Le Terraform `terraform/main.tf` utilise la variable `image` avec `locatic-web:latest` par défaut.
- `imagePullPolicy` est défini sur `Never` dans Terraform, ce qui implique que l’image doit être disponible localement dans minikube.

## Absence actuelle de Nginx

Le dépôt ne contient pas de manifest Nginx. L’accès est donc assuré directement via le service `locatic-web`.

## Utilisation

Pour tester les manifests Kubernetes :

```bash
kubectl apply -f kubernetes/namespace.yaml
kubectl apply -f kubernetes/pvc.yaml
kubectl apply -f kubernetes/deployment.yaml
kubectl apply -f kubernetes/service.yaml
```

Puis vérifier :

```bash
kubectl get pods -n locatic
kubectl get svc -n locatic
kubectl get pvc -n locatic
```

Et exposer le service localement :

```bash
kubectl port-forward svc/locatic-web 8080:80 -n locatic
```
