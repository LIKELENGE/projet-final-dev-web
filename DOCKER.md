# Lancement avec Docker

Prerequis :

- Docker Desktop installe et lance

Commande depuis la racine du projet :

```powershell
docker compose up --build
```

Le service MySQL utilise l'image officielle `mysql:8.4`.
Le fichier `backend/Infrastructure/db.mysql.sql` est monte dans `/docker-entrypoint-initdb.d/01-db.sql`.
Au premier demarrage du volume MySQL, Docker cree la base `market_place` et charge les donnees de test automatiquement.

URLs :

- Frontend : http://localhost:4200
- API : http://localhost:5124
- MySQL : localhost:3306

Comptes de test charges par `backend/Infrastructure/db.mysql.sql` :

| Email | Mot de passe |
| --- | --- |
| moise@example.com | moise123 |
| sarah@example.com | sarah123 |
| david@example.com | david123 |
| grace@example.com | grace123 |

Important : le script SQL n'est execute automatiquement que si le volume MySQL est vide.
Pour repartir avec une base vide puis recharger le script SQL :

```powershell
docker compose down -v
docker compose up --build
```
