using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Core.Models;

namespace Api.Security;

public class JwtTokenService : IJwtTokenService
{
    private readonly string _secret;
    private readonly int _expirationMinutes;

    public JwtTokenService(IConfiguration configuration)
    {
        _secret = configuration["Jwt:Secret"] ??
            throw new InvalidOperationException("La cle JWT est manquante.");
        _expirationMinutes = configuration.GetValue("Jwt:ExpirationMinutes", 120);
    }

    public string GenerateToken(Utilisateur utilisateur)
    {
        ArgumentNullException.ThrowIfNull(utilisateur);

        var header = new Dictionary<string, object>
        {
            ["alg"] = "HS256",
            ["typ"] = "JWT"
        };

        var now = DateTimeOffset.UtcNow;
        var payload = new Dictionary<string, object>
        {
            ["sub"] = utilisateur.Id,
            ["mail"] = utilisateur.Mail,
            ["prenom"] = utilisateur.Prenom,
            ["nom"] = utilisateur.Nom,
            ["iat"] = now.ToUnixTimeSeconds(),
            ["exp"] = now.AddMinutes(_expirationMinutes).ToUnixTimeSeconds()
        };

        var encodedHeader = Base64UrlEncode(JsonSerializer.SerializeToUtf8Bytes(header));
        var encodedPayload = Base64UrlEncode(JsonSerializer.SerializeToUtf8Bytes(payload));
        var signature = Sign($"{encodedHeader}.{encodedPayload}");

        return $"{encodedHeader}.{encodedPayload}.{signature}";
    }

    public int? GetUtilisateurId(string? authorizationHeader)
    {
        if (string.IsNullOrWhiteSpace(authorizationHeader) ||
            !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        var token = authorizationHeader["Bearer ".Length..].Trim();
        var parts = token.Split('.');

        if (parts.Length != 3)
        {
            return null;
        }

        var expectedSignature = Sign($"{parts[0]}.{parts[1]}");
        var signatureBytes = Encoding.UTF8.GetBytes(parts[2]);
        var expectedBytes = Encoding.UTF8.GetBytes(expectedSignature);

        if (signatureBytes.Length != expectedBytes.Length ||
            !CryptographicOperations.FixedTimeEquals(signatureBytes, expectedBytes))
        {
            return null;
        }

        try
        {
            var payloadJson = Encoding.UTF8.GetString(Base64UrlDecode(parts[1]));
            using var payload = JsonDocument.Parse(payloadJson);
            var root = payload.RootElement;

            if (!root.TryGetProperty("exp", out var expProperty) ||
                DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= expProperty.GetInt64())
            {
                return null;
            }

            if (!root.TryGetProperty("sub", out var subProperty))
            {
                return null;
            }

            return subProperty.GetInt32();
        }
        catch
        {
            return null;
        }
    }

    private string Sign(string value)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secret));
        return Base64UrlEncode(hmac.ComputeHash(Encoding.UTF8.GetBytes(value)));
    }

    private static string Base64UrlEncode(byte[] bytes)
    {
        return Convert.ToBase64String(bytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }

    private static byte[] Base64UrlDecode(string value)
    {
        var base64 = value.Replace('-', '+').Replace('_', '/');
        var padding = base64.Length % 4;

        if (padding > 0)
        {
            base64 = base64.PadRight(base64.Length + 4 - padding, '=');
        }

        return Convert.FromBase64String(base64);
    }
}
