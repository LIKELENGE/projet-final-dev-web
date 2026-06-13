using Core.UseCases;
using Core.UseCases.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddTransient<IInscrireUtilisateurUseCase, InscrireUtilisateurUseCase>();
        services.AddTransient<IRechercherAnnonceUseCase, RechercherAnnonceUseCase>();
        services.AddTransient<IConsulterAnnonceUseCase, ConsulterAnnonceUseCase>();
        services.AddTransient<ICreerAnnonceUseCase, CreerAnnonceUseCase>();
        services.AddTransient<IModifierAnnonceUseCase, ModifierAnnonceUseCase>();
        services.AddTransient<ISupprimerAnnonceUseCase, SupprimerAnnonceUseCase>();
        services.AddTransient<IChatterUseCase, ChatterUseCase>();
        services.AddTransient<IValiderAnnonceUseCase, ValiderAnnonceUseCase>();
        services.AddTransient<ICreerCategorieUseCase, CreerCategorieUseCase>();
        services.AddTransient<IModifierCategorieUseCase, ModifierCategorieUseCase>();
        services.AddTransient<ISupprimerCategorieUseCase, SupprimerCategorieUseCase>();

        return services;
    }
}
