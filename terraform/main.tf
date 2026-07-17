resource "kubernetes_namespace" "locatic" {
  metadata {
    name = var.namespace
  }
}

resource "kubernetes_persistent_volume_claim" "sqlite_pvc" {
  metadata {
    name      = "sqlite-pvc"
    namespace = kubernetes_namespace.locatic.metadata[0].name
  }

  spec {
    access_modes = ["ReadWriteOnce"]

    resources {
      requests = {
        storage = "1Gi"
      }
    }
  }
}

resource "kubernetes_deployment" "locatic" {
  metadata {
    name      = "locatic-web"
    namespace = kubernetes_namespace.locatic.metadata[0].name

    labels = {
      app = "locatic-web"
    }
  }

  spec {
    replicas = 1

    selector {
      match_labels = {
        app = "locatic-web"
      }
    }

    template {
      metadata {
        labels = {
          app = "locatic-web"
        }
      }

      spec {
        container {
          name  = "locatic-web"
          image = var.image

          image_pull_policy = "Never"

          port {
            container_port = 8080
          }

          env {
            name  = "ASPNETCORE_URLS"
            value = "http://+:8080"
          }

          volume_mount {
            name       = "sqlite-storage"
            mount_path = "/data"
          }

          readiness_probe {
            http_get {
              path = "/"
              port = 8080
            }

            initial_delay_seconds = 10
            period_seconds        = 10
          }

          liveness_probe {
            http_get {
              path = "/"
              port = 8080
            }

            initial_delay_seconds = 30
            period_seconds        = 20
          }
        }

        volume {
          name = "sqlite-storage"

          persistent_volume_claim {
            claim_name = kubernetes_persistent_volume_claim.sqlite_pvc.metadata[0].name
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "locatic" {
  metadata {
    name      = "locatic-web"
    namespace = kubernetes_namespace.locatic.metadata[0].name
  }

  spec {
    selector = {
      app = "locatic-web"
    }

    port {
      port        = 80
      target_port = 8080
    }

    type = "NodePort"
  }
}