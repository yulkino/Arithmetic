using System.Collections;

namespace Domain.StatisticStaff;

public class Diagram<TElement, TX, TY> : IEnumerable<TElement>
    where TElement : IStatisticElement<TX, TY>
{
    private readonly List<TElement> _elements = new();

    public Diagram() { }

    internal Diagram(IEnumerable<TElement> elements) => _elements = elements.ToList();
    public IReadOnlyCollection<TElement> Elements => _elements.AsReadOnly();

    public IEnumerator<TElement> GetEnumerator()
    {
        return _elements.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void AddNode(TElement element)
    {
        _elements.Add(element);
    }
}

public static class EnumerableExtensions
{
    public static Diagram<TElement, TX, TY> ToDiagram<TElement, TX, TY>(this IEnumerable<TElement> elements)
        where TElement : IStatisticElement<TX, TY>
    {
        return new Diagram<TElement, TX, TY>(elements);
    }
}