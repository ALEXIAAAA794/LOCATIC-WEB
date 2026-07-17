# Pipeline CI/CD

## Objectif

Le pipeline GitHub Actions valide le dépôt et prépare l’image Docker. Il ne déploie pas directement sur minikube.

## Règles de branche

La branche main doit être protégée et les changements doivent passer par une Pull Request.

## Étapes attendues

Le pipeline doit inclure :

- checkout du code
- installation du SDK .NET
- restauration des dépendances
- compilation
- exécution des tests
- build de l’image Docker
- scan ou validation simple

## Publication de l’image

La publication de l’image Docker doit être effectuée uniquement depuis main, avec des secrets stockés dans GitHub.

## Limites du pipeline

Le pipeline GitHub Actions valide le code et l’image. Le déploiement final est exécuté localement via Terraform et Ansible.
