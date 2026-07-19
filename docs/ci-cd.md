# Pipeline CI/CD GitHub Actions

## Objectif

Le workflow GitHub Actions automatise la validation du projet Locatic. Il compile l'application, exécute les tests unitaires, construit l'image Docker, réalise un scan de sécurité avec Trivy et publie automatiquement l'image Docker dans GitHub Container Registry (GHCR) lorsque le code est fusionné dans la branche `main`.

Le pipeline s'arrête automatiquement si une étape critique échoue.

---

## Déclencheurs

Le workflow est exécuté automatiquement lors des événements suivants :

- Push sur la branche `main`
- Push sur les branches `feature/**`
- Pull Request vers la branche `main`

---

## Étapes du pipeline

Le pipeline réalise les opérations suivantes :

1. Récupération du dépôt (`actions/checkout@v4`)
2. Installation du SDK .NET 8 (`actions/setup-dotnet@v4`)
3. Restauration des dépendances (`dotnet restore`)
4. Compilation de la solution (`dotnet build`)
5. Exécution des tests unitaires MSTest (`dotnet test`)
6. Publication de l'application (`dotnet publish`)
7. Construction de l'image Docker
8. Analyse de sécurité de l'image Docker avec Trivy
9. Authentification à GitHub Container Registry (GHCR)
10. Publication de l'image Docker avec le tag du commit et le tag `latest` (uniquement sur la branche `main`)

---

## Technologies utilisées

- GitHub Actions
- .NET 8
- MSTest
- Docker
- Trivy
- GitHub Container Registry (GHCR)

---

## Validation automatique

Le pipeline vérifie automatiquement que :

- les dépendances sont restaurées ;
- le projet compile correctement ;
- les tests unitaires réussissent ;
- l'image Docker est construite ;
- aucune vulnérabilité critique ou élevée n'est détectée par Trivy ;
- l'image Docker est publiée dans GHCR.

Si l'une de ces étapes échoue, le pipeline s'arrête automatiquement.

---

## Publication de l'image

Lors d'un `push` sur la branche `main`, le pipeline :

- se connecte à GitHub Container Registry ;
- publie une image Docker identifiée par le hash du commit ;
- publie également l'image avec le tag `latest`.

Les Pull Requests et les branches de développement ne publient pas d'image.

---

## Déploiement

Le pipeline GitHub Actions ne réalise volontairement pas le déploiement Kubernetes.

Après la publication de l'image, le déploiement est effectué localement selon la chaîne DevOps suivante :

1. Terraform prépare l'infrastructure Kubernetes.
2. Ansible orchestre le déploiement.
3. Kubernetes déploie l'application, Nginx et le volume persistant SQLite.
4. Prometheus collecte les métriques.
5. Grafana affiche les tableaux de bord de supervision.

---

## Résultat obtenu

À chaque fusion dans la branche `main` :

- le projet est validé automatiquement ;
- les tests unitaires sont exécutés ;
- l'image Docker est construite ;
- un scan de sécurité est effectué avec Trivy ;
- l'image est publiée dans GitHub Container Registry ;
- le projet est prêt à être déployé sur le cluster Minikube via Terraform et Ansible.