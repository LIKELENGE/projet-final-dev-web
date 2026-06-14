using CoreModels = Core.Models;
using InfraModels = Infrastructure.Models;
using InfraMessage = Infrastructure.models.Message;

namespace Infrastructure.Utils;

internal static class ModelMapper
{
    public static CoreModels.Admin ToCore(this InfraModels.Admin admin)
    {
        return new CoreModels.Admin
        {
            Compte = admin.Compte,
            Nom = admin.Nom,
            Prenom = admin.Prenom,
            Niveau = admin.Niveau,
            MotDePasseHash = admin.Mp
        };
    }

    public static InfraModels.Admin ToInfrastructure(this CoreModels.Admin admin)
    {
        return new InfraModels.Admin
        {
            Compte = admin.Compte,
            Nom = admin.Nom,
            Prenom = admin.Prenom,
            Niveau = admin.Niveau,
            Mp = admin.MotDePasseHash
        };
    }

    public static CoreModels.Annonce ToCore(this InfraModels.Annonce annonce)
    {
        return new CoreModels.Annonce
        {
            Id = annonce.IdAnnonce,
            Utilisateur = new CoreModels.Utilisateur { Id = annonce.IdUtilisateur },
            Categorie = new CoreModels.Categorie { Id = annonce.IdCategorie },
            Commune = annonce.IdCommune.HasValue ? new CoreModels.Commune { Id = annonce.IdCommune.Value } : null,
            Nom = annonce.Nom,
            Description = annonce.Description,
            Prix = annonce.Prix,
            DateAjout = annonce.DateAjout,
            DerniereModification = annonce.DerniereModification
        };
    }

    public static InfraModels.Annonce ToInfrastructure(this CoreModels.Annonce annonce)
    {
        return new InfraModels.Annonce
        {
            IdAnnonce = annonce.Id,
            IdUtilisateur = annonce.Utilisateur?.Id ?? 0,
            IdCategorie = annonce.Categorie?.Id ?? 0,
            IdCommune = annonce.Commune?.Id,
            Nom = annonce.Nom,
            Description = annonce.Description,
            Prix = annonce.Prix,
            DateAjout = annonce.DateAjout,
            DerniereModification = annonce.DerniereModification
        };
    }

    public static CoreModels.Categorie ToCore(this InfraModels.Categorie categorie)
    {
        return new CoreModels.Categorie
        {
            Id = categorie.IdCategorie,
            Nom = categorie.NomCategorie
        };
    }

    public static InfraModels.Categorie ToInfrastructure(this CoreModels.Categorie categorie)
    {
        return new InfraModels.Categorie
        {
            IdCategorie = categorie.Id,
            NomCategorie = categorie.Nom
        };
    }

    public static CoreModels.CommentaireAnnonce ToCore(this InfraModels.CommentaireAnnonce commentaire)
    {
        return new CoreModels.CommentaireAnnonce
        {
            Id = commentaire.IdCommentaireAnnonce,
            Annonce = new CoreModels.Annonce { Id = commentaire.IdAnnonce },
            Utilisateur = new CoreModels.Utilisateur { Id = commentaire.IdUtilisateur },
            Contenu = commentaire.ContenuCommentaireAnnonce,
            DateCommentaire = commentaire.DateCommentaire
        };
    }

    public static InfraModels.CommentaireAnnonce ToInfrastructure(this CoreModels.CommentaireAnnonce commentaire)
    {
        return new InfraModels.CommentaireAnnonce
        {
            IdCommentaireAnnonce = commentaire.Id,
            IdAnnonce = commentaire.Annonce?.Id ?? 0,
            IdUtilisateur = commentaire.Utilisateur?.Id ?? 0,
            ContenuCommentaireAnnonce = commentaire.Contenu,
            DateCommentaire = commentaire.DateCommentaire
        };
    }

    public static CoreModels.CommentairePhoto ToCore(this InfraModels.CommentairePhoto commentaire)
    {
        return new CoreModels.CommentairePhoto
        {
            Id = commentaire.IdCommentairePhoto,
            PhotoAnnonce = new CoreModels.PhotoAnnonce { Id = commentaire.IdPhotoAnnonce },
            Utilisateur = new CoreModels.Utilisateur { Id = commentaire.IdUtilisateur },
            Contenu = commentaire.ContenuCommentairePhoto,
            DateCommentaire = commentaire.DateCommentaire
        };
    }

    public static InfraModels.CommentairePhoto ToInfrastructure(this CoreModels.CommentairePhoto commentaire)
    {
        return new InfraModels.CommentairePhoto
        {
            IdCommentairePhoto = commentaire.Id,
            IdPhotoAnnonce = commentaire.PhotoAnnonce?.Id ?? 0,
            IdUtilisateur = commentaire.Utilisateur?.Id ?? 0,
            ContenuCommentairePhoto = commentaire.Contenu,
            DateCommentaire = commentaire.DateCommentaire
        };
    }

    public static CoreModels.Commune ToCore(this InfraModels.Commune commune)
    {
        return new CoreModels.Commune
        {
            Id = commune.IdCommune,
            Nom = commune.NomCommune,
            CodePostal = commune.CodePostal
        };
    }

    public static InfraModels.Commune ToInfrastructure(this CoreModels.Commune commune)
    {
        return new InfraModels.Commune
        {
            IdCommune = commune.Id,
            NomCommune = commune.Nom,
            CodePostal = commune.CodePostal
        };
    }

    public static CoreModels.Conversation ToCore(this InfraModels.Conversation conversation)
    {
        return new CoreModels.Conversation
        {
            Id = conversation.IdConversation,
            Titre = conversation.Titre,
            LienPhoto = conversation.LienPhoto
        };
    }

    public static InfraModels.Conversation ToInfrastructure(this CoreModels.Conversation conversation)
    {
        return new InfraModels.Conversation
        {
            IdConversation = conversation.Id,
            Titre = conversation.Titre,
            LienPhoto = conversation.LienPhoto
        };
    }

    public static CoreModels.DetailStatutUtilisateur ToCore(this InfraModels.DetailStatutUtilisateur detail)
    {
        return new CoreModels.DetailStatutUtilisateur
        {
            Id = detail.IdDetailStatutUtilisateur,
            Utilisateur = new CoreModels.Utilisateur { Id = detail.IdUtilisateur },
            StatutUtilisateur = new CoreModels.StatutUtilisateur { Id = detail.IdStatutUtilisateur },
            DateStatut = detail.DateStatut,
            DelaiStatut = detail.DelaiStatut,
            Illimite = detail.Illimite
        };
    }

    public static InfraModels.DetailStatutUtilisateur ToInfrastructure(this CoreModels.DetailStatutUtilisateur detail)
    {
        return new InfraModels.DetailStatutUtilisateur
        {
            IdDetailStatutUtilisateur = detail.Id,
            IdUtilisateur = detail.Utilisateur?.Id ?? 0,
            IdStatutUtilisateur = detail.StatutUtilisateur?.Id ?? 0,
            DateStatut = detail.DateStatut,
            DelaiStatut = detail.DelaiStatut,
            Illimite = detail.Illimite
        };
    }

    public static CoreModels.Dimension ToCore(this InfraModels.Dimension dimension)
    {
        return new CoreModels.Dimension
        {
            Id = dimension.IdDimension,
            Annonce = new CoreModels.Annonce { Id = dimension.IdAnnonce },
            ProfondeurCm = dimension.ProfondeurCm,
            LongueurCm = dimension.LongueurCm,
            LargeurCm = dimension.LargeurCm,
            PoidsKg = dimension.PoidsKg
        };
    }

    public static InfraModels.Dimension ToInfrastructure(this CoreModels.Dimension dimension)
    {
        return new InfraModels.Dimension
        {
            IdDimension = dimension.Id,
            IdAnnonce = dimension.Annonce?.Id ?? 0,
            ProfondeurCm = dimension.ProfondeurCm,
            LongueurCm = dimension.LongueurCm,
            LargeurCm = dimension.LargeurCm,
            PoidsKg = dimension.PoidsKg
        };
    }

    public static CoreModels.EtatAnnonce ToCore(this InfraModels.EtatAnnonce etat)
    {
        return new CoreModels.EtatAnnonce
        {
            Id = etat.IdEtat,
            Nom = etat.NomEtat
        };
    }

    public static InfraModels.EtatAnnonce ToInfrastructure(this CoreModels.EtatAnnonce etat)
    {
        return new InfraModels.EtatAnnonce
        {
            IdEtat = etat.Id,
            NomEtat = etat.Nom
        };
    }

    public static CoreModels.EtatProduit ToCore(this InfraModels.EtatProduit etat)
    {
        return new CoreModels.EtatProduit
        {
            Id = etat.IdEtatProduit,
            Nom = etat.NomEtat
        };
    }

    public static InfraModels.EtatProduit ToInfrastructure(this CoreModels.EtatProduit etat)
    {
        return new InfraModels.EtatProduit
        {
            IdEtatProduit = etat.Id,
            NomEtat = etat.Nom
        };
    }

    public static CoreModels.FichierMessage ToCore(this InfraModels.FichierMessage fichier)
    {
        return new CoreModels.FichierMessage
        {
            Id = fichier.IdLien,
            Message = new CoreModels.Message { Id = fichier.IdMessage },
            Lien = fichier.Lien
        };
    }

    public static InfraModels.FichierMessage ToInfrastructure(this CoreModels.FichierMessage fichier)
    {
        return new InfraModels.FichierMessage
        {
            IdLien = fichier.Id,
            IdMessage = fichier.Message?.Id ?? 0,
            Lien = fichier.Lien
        };
    }

    public static CoreModels.InteretUtilisateur ToCore(this InfraModels.InteretUtilisateur interet)
    {
        return new CoreModels.InteretUtilisateur
        {
            Id = interet.IdInteretUtilisateur,
            Utilisateur = new CoreModels.Utilisateur { Id = interet.IdUtilisateur },
            Categorie = new CoreModels.Categorie { Id = interet.IdCategorie },
            Commune = interet.IdCommune.HasValue ? new CoreModels.Commune { Id = interet.IdCommune.Value } : null,
            DateConsultation = interet.DateConsultation
        };
    }

    public static InfraModels.InteretUtilisateur ToInfrastructure(this CoreModels.InteretUtilisateur interet)
    {
        return new InfraModels.InteretUtilisateur
        {
            IdInteretUtilisateur = interet.Id,
            IdUtilisateur = interet.Utilisateur?.Id ?? 0,
            IdCategorie = interet.Categorie?.Id ?? 0,
            IdCommune = interet.Commune?.Id,
            DateConsultation = interet.DateConsultation
        };
    }

    public static CoreModels.Message ToCore(this InfraMessage message)
    {
        return new CoreModels.Message
        {
            Id = message.IdMessage,
            Conversation = new CoreModels.Conversation { Id = message.IdConversation },
            Utilisateur = new CoreModels.Utilisateur { Id = message.IdUtilisateur },
            Contenu = message.ContenuMessage,
            DateHeureMessage = message.DateHeureMessage
        };
    }

    public static InfraMessage ToInfrastructure(this CoreModels.Message message)
    {
        return new InfraMessage
        {
            IdMessage = message.Id,
            IdConversation = message.Conversation?.Id ?? 0,
            IdUtilisateur = message.Utilisateur?.Id ?? 0,
            ContenuMessage = message.Contenu,
            DateHeureMessage = message.DateHeureMessage
        };
    }

    public static CoreModels.Moderer ToCore(this InfraModels.Moderer moderation)
    {
        return new CoreModels.Moderer
        {
            Id = moderation.IdModeration,
            Admin = new CoreModels.Admin { Compte = moderation.Compte },
            Annonce = new CoreModels.Annonce { Id = moderation.IdAnnonce },
            EtatAnnonce = new CoreModels.EtatAnnonce { Id = moderation.IdEtat },
            DateStatut = moderation.DateStatut,
            DelaiStatut = moderation.DelaiStatut,
            Illimite = moderation.Illimite
        };
    }

    public static InfraModels.Moderer ToInfrastructure(this CoreModels.Moderer moderation)
    {
        return new InfraModels.Moderer
        {
            IdModeration = moderation.Id,
            Compte = moderation.Admin?.Compte ?? 0,
            IdAnnonce = moderation.Annonce?.Id ?? 0,
            IdEtat = moderation.EtatAnnonce?.Id ?? 0,
            DateStatut = moderation.DateStatut,
            DelaiStatut = moderation.DelaiStatut,
            Illimite = moderation.Illimite
        };
    }

    public static CoreModels.PhotoAnnonce ToCore(this InfraModels.PhotoAnnonce photo)
    {
        return new CoreModels.PhotoAnnonce
        {
            Id = photo.IdPhotoAnnonce,
            Annonce = new CoreModels.Annonce { Id = photo.IdAnnonce },
            Titre = photo.Titre,
            Lien = photo.Lien
        };
    }

    public static InfraModels.PhotoAnnonce ToInfrastructure(this CoreModels.PhotoAnnonce photo)
    {
        return new InfraModels.PhotoAnnonce
        {
            IdPhotoAnnonce = photo.Id,
            IdAnnonce = photo.Annonce?.Id ?? 0,
            Titre = photo.Titre,
            Lien = photo.Lien
        };
    }

    public static CoreModels.Produit ToCore(this InfraModels.Produit produit)
    {
        return new CoreModels.Produit
        {
            Annonce = new CoreModels.Annonce { Id = produit.IdAnnonce },
            EtatProduit = produit.IdEtatProduit.HasValue ? new CoreModels.EtatProduit { Id = produit.IdEtatProduit.Value } : null
        };
    }

    public static InfraModels.Produit ToInfrastructure(this CoreModels.Produit produit)
    {
        return new InfraModels.Produit
        {
            IdAnnonce = produit.Annonce?.Id ?? 0,
            IdEtatProduit = produit.EtatProduit?.Id
        };
    }

    public static CoreModels.Service ToCore(this InfraModels.Service service)
    {
        return new CoreModels.Service
        {
            Annonce = new CoreModels.Annonce { Id = service.IdAnnonce }
        };
    }

    public static InfraModels.Service ToInfrastructure(this CoreModels.Service service)
    {
        return new InfraModels.Service
        {
            IdAnnonce = service.Annonce?.Id ?? 0
        };
    }

    public static CoreModels.Sexe ToCore(this InfraModels.Sexe sexe)
    {
        return new CoreModels.Sexe
        {
            Code = sexe.CodeSexe,
            Nom = sexe.NomSexe
        };
    }

    public static InfraModels.Sexe ToInfrastructure(this CoreModels.Sexe sexe)
    {
        return new InfraModels.Sexe
        {
            CodeSexe = sexe.Code,
            NomSexe = sexe.Nom
        };
    }

    public static CoreModels.StatutUtilisateur ToCore(this InfraModels.StatutUtilisateur statut)
    {
        return new CoreModels.StatutUtilisateur
        {
            Id = statut.IdStatutUtilisateur,
            Nom = statut.NomStatutUtilisateur
        };
    }

    public static InfraModels.StatutUtilisateur ToInfrastructure(this CoreModels.StatutUtilisateur statut)
    {
        return new InfraModels.StatutUtilisateur
        {
            IdStatutUtilisateur = statut.Id,
            NomStatutUtilisateur = statut.Nom
        };
    }

    public static CoreModels.Utilisateur ToCore(this InfraModels.Utilisateur utilisateur)
    {
        return new CoreModels.Utilisateur
        {
            Id = utilisateur.IdUtilisateur,
            Nom = utilisateur.Nom,
            Prenom = utilisateur.Prenom,
            Mail = utilisateur.Mail,
            Tel = utilisateur.Tel,
            PhotoProfil = utilisateur.PhotoProfil,
            MotDePasseHash = utilisateur.Mp,
            DateInscription = utilisateur.DateInscription,
            DateNaissance = utilisateur.DateNaiss,
            Sexe = utilisateur.CodeSexe.HasValue ? new CoreModels.Sexe { Code = utilisateur.CodeSexe.Value } : null,
            Commune = utilisateur.IdCommune.HasValue ? new CoreModels.Commune { Id = utilisateur.IdCommune.Value } : null
        };
    }

    public static InfraModels.Utilisateur ToInfrastructure(this CoreModels.Utilisateur utilisateur)
    {
        return new InfraModels.Utilisateur
        {
            IdUtilisateur = utilisateur.Id,
            Nom = utilisateur.Nom,
            Prenom = utilisateur.Prenom,
            Mail = utilisateur.Mail,
            Tel = utilisateur.Tel,
            PhotoProfil = utilisateur.PhotoProfil,
            Mp = utilisateur.MotDePasseHash,
            DateInscription = utilisateur.DateInscription,
            DateNaiss = utilisateur.DateNaissance,
            CodeSexe = utilisateur.Sexe?.Code,
            IdCommune = utilisateur.Commune?.Id
        };
    }
}
