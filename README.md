# Monitoring

Le monitoring du projet Locatic est assuré par Prometheus et Grafana grâce au chart Helm **kube-prometheus-stack**.

## Installation

Ajouter le dépôt Helm :

```bash
helm repo add prometheus-community https://prometheus-community.github.io/helm-charts
helm repo update
```

Installer la stack :

```bash
helm install monitoring prometheus-community/kube-prometheus-stack \
  --namespace monitoring \
  --create-namespace
```

Vérifier que les pods sont démarrés :

```bash
kubectl get pods -n monitoring
```

## ServiceMonitor

Le fichier `servicemonitor.yaml` permet à Prometheus de découvrir automatiquement les métriques de l'application Locatic.

Application :

```bash
kubectl apply -f monitoring/servicemonitor.yaml
```

## Accès Prometheus

```bash
kubectl port-forward svc/monitoring-kube-prometheus-prometheus -n monitoring 9090:9090
```

Puis ouvrir :

```
http://localhost:9090
```

## Accès Grafana

```bash
kubectl port-forward svc/monitoring-grafana -n monitoring 3000:80
```

Puis ouvrir :

```
http://localhost:3000
```

## Dashboard

Le dashboard Grafana affiche notamment :

- Utilisation CPU de Locatic
- Nombre total de requêtes ASP.NET Core
- État des services Kubernetes
- Métriques Prometheus

## Monitoring

Le projet utilise :

- Prometheus
- Grafana
- ServiceMonitor

Le monitoring peut être installé avec :

```bash
cd monitoring
./install-monitoring.sh
```

Les métriques de l'application sont automatiquement collectées grâce au fichier `servicemonitor.yaml`.