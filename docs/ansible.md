# Documentation Ansible

## Objectif

Le playbook `ansible/playbook.yml` orchestre l’environnement DevOps local en établissant la chaîne entre Terraform, Kubernetes et Docker.

## Contenu réel

Le playbook contient une liste de rôles :

- `docker`
- `kubernetes`
- `terraform`

Chaque rôle est aujourd’hui centré sur la vérification des outils :

- `ansible/roles/docker/tasks/main.yml` vérifie `docker --version`
- `ansible/roles/kubernetes/tasks/main.yml` vérifie `kubectl version --client`
- `ansible/roles/terraform/tasks/main.yml` vérifie `terraform version`

## Ce qui est vérifié

- la présence de Docker
- la présence de kubectl
- la présence de Terraform

## Usage

Lancer le playbook depuis la racine du dépôt :

```bash
ansible-playbook ansible/playbook.yml
```

## Limite actuelle

Le playbook ne déploie pas automatiquement les ressources Kubernetes ou Terraform. Il doit être enrichi avec des tâches qui :

- initialisent et appliquent Terraform
- injectent les variables de configuration
- déploient les manifests Kubernetes ou gèrent les ressources par Terraform

## Recommandation

Pour une automatisation complète, ajouter des tâches Ansible qui réalisent :

- `terraform init`
- `terraform apply`
- `kubectl apply -f kubernetes/`

Cela rendra la chaîne DevOps réellement reproductible sur le poste local.
