# Lancement avec Docker

Prerequis :

- Docker Desktop installe et lance

Commande depuis la racine du projet :

```powershell
docker compose up --build
```

L'image MySQL du projet embarque deja le script `backend/Infrastructure/db.mysql.sql`.
Au premier demarrage, Docker cree la base `market_place` et charge les donnees de test automatiquement.

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

Pour repartir avec une base vide puis recharger le script SQL :

```powershell
docker compose down -v
docker compose up --build
```
