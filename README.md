# Marketplace Angular / .NET

## Lancement rapide avec Docker

Prerequis :

- Docker Desktop installe et demarre
- le port `3306` disponible sur la machine

Depuis la racine du projet :

```powershell
docker compose up --build
```

Pour lancer les conteneurs en arriere-plan :

```powershell
docker compose up --build -d
```

Le service Docker MySQL utilise l'image officielle `mysql:8.4`.
Le fichier `backend/Infrastructure/db.mysql.sql` est monte dans `/docker-entrypoint-initdb.d/01-db.sql`.
Au premier lancement du volume MySQL, les tables et les donnees de test sont chargees automatiquement.

URLs :

- Frontend : http://localhost:4200
- API : http://localhost:5124
- MySQL : localhost:3306

Connexion MySQL depuis un outil externe :

| Parametre | Valeur |
| --- | --- |
| Hote | `localhost` |
| Port | `3306` |
| Base | `market_place` |
| Utilisateur | `root` |
| Mot de passe | `root` |



Pour arreter et supprimer les conteneurs sans supprimer les donnees MySQL :

```powershell
docker compose down
```

Important : le script SQL n'est execute automatiquement que si le volume MySQL est vide.
Pour supprimer la base Docker et la recharger depuis le script SQL :

```powershell
docker compose down -v
docker compose up --build
```

### Erreur : port 3306 deja utilise

Cette erreur signifie qu'un autre serveur MySQL utilise deja le port `3306` sur Windows.
Il faut arreter le service MySQL local avant de relancer Docker :

```powershell
docker compose up --build
```

L'API utilise le nom de service Docker `mysql` et le port interne `3306` pour communiquer avec la base.




## But du logiciel

Marketplace est une application web de petites annonces. Elle permet a un visiteur de consulter et rechercher les publications disponibles. Un utilisateur authentifie peut creer ses propres annonces, consulter la section "Mes articles", modifier ou supprimer uniquement ses propres publications. Un administrateur peut gerer les categories et valider les annonces selon les cas d'utilisation prevus.

Le projet sert aussi a demontrer une architecture Angular moderne avec services, formulaires, routage et nouvelle syntaxe de templates, ainsi qu'un backend ASP.NET Core organise en Clean Architecture avec Dapper pour l'acces aux donnees.

## Fonctionnalites principales

- Inscription utilisateur
- Connexion avec JWT
- Consultation de toutes les annonces
- Recherche par mot-cle et categorie
- Creation d'annonce par un utilisateur authentifie
- Consultation des annonces personnelles dans "Mes articles"
- Modification et suppression reservees a l'auteur de l'annonce
- Gestion des categories
- Use cases backend pour les conversations
- Use case backend pour la moderation et la validation d'annonces
- Listes de choix avec libelles : categories, communes, sexes

## Architecture

Le backend respecte une separation en couches :

- `backend/Api` : endpoints HTTP, configuration, JWT, middleware
- `backend/Core` : modeles metier, interfaces de gateways, use cases
- `backend/Infrastructure` : repositories, gateways, Dapper, scripts SQL

Le frontend Angular est dans `frontend` et utilise :

- composants Angular
- services Angular pour l'etat et les appels HTTP
- formulaires reactifs
- routage Angular
- syntaxe moderne `@if`, `@for`, `@empty`

## Technologies

- .NET : 10.0
- ASP.NET Core
- Dapper
- MySQL : 8.x
- Angular : 21.x
- Node.js : 24.x
- Bootstrap : 5.x
- Docker / Docker Compose optionnel

Entity Framework n'est pas utilise.

## Comptes de test

Ces comptes sont charges par `backend/Infrastructure/db.mysql.sql`.

| Email | Mot de passe |
| --- | --- |
| moise@example.com | moise123 |
| sarah@example.com | sarah123 |
| david@example.com | david123 |
| grace@example.com | grace123 |

Les mots de passe sont stockes en SHA-256. Dans le script MySQL, ils sont generes avec `SHA2('motdepasse', 256)`.


## Lancement manuel

### 1. Base de donnees

Installer MySQL puis executer le script :

```text
backend/Infrastructure/db.mysql.sql
```

La base creee s'appelle :

```text
market_place
```

La chaine de connexion locale se trouve dans :

```text
backend/Api/appsettings.json
```



### 2. Backend

Depuis la racine du projet :

```powershell
cd backend
dotnet restore
dotnet build backend.slnx
dotnet run --project Api\Api.csproj
```

L'API demarre sur :

```text
http://localhost:5124
```

### 3. Frontend

Dans un deuxieme terminal :

```powershell
cd frontend
npm install
npm start
```

Si PowerShell bloque `npm`, utiliser :

```powershell
npm.cmd install
npm.cmd start
```

Le frontend demarre sur :

```text
http://localhost:4200
```

## Utilisation

1. Ouvrir http://localhost:4200.
2. Consulter les publications dans la page Articles.
3. Creer un compte ou utiliser un compte de test.
4. Se connecter.
5. Creer une annonce depuis la section "Nouvelle annonce".
6. Retrouver ses annonces dans "Mes articles".
7. Modifier ou supprimer uniquement ses propres annonces.




## API principale

- `POST /utilisateurs/inscription`
- `POST /utilisateurs/connexion`
- `GET /annonces`
- `GET /annonces/{annonceId}`
- `GET /annonces/mes-annonces`
- `POST /annonces`
- `PUT /annonces/{annonceId}`
- `DELETE /annonces/{annonceId}`
- `GET /choix/categories`
- `GET /choix/communes`
- `GET /choix/sexes`
- `POST /conversations`
- `GET /conversations/{conversationId}`
- `GET /conversations/{conversationId}/messages`
- `POST /conversations/{conversationId}/messages`
- `POST /moderations/annonces/validation`


