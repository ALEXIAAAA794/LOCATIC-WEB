# Documentation Ansible

## Objectif

Ansible est utilisé pour préparer l'environnement DevOps local du projet **Locatic**.

Le playbook principal orchestre les différents rôles Ansible du projet afin de vérifier que les outils nécessaires au déploiement sont correctement installés et disponibles sur la machine.

---

## Structure

Le playbook principal est situé dans :

```text
ansible/playbook.yml
```

Son contenu est le suivant :

```yaml
---
- name: Configuration de l'environnement DevOps
  hosts: local
  become: false

  roles:
    - docker
    - kubernetes
    - terraform
```

Le playbook exécute successivement les trois rôles :

- `docker`
- `kubernetes`
- `terraform`

---

## Description des rôles

### Rôle Docker

Le rôle **docker** vérifie que Docker est installé et accessible.

Commande exécutée :

```bash
docker --version
```

---

### Rôle Kubernetes

Le rôle **kubernetes** vérifie que l'outil **kubectl** est installé.

Commande exécutée :

```bash
kubectl version --client
```

---

### Rôle Terraform

Le rôle **terraform** vérifie que Terraform est correctement installé.

Commande exécutée :

```bash
terraform version
```

---

## Exécution

Depuis la racine du projet :

```bash
ansible-playbook ansible/playbook.yml
```

Le playbook exécute les différents rôles afin de vérifier la disponibilité des outils nécessaires au projet.

---

## Intégration dans le projet

Ansible intervient dans la chaîne DevOps du projet en complément des autres outils.

La chaîne de déploiement est la suivante :

1. GitHub Actions valide le projet et publie l'image Docker dans GitHub Container Registry (GHCR).
2. Terraform prépare l'infrastructure Kubernetes locale.
3. Ansible vérifie que l'environnement de déploiement est correctement configuré.
4. Kubernetes exécute l'application Locatic.
5. Prometheus collecte les métriques.
6. Grafana permet la supervision de l'application.

---

## Limites actuelles

Dans sa version actuelle, le playbook se limite à vérifier la présence des outils nécessaires au projet.

Il ne réalise pas automatiquement :

- l'initialisation de Terraform ;
- l'application de l'infrastructure Terraform ;
- le déploiement des ressources Kubernetes ;
- les mises à jour de l'application.

---

## Évolutions possibles

Le playbook pourra être enrichi afin d'automatiser davantage le déploiement en ajoutant notamment :

- l'exécution de `terraform init` ;
- l'exécution de `terraform apply` ;
- le déploiement automatique des ressources Kubernetes ;
- des vérifications après déploiement ;
- des tâches de maintenance et de mise à jour de l'application.