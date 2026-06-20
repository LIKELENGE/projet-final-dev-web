using Core.IGateways;
using Dapper;
using Infrastructure.Gateways;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        services.AddTransient<IUtilisateurRepository, UtilisateurRepository>();
        services.AddTransient<IAnnonceRepository, AnnonceRepository>();
        services.AddTransient<IProduitRepository, ProduitRepository>();
        services.AddTransient<IServiceRepository, ServiceRepository>();
        services.AddTransient<ICategorieRepository, CategorieRepository>();
        services.AddTransient<ICommuneRepository, CommuneRepository>();
        services.AddTransient<ISexeRepository, SexeRepository>();
        services.AddTransient<IConversationRepository, ConversationRepository>();
        services.AddTransient<IMessageRepository, MessageRepository>();
        services.AddTransient<IPhotoAnnonceRepository, PhotoAnnonceRepository>();
        services.AddTransient<ICommentaireAnnonceRepository, CommentaireAnnonceRepository>();
        services.AddTransient<ICommentairePhotoRepository, CommentairePhotoRepository>();
        services.AddTransient<IAdminRepository, AdminRepository>();
        services.AddTransient<IModererRepository, ModererRepository>();
        services.AddTransient<IEtatAnnonceRepository, EtatAnnonceRepository>();
        services.AddTransient<IEtatProduitRepository, EtatProduitRepository>();
        services.AddTransient<IStatutUtilisateurRepository, StatutUtilisateurRepository>();
        services.AddTransient<IDetailStatutUtilisateurRepository, DetailStatutUtilisateurRepository>();
        services.AddTransient<IInteretUtilisateurRepository, InteretUtilisateurRepository>();
        services.AddTransient<IDimensionRepository, DimensionRepository>();
        services.AddTransient<IFichierMessageRepository, FichierMessageRepository>();

        services.AddTransient<IUtilisateurGateway, UtilisateurGateway>();
        services.AddTransient<IAnnonceGateway, AnnonceGateway>();
        services.AddTransient<IProduitGateway, ProduitGateway>();
        services.AddTransient<IServiceGateway, ServiceGateway>();
        services.AddTransient<ICategorieGateway, CategorieGateway>();
        services.AddTransient<ICommuneGateway, CommuneGateway>();
        services.AddTransient<ISexeGateway, SexeGateway>();
        services.AddTransient<IConversationGateway, ConversationGateway>();
        services.AddTransient<IMessageGateway, MessageGateway>();
        services.AddTransient<IPhotoAnnonceGateway, PhotoAnnonceGateway>();
        services.AddTransient<ICommentaireAnnonceGateway, CommentaireAnnonceGateway>();
        services.AddTransient<ICommentairePhotoGateway, CommentairePhotoGateway>();
        services.AddTransient<IAdminGateway, AdminGateway>();
        services.AddTransient<IModererGateway, ModererGateway>();
        services.AddTransient<IEtatAnnonceGateway, EtatAnnonceGateway>();
        services.AddTransient<IEtatProduitGateway, EtatProduitGateway>();
        services.AddTransient<IStatutUtilisateurGateway, StatutUtilisateurGateway>();
        services.AddTransient<IDetailStatutUtilisateurGateway, DetailStatutUtilisateurGateway>();
        services.AddTransient<IInteretUtilisateurGateway, InteretUtilisateurGateway>();
        services.AddTransient<IDimensionGateway, DimensionGateway>();
        services.AddTransient<IFichierMessageGateway, FichierMessageGateway>();

        return services;
    }
}
