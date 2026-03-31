using System.Diagnostics.CodeAnalysis;

namespace Challenge.Domain.Models;

[ExcludeFromCodeCoverage]
public class Pagination<T> where T : class
{
    public virtual IEnumerable<T>? Items { get; set; }

    public int Offset { get; set; }

    public int Limit { get; set; }

    public long Total { get; set; }
}