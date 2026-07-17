# Pipeline CI/CD

Le pipeline GitHub Actions est exécuté :

- sur chaque Pull Request
- après chaque merge sur main

Le pipeline réalise :

1. Checkout du dépôt
2. Installation du SDK .NET
3. Restauration des dépendances
4. Compilation
5. Exécution des tests
6. Build Docker
7. Scan de sécurité
8. Publication de l'image Docker

Le pipeline ne déploie pas directement sur Minikube.

Le déploiement est réalisé localement à l'aide de Terraform et Ansible.