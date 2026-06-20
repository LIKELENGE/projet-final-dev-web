using Api.Models;
using Api.Security;

namespace Api.EndPoints;

public static class ImageEndpoints
{
    private const long MaxImageSize = 5 * 1024 * 1024;

    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg",
        ".jpeg",
        ".png",
        ".webp",
        ".gif"
    };

    public static IEndpointRouteBuilder MapImageEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/images/upload", async Task<IResult> (HttpContext context, IJwtTokenService jwtTokenService, IWebHostEnvironment environment) =>
        {
            var utilisateurId = jwtTokenService.GetUtilisateurId(context.Request.Headers.Authorization.ToString());

            if (utilisateurId == null)
            {
                return Results.Unauthorized();
            }

            if (!context.Request.HasFormContentType)
            {
                return Results.BadRequest(new { error = "La requete doit contenir un formulaire multipart." });
            }

            var form = await context.Request.ReadFormAsync();
            var image = form.Files.GetFile("image") ?? form.Files.GetFile("file") ?? form.Files.FirstOrDefault();

            if (image == null || image.Length == 0)
            {
                return Results.BadRequest(new { error = "Aucune image n'a ete envoyee." });
            }

            if (image.Length > MaxImageSize)
            {
                return Results.BadRequest(new { error = "L'image ne peut pas depasser 5 Mo." });
            }

            if (!image.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
            {
                return Results.BadRequest(new { error = "Le fichier envoye doit etre une image." });
            }

            var extension = Path.GetExtension(image.FileName);

            if (!AllowedExtensions.Contains(extension))
            {
                return Results.BadRequest(new { error = "Format d'image non autorise." });
            }

            var webRoot = environment.WebRootPath ?? Path.Combine(environment.ContentRootPath, "wwwroot");
            var imageDirectory = Path.Combine(webRoot, "images", "annonces");
            Directory.CreateDirectory(imageDirectory);

            var fileName = $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";
            var filePath = Path.Combine(imageDirectory, fileName);

            await using (var stream = File.Create(filePath))
            {
                await image.CopyToAsync(stream);
            }

            return Results.Ok(new ImageUploadResponse
            {
                Titre = form["titre"].ToString(),
                Lien = $"/images/annonces/{fileName}"
            });
        });

        return app;
    }
}
