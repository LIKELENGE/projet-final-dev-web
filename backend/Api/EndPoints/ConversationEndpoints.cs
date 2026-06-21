using Api.Models;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class ConversationEndpoints
{
    public static IEndpointRouteBuilder MapConversationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/conversations");

        group.MapGet("/", (int? page, int? taillePage, IChatterUseCase useCase) =>
        {
            var conversations = useCase.GetConversations();

            return Results.Ok(PagedResponse<Conversation>.Create(conversations, page ?? 1, taillePage ?? 6));
        });

        group.MapPost("/", (CreerConversationRequest request, IChatterUseCase useCase) =>
        {
            var conversation = new Conversation
            {
                Titre = request.Titre,
                LienPhoto = request.LienPhoto
            };

            useCase.CreerConversation(conversation);

            return Results.Created("/conversations", conversation);
        });

        group.MapGet("/{conversationId:int}", (int conversationId, IChatterUseCase useCase) =>
        {
            var conversation = useCase.GetConversation(conversationId);

            return conversation == null ? Results.NotFound() : Results.Ok(conversation);
        });

        group.MapGet("/{conversationId:int}/messages", (int conversationId, int? page, int? taillePage, IChatterUseCase useCase) =>
        {
            var messages = useCase.GetMessages(conversationId);

            return Results.Ok(PagedResponse<Message>.Create(messages, page ?? 1, taillePage ?? 6));
        });

        group.MapPost("/{conversationId:int}/messages", (int conversationId, EnvoyerMessageRequest request, IChatterUseCase useCase) =>
        {
            var message = new Message
            {
                Conversation = new Conversation { Id = conversationId },
                Utilisateur = new Utilisateur { Id = request.UtilisateurId },
                Contenu = request.Contenu
            };

            useCase.EnvoyerMessage(message);

            return Results.Created($"/conversations/{conversationId}/messages", message);
        });

        group.MapPost("/messages/{messageId:int}/fichiers", (int messageId, AjouterFichierMessageRequest request, IChatterUseCase useCase) =>
        {
            var fichier = new FichierMessage
            {
                Message = new Message { Id = messageId },
                Lien = request.Lien
            };

            useCase.AjouterFichierMessage(fichier);

            return Results.Created($"/conversations/messages/{messageId}/fichiers", fichier);
        });

        return app;
    }
}
