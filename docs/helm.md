# Helm (bonus)

## Statut

Helm n’est pas obligatoire pour respecter le mini-projet, mais il peut être utilisé comme bonus pour rendre le déploiement Kubernetes plus propre et plus paramétrable.

## Structure du chart

Si Helm est utilisé, le chart peut être organisé de la façon suivante :

- templates/ : manifests Kubernetes
- values.yaml : valeurs configurables
- Chart.yaml : métadonnées du chart

## Valeurs configurables

Les paramètres typiques à exposer sont :

- image.repository
- image.tag
- replicaCount
- service.type
- persistence.enabled
- persistence.size

## Procédure de release

La release Helm peut être installée ou mise à jour avec :

```bash
helm upgrade --install locatic ./helm
```

Cette approche permet de gérer plus proprement les versions de déploiement et de simplifier les mises à jour.
