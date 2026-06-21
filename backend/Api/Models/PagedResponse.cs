namespace Api.Models;

public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();

    public int Page { get; set; }

    public int TaillePage { get; set; }

    public int Total { get; set; }

    public int TotalPages { get; set; }

    public static PagedResponse<T> Create(IEnumerable<T> source, int page, int taillePage)
    {
        var pageCorrigee = Math.Max(1, page);
        var tailleCorrigee = Math.Clamp(taillePage, 1, 50);
        var liste = source.ToList();
        var totalPages = (int)Math.Ceiling(liste.Count / (double)tailleCorrigee);

        return new PagedResponse<T>
        {
            Items = liste
                .Skip((pageCorrigee - 1) * tailleCorrigee)
                .Take(tailleCorrigee)
                .ToList(),
            Page = pageCorrigee,
            TaillePage = tailleCorrigee,
            Total = liste.Count,
            TotalPages = totalPages
        };
    }
}
