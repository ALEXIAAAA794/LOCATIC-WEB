# Pipeline CI/CD GitHub Actions

## Objectif

Le workflow `.github/workflows/ci.yml` valide le dépôt et construit l’image Docker Locatic. Il ne publie pas encore l’image dans une registry distante.

## Déclencheurs

Le pipeline s’exécute sur :

- `push` sur `main`
- `push` sur `feature/**`
- `pull_request` ciblant `main`

## Étapes du job `build`

1. `Checkout` : récupération du code avec `actions/checkout@v4`
2. `Setup .NET 8` : installation du SDK avec `actions/setup-dotnet@v4` et `dotnet-version: '8.0.x'`
3. `Restore` : `dotnet restore Locatic.sln`
4. `Build` : `dotnet build Locatic.sln --configuration Release --no-restore`
5. `Publish` : `dotnet publish Locatic.Web/Locatic.Web.csproj -c Release -o publish --no-build`
6. `Build Docker image` : `docker build -t locatic-web:latest .`

## Spécificités réelles

- le workflow utilise la solution `Locatic.sln`
- le projet web publié est `Locatic.Web/Locatic.Web.csproj`
- l’image Docker construite est `locatic-web:latest`

## Améliorations utiles

Pour rendre ce pipeline complet, il faudrait ajouter :

- un test de l’application ou des tests unitaires
- la publication de l’image vers une registry Docker
- des validations de sécurité ou de scan additionnel

## Limite actuelle

Le workflow actuel ne déploie pas vers Kubernetes ni minikube. Le déploiement est prévu localement via `terraform/` et `ansible/`.
