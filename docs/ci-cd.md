# Pipeline CI/CD

## Règles de branche

Le dépôt doit suivre une logique de travail par Pull Request. La branche main doit être protégée et ne doit pas accepter de push directs.

Le flux recommandé est le suivant :

1. créer une branche de travail dédiée
2. apporter les modifications nécessaires
3. ouvrir une Pull Request
4. laisser s’exécuter les checks CI
5. valider la PR puis fusionner vers main

## Pull Requests et validation

La Pull Request constitue la porte d’entrée unique vers la branche main. Elle permet d’ouvrir un espace de revue, de discuter des changements et d’assurer que les contrôles automatisés sont bien passés avant toute intégration.

Les checks obligatoires doivent couvrir au minimum :

- build du projet
- exécution des tests
- vérification de qualité ou de conformité simple
- build de l’image Docker
- validation de la publication potentielle de l’image

## Jobs du pipeline GitHub Actions

Le workflow CI doit être organisé en jobs distincts afin de garder une lecture claire du pipeline. Un exemple logique de structure est :

- checkout : récupération du code
- restore : restauration des packages
- build : compilation du projet
- test : exécution des tests
- docker-build : construction de l’image
- security-scan : vérification de sécurité ou de conformité

Chaque job doit dépendre explicitement des étapes précédentes afin d’éviter les échecs silencieux.

## Publication de l’image Docker

La publication de l’image Docker doit être faite uniquement depuis la branche main, ou à partir d’un déclenchement adapté à la branche de production. L’objectif est de garantir que l’image publiée correspond à un état validé par la chaîne CI.

Les valeurs de tag doivent être explicites, par exemple :

- latest pour la version de référence
- un tag basé sur le commit ou sur la version

## Limites du pipeline GitHub

Le pipeline GitHub Actions ne doit pas être utilisé pour déployer directement sur minikube. Cette étape dépend d’un environnement local et ne peut pas être exécutée de manière fiable sur les runners GitHub.

Le pipeline doit donc s’arrêter après les validations et la publication de l’image. Le déploiement final est ensuite lancé localement avec Terraform puis Ansible.
