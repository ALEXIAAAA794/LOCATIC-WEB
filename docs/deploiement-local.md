# Déploiement local sur Minikube

## Objectif

Cette documentation décrit les étapes permettant de déployer l'application **Locatic** sur un cluster Kubernetes local avec **Minikube**.

Le déploiement utilise :

- Terraform pour préparer l'infrastructure Kubernetes ;
- Ansible pour orchestrer le déploiement ;
- Kubernetes pour exécuter l'application ;
- SQLite avec un volume persistant ;
- Nginx comme reverse proxy ;
- Prometheus et Grafana pour le monitoring.

---

## Prérequis

Les outils suivants doivent être installés sur la machine locale :

- Docker Desktop
- Minikube
- kubectl
- Terraform
- Ansible
- Git

Le cluster Minikube doit être démarré avant le déploiement.

---

## 1. Démarrer Minikube

```bash
minikube start
minikube status
```

Vérifier que le cluster est en état **Running**.

---

## 2. Préparer l'infrastructure avec Terraform

Depuis le dossier `terraform` :

```bash
terraform init
terraform validate
terraform plan
terraform apply
```

Terraform prépare notamment :

- le namespace Kubernetes ;
- le stockage persistant utilisé par SQLite ;
- les ressources nécessaires au déploiement.

---

## 3. Déployer l'application

Depuis la racine du projet :

```bash
ansible-playbook ansible/playbook.yml
```

Le playbook :

- vérifie les prérequis ;
- récupère les informations produites par Terraform ;
- applique les ressources Kubernetes ;
- déploie l'application Locatic ;
- déploie Nginx ;
- déploie la stack Prometheus/Grafana.

---

## 4. Vérifier le déploiement

Afficher les ressources du namespace :

```bash
kubectl get all -n locatic
```

Vérifier également les volumes persistants :

```bash
kubectl get pvc -n locatic
```

Contrôler les services :

```bash
kubectl get svc -A
```

---

## 5. Accéder à l'application

L'application peut être ouverte avec :

```bash
minikube service locatic-web -n locatic
```

ou, si nécessaire :

```bash
kubectl port-forward svc/locatic-web 8080:80 -n locatic
```

Puis ouvrir :

```
http://localhost:8080
```

---

## 6. Vérifier le monitoring

Accéder à Prometheus :

```bash
kubectl port-forward -n monitoring svc/monitoring-kube-prometheus-prometheus 9090:9090
```

Puis ouvrir :

```
http://localhost:9090
```

Accéder à Grafana :

```bash
kubectl port-forward -n monitoring svc/monitoring-grafana 3000:80
```

Puis ouvrir :

```
http://localhost:3000
```

Le dashboard Grafana permet de suivre :

- l'état de l'application Locatic ;
- les pods Kubernetes ;
- l'utilisation CPU et mémoire ;
- les requêtes HTTP ;
- les services de monitoring.

---

## Déploiement via GitHub Actions

Le pipeline GitHub Actions :

- compile le projet ;
- exécute les tests unitaires ;
- construit l'image Docker ;
- réalise un scan de sécurité avec Trivy ;
- publie automatiquement l'image dans GitHub Container Registry (GHCR).

Le déploiement sur Minikube n'est volontairement **pas réalisé** par GitHub Actions. Il est exécuté localement avec Terraform et Ansible.