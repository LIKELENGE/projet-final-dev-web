# Marketplace Angular / .NET

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
- Chat/conversations
- Moderation et validation d'annonces
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

## Lancement rapide avec Docker

Prerequis :

- Docker Desktop installe et lance

Depuis la racine du projet :

```powershell
docker compose up --build
```

Le service Docker MySQL utilise l'image officielle `mysql:8.4`.
Le fichier `backend/Infrastructure/db.mysql.sql` est monte dans `/docker-entrypoint-initdb.d/01-db.sql`.
Au premier lancement du volume MySQL, les tables et les donnees de test sont chargees automatiquement.

URLs :

- Frontend : http://localhost:4200
- API : http://localhost:5124
- MySQL : localhost:3306

Important : le script SQL n'est execute automatiquement que si le volume MySQL est vide.
Pour supprimer la base Docker et la recharger depuis le script SQL :

```powershell
docker compose down -v
docker compose up --build
```

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

Valeur par defaut :

```json
"DefaultConnection": "server=localhost;port=3306;database=market_place;user=root;"
```

Si votre utilisateur MySQL a un mot de passe, ajoutez-le dans la chaine de connexion :

```json
"DefaultConnection": "server=localhost;port=3306;database=market_place;user=root;password=votre_mot_de_passe;"
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

## Images des articles

Les images sont envoyees au backend depuis le formulaire "Nouvel article".
Le backend stocke les fichiers dans :

```text
backend/Api/wwwroot/images/annonces
```

La base de donnees ne stocke pas les octets de l'image. Elle stocke seulement le chemin public, par exemple :

```text
/images/annonces/nom-du-fichier.jpg
```

Dans Docker, le volume `api-images` garde ces fichiers entre deux redemarrages.

Il reste possible d'indiquer une URL externe comme `https://exemple.com/image.jpg`.
Eviter les chemins absolus Windows comme `C:\Users\...\photo.jpg`, car ils ne fonctionneront pas sur une autre machine, sur GitHub ou dans Docker.

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

Les routes de creation, modification, suppression et "mes annonces" utilisent un token JWT :

```http
Authorization: Bearer <token>
```

## Preparation du ZIP de remise

Inclure :

- `backend`
- `frontend`
- `README.md`
- `DOCKER.md`
- `docker-compose.yml`

Exclure :

- `node_modules`
- `bin`
- `obj`
- `dist`
- `.angular`
- `.vs`
