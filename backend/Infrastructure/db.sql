PRAGMA foreign_keys = OFF;

DROP TABLE IF EXISTS moderer;
DROP TABLE IF EXISTS commentaire_photo;
DROP TABLE IF EXISTS commentaire_annonce;
DROP TABLE IF EXISTS photo_annonce;
DROP TABLE IF EXISTS dimension;
DROP TABLE IF EXISTS service;
DROP TABLE IF EXISTS produit;
DROP TABLE IF EXISTS annonce;
DROP TABLE IF EXISTS fichier_message;
DROP TABLE IF EXISTS message;
DROP TABLE IF EXISTS conversation;
DROP TABLE IF EXISTS interet_utilisateur;
DROP TABLE IF EXISTS detail_statut_utilisateur;
DROP TABLE IF EXISTS admin;
DROP TABLE IF EXISTS utilisateur;
DROP TABLE IF EXISTS etat_produit;
DROP TABLE IF EXISTS etat_annonce;
DROP TABLE IF EXISTS statut_utilisateur;
DROP TABLE IF EXISTS categorie;
DROP TABLE IF EXISTS commune;
DROP TABLE IF EXISTS sexe;

PRAGMA foreign_keys = ON;

-- =========================
-- TABLES DE REFERENCE
-- =========================

CREATE TABLE sexe (
    code_sexe INTEGER PRIMARY KEY AUTOINCREMENT,
    nom_sexe TEXT NOT NULL
);

CREATE TABLE commune (
    id_commune INTEGER PRIMARY KEY AUTOINCREMENT,
    nom_commune TEXT NOT NULL,
    code_postal TEXT
);

CREATE TABLE categorie (
    id_categorie INTEGER PRIMARY KEY AUTOINCREMENT,
    nom_categorie TEXT NOT NULL
);

CREATE TABLE statut_utilisateur (
    id_statut_utilisateur INTEGER PRIMARY KEY AUTOINCREMENT,
    nom_statut_utilisateur TEXT NOT NULL
);

CREATE TABLE etat_annonce (
    id_etat INTEGER PRIMARY KEY AUTOINCREMENT,
    nom_etat TEXT NOT NULL
);

CREATE TABLE etat_produit (
    id_etat_produit INTEGER PRIMARY KEY AUTOINCREMENT,
    nom_etat TEXT NOT NULL
);

-- =========================
-- UTILISATEURS ET ADMINS
-- =========================

CREATE TABLE utilisateur (
    id_utilisateur INTEGER PRIMARY KEY AUTOINCREMENT,
    nom TEXT NOT NULL,
    prenom TEXT NOT NULL,
    mail TEXT NOT NULL UNIQUE,
    tel TEXT,
    photo_profil TEXT,
    mp TEXT NOT NULL,
    date_inscription TEXT DEFAULT CURRENT_TIMESTAMP,
    date_naiss TEXT,
    code_sexe INTEGER,
    id_commune INTEGER,

    FOREIGN KEY (code_sexe) REFERENCES sexe(code_sexe),
    FOREIGN KEY (id_commune) REFERENCES commune(id_commune)
);

CREATE TABLE admin (
    compte INTEGER PRIMARY KEY AUTOINCREMENT,
    nom TEXT NOT NULL,
    prenom TEXT NOT NULL,
    niveau TEXT,
    mp TEXT NOT NULL
);

CREATE TABLE detail_statut_utilisateur (
    id_detail_statut_utilisateur INTEGER PRIMARY KEY AUTOINCREMENT,
    id_utilisateur INTEGER NOT NULL,
    id_statut_utilisateur INTEGER NOT NULL,
    date_statut TEXT DEFAULT CURRENT_TIMESTAMP,
    delai_statut TEXT,
    illimite INTEGER DEFAULT 0,

    FOREIGN KEY (id_utilisateur) REFERENCES utilisateur(id_utilisateur) ON DELETE CASCADE,
    FOREIGN KEY (id_statut_utilisateur) REFERENCES statut_utilisateur(id_statut_utilisateur)
);

CREATE TABLE interet_utilisateur (
    id_interet_utilisateur INTEGER PRIMARY KEY AUTOINCREMENT,
    id_utilisateur INTEGER NOT NULL,
    id_categorie INTEGER NOT NULL,
    id_commune INTEGER,
    date_consultation TEXT DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (id_utilisateur) REFERENCES utilisateur(id_utilisateur) ON DELETE CASCADE,
    FOREIGN KEY (id_categorie) REFERENCES categorie(id_categorie),
    FOREIGN KEY (id_commune) REFERENCES commune(id_commune)
);

-- =========================
-- CONVERSATIONS ET MESSAGES
-- =========================

CREATE TABLE conversation (
    id_conversation INTEGER PRIMARY KEY AUTOINCREMENT,
    titre TEXT,
    lien_photo TEXT
);

CREATE TABLE message (
    id_message INTEGER PRIMARY KEY AUTOINCREMENT,
    id_conversation INTEGER NOT NULL,
    id_utilisateur INTEGER NOT NULL,
    contenu_message TEXT NOT NULL,
    date_heure_message TEXT DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (id_conversation) REFERENCES conversation(id_conversation) ON DELETE CASCADE,
    FOREIGN KEY (id_utilisateur) REFERENCES utilisateur(id_utilisateur) ON DELETE CASCADE
);

CREATE TABLE fichier_message (
    id_lien INTEGER PRIMARY KEY AUTOINCREMENT,
    id_message INTEGER NOT NULL,
    lien TEXT NOT NULL,

    FOREIGN KEY (id_message) REFERENCES message(id_message) ON DELETE CASCADE
);

-- =========================
-- ANNONCES
-- =========================

CREATE TABLE annonce (
    id_annonce INTEGER PRIMARY KEY AUTOINCREMENT,
    id_utilisateur INTEGER NOT NULL,
    id_categorie INTEGER NOT NULL,
    id_commune INTEGER,
    nom TEXT NOT NULL,
    description TEXT,
    prix REAL DEFAULT 0,
    date_ajout TEXT DEFAULT CURRENT_TIMESTAMP,
    derniere_modification TEXT DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (id_utilisateur) REFERENCES utilisateur(id_utilisateur) ON DELETE CASCADE,
    FOREIGN KEY (id_categorie) REFERENCES categorie(id_categorie),
    FOREIGN KEY (id_commune) REFERENCES commune(id_commune)
);

CREATE TRIGGER trg_annonce_derniere_modification
AFTER UPDATE OF id_utilisateur, id_categorie, id_commune, nom, description, prix
ON annonce
FOR EACH ROW
BEGIN
    UPDATE annonce
    SET derniere_modification = CURRENT_TIMESTAMP
    WHERE id_annonce = OLD.id_annonce;
END;

CREATE TABLE produit (
    id_annonce INTEGER PRIMARY KEY,
    id_etat_produit INTEGER,

    FOREIGN KEY (id_annonce) REFERENCES annonce(id_annonce) ON DELETE CASCADE,
    FOREIGN KEY (id_etat_produit) REFERENCES etat_produit(id_etat_produit)
);

CREATE TABLE service (
    id_annonce INTEGER PRIMARY KEY,

    FOREIGN KEY (id_annonce) REFERENCES annonce(id_annonce) ON DELETE CASCADE
);

CREATE TABLE dimension (
    id_dimension INTEGER PRIMARY KEY AUTOINCREMENT,
    id_annonce INTEGER NOT NULL UNIQUE,
    profondeur_cm REAL,
    longueur_cm REAL,
    largeur_cm REAL,
    poids_kg REAL,

    FOREIGN KEY (id_annonce) REFERENCES produit(id_annonce) ON DELETE CASCADE
);

CREATE TABLE photo_annonce (
    id_photo_annonce INTEGER PRIMARY KEY AUTOINCREMENT,
    id_annonce INTEGER NOT NULL,
    titre TEXT,
    lien TEXT NOT NULL,

    FOREIGN KEY (id_annonce) REFERENCES annonce(id_annonce) ON DELETE CASCADE
);

CREATE TABLE commentaire_annonce (
    id_commentaire_annonce INTEGER PRIMARY KEY AUTOINCREMENT,
    id_annonce INTEGER NOT NULL,
    id_utilisateur INTEGER NOT NULL,
    contenu_commentaire_annonce TEXT NOT NULL,
    date_commentaire TEXT DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (id_annonce) REFERENCES annonce(id_annonce) ON DELETE CASCADE,
    FOREIGN KEY (id_utilisateur) REFERENCES utilisateur(id_utilisateur) ON DELETE CASCADE
);

CREATE TABLE commentaire_photo (
    id_commentaire_photo INTEGER PRIMARY KEY AUTOINCREMENT,
    id_photo_annonce INTEGER NOT NULL,
    id_utilisateur INTEGER NOT NULL,
    contenu_commentaire_photo TEXT NOT NULL,
    date_commentaire TEXT DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (id_photo_annonce) REFERENCES photo_annonce(id_photo_annonce) ON DELETE CASCADE,
    FOREIGN KEY (id_utilisateur) REFERENCES utilisateur(id_utilisateur) ON DELETE CASCADE
);

CREATE TABLE moderer (
    id_moderation INTEGER PRIMARY KEY AUTOINCREMENT,
    compte INTEGER NOT NULL,
    id_annonce INTEGER NOT NULL,
    id_etat INTEGER NOT NULL,
    date_statut TEXT DEFAULT CURRENT_TIMESTAMP,
    delai_statut TEXT,
    illimite INTEGER DEFAULT 0,

    FOREIGN KEY (compte) REFERENCES admin(compte),
    FOREIGN KEY (id_annonce) REFERENCES annonce(id_annonce) ON DELETE CASCADE,
    FOREIGN KEY (id_etat) REFERENCES etat_annonce(id_etat)
);

-- =========================
-- JEU DE DONNEES
-- =========================

INSERT INTO sexe (code_sexe, nom_sexe) VALUES
(1, 'Masculin'),
(2, 'Féminin'),
(3, 'Autre');

INSERT INTO commune (id_commune, nom_commune, code_postal) VALUES
(1, 'Kinshasa', '1000'),
(2, 'Lubumbashi', '2000'),
(3, 'Goma', '3000'),
(4, 'Matadi', '4000');

INSERT INTO categorie (id_categorie, nom_categorie) VALUES
(1, 'Téléphones'),
(2, 'Ordinateurs'),
(3, 'Meubles'),
(4, 'Vêtements'),
(5, 'Services'),
(6, 'Électroménager');

INSERT INTO statut_utilisateur 
(id_statut_utilisateur, nom_statut_utilisateur) VALUES
(1, 'Actif'),
(2, 'Suspendu'),
(3, 'Banni'),
(4, 'Vérifié');

INSERT INTO etat_annonce (id_etat, nom_etat) VALUES
(1, 'En attente'),
(2, 'Validée'),
(3, 'Refusée'),
(4, 'Signalée'),
(5, 'Archivée');

INSERT INTO etat_produit (id_etat_produit, nom_etat) VALUES
(1, 'Neuf'),
(2, 'Très bon état'),
(3, 'Bon état'),
(4, 'Usé');

INSERT INTO utilisateur 
(id_utilisateur, nom, prenom, mail, tel, photo_profil, mp, date_inscription, date_naiss, code_sexe, id_commune)
VALUES
(1, 'Likelenge', 'Moïse', 'moise@example.com', '+243810000001', 'profil_moise.jpg', '08f91946ad0913a56f2f7115a0b964ab0ed66aa99cecfa18fe957195cecfbdd2', '2025-01-10 08:30:00', '1998-05-12', 1, 1),
(2, 'Mbuyi', 'Sarah', 'sarah@example.com', '+243810000002', 'profil_sarah.jpg', 'dd7f6bfb6e0d8bcd754e97cae4975c07996f00508f8346d649c5814e19c3f9b9', '2025-01-12 10:15:00', '2000-03-20', 2, 2),
(3, 'Kabongo', 'David', 'david@example.com', '+243810000003', 'profil_david.jpg', '0f14089313b20c1723ec1d660b0aaa4f473cf5b321cd370f2d48b7bcf9a7b234', '2025-02-01 14:00:00', '1995-11-02', 1, 3),
(4, 'Lukusa', 'Grace', 'grace@example.com', '+243810000004', 'profil_grace.jpg', 'd53ecaf3fc441a2086dc0cdc26692699dc8e12dd80f329414d605f1cdc4da3df', '2025-02-05 16:40:00', '1999-08-18', 2, 1);

INSERT INTO admin (compte, nom, prenom, niveau, mp) VALUES
(1, 'Admin', 'Principal', 'super_admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9'),
(2, 'Modérateur', 'Junior', 'moderateur', '5122d56860571772f8ccc768cfe724ad22d4176bb4b7fc1c624ae628622221f2');

INSERT INTO detail_statut_utilisateur
(id_detail_statut_utilisateur, id_utilisateur, id_statut_utilisateur, date_statut, delai_statut, illimite)
VALUES
(1, 1, 4, '2025-01-10 09:00:00', NULL, 1),
(2, 2, 1, '2025-01-12 10:30:00', NULL, 1),
(3, 3, 1, '2025-02-01 15:00:00', NULL, 1),
(4, 4, 1, '2025-02-05 17:00:00', NULL, 1);

INSERT INTO interet_utilisateur 
(id_interet_utilisateur, id_utilisateur, id_categorie, id_commune, date_consultation)
VALUES
(1, 1, 1, 1, '2025-03-01 09:15:00'),
(2, 1, 2, 1, '2025-03-02 11:00:00'),
(3, 2, 3, 2, '2025-03-03 13:25:00'),
(4, 3, 5, 3, '2025-03-04 18:10:00'),
(5, 4, 4, 1, '2025-03-05 20:05:00');

INSERT INTO conversation (id_conversation, titre, lien_photo) VALUES
(1, 'Discussion iPhone 12', 'conv_iphone.jpg'),
(2, 'Service de réparation PC', 'conv_service.jpg');

INSERT INTO message 
(id_message, id_conversation, id_utilisateur, contenu_message, date_heure_message)
VALUES
(1, 1, 2, 'Bonjour, le téléphone est-il encore disponible ?', '2025-03-10 09:00:00'),
(2, 1, 1, 'Oui, il est encore disponible.', '2025-03-10 09:05:00'),
(3, 2, 3, 'Bonjour, pouvez-vous réparer un ordinateur HP ?', '2025-03-11 14:20:00'),
(4, 2, 4, 'Oui, envoyez-moi le modèle exact.', '2025-03-11 14:25:00');

INSERT INTO fichier_message (id_lien, id_message, lien) VALUES
(1, 1, 'capture_iphone.png'),
(2, 3, 'photo_pc_hp.jpg');

INSERT INTO annonce 
(id_annonce, id_utilisateur, id_categorie, id_commune, nom, description, prix, date_ajout)
VALUES
(1, 1, 1, 1, 'iPhone 12 128GB', 'iPhone 12 en très bon état, batterie correcte.', 450.00, '2025-03-01 08:00:00'),
(2, 2, 3, 2, 'Table en bois', 'Table familiale en bois massif.', 120.00, '2025-03-02 10:00:00'),
(3, 4, 5, 1, 'Réparation ordinateur', 'Service de réparation et installation logiciels.', 25.00, '2025-03-03 12:00:00'),
(4, 3, 2, 3, 'Laptop Dell Latitude', 'Ordinateur portable Dell Core i5.', 380.00, '2025-03-04 15:00:00');

INSERT INTO produit (id_annonce, id_etat_produit) VALUES
(1, 2),
(2, 3),
(4, 2);

INSERT INTO service (id_annonce) VALUES
(3);

INSERT INTO dimension 
(id_dimension, id_annonce, profondeur_cm, longueur_cm, largeur_cm, poids_kg)
VALUES
(1, 1, 0.80, 14.67, 7.15, 0.16),
(2, 2, 75.00, 160.00, 90.00, 25.00),
(3, 4, 2.00, 32.00, 22.00, 1.80);

INSERT INTO photo_annonce 
(id_photo_annonce, id_annonce, titre, lien)
VALUES
(1, 1, 'Face avant iPhone', 'iphone12_face.jpg'),
(2, 1, 'Dos iPhone', 'iphone12_dos.jpg'),
(3, 2, 'Table en bois', 'table_bois.jpg'),
(4, 3, 'Réparation PC', 'reparation_pc.jpg'),
(5, 4, 'Dell Latitude', 'dell_latitude.jpg');

INSERT INTO commentaire_annonce 
(id_commentaire_annonce, id_annonce, id_utilisateur, contenu_commentaire_annonce, date_commentaire)
VALUES
(1, 1, 2, 'Le prix est-il négociable ?', '2025-03-06 09:00:00'),
(2, 1, 1, 'Oui, légèrement négociable.', '2025-03-06 09:10:00'),
(3, 3, 3, 'Est-ce que vous faites aussi la récupération de données ?', '2025-03-07 11:30:00'),
(4, 4, 4, 'La batterie tient combien de temps ?', '2025-03-08 16:45:00');

INSERT INTO commentaire_photo 
(id_commentaire_photo, id_photo_annonce, id_utilisateur, contenu_commentaire_photo, date_commentaire)
VALUES
(1, 1, 2, 'L’écran semble propre.', '2025-03-06 09:05:00'),
(2, 3, 1, 'La table est-elle démontable ?', '2025-03-07 10:15:00'),
(3, 5, 4, 'Pouvez-vous ajouter une photo du clavier ?', '2025-03-08 17:00:00');

INSERT INTO moderer 
(id_moderation, compte, id_annonce, id_etat, date_statut, delai_statut, illimite)
VALUES
(1, 1, 1, 2, '2025-03-01 09:00:00', NULL, 1),
(2, 1, 2, 2, '2025-03-02 11:00:00', NULL, 1),
(3, 2, 3, 2, '2025-03-03 13:00:00', NULL, 1),
(4, 2, 4, 1, '2025-03-04 16:00:00', NULL, 0);

-- =========================
-- TESTS RAPIDES
-- =========================

SELECT 
    a.id_annonce,
    a.nom AS annonce,
    c.nom_categorie,
    u.prenom,
    u.nom,
    a.prix,
    co.nom_commune
FROM annonce a
JOIN utilisateur u ON a.id_utilisateur = u.id_utilisateur
JOIN categorie c ON a.id_categorie = c.id_categorie
LEFT JOIN commune co ON a.id_commune = co.id_commune;

SELECT 
    m.id_message,
    conv.titre,
    u.prenom,
    u.nom,
    m.contenu_message,
    m.date_heure_message
FROM message m
JOIN conversation conv ON m.id_conversation = conv.id_conversation
JOIN utilisateur u ON m.id_utilisateur = u.id_utilisateur
ORDER BY m.date_heure_message;
