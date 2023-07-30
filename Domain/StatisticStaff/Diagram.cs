using System.Collections;

namespace Domain.StatisticStaff;

public class Diagram<TElement, TX, TY> : ICollection<TElement>
    where TElement : IStatisticElement<TX, TY>
{
    private readonly List<TElement> _elements = new();

    public Diagram() { }

    internal Diagram(IEnumerable<TElement> elements) => _elements = elements.ToList();
    public IReadOnlyCollection<TElement> Elements => _elements.AsReadOnly();

    public int Count { get { return _elements.Count; } }
    public bool IsReadOnly { get { return false; } }

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

    public void Add(TElement item)
    {
        _elements.Add(item);
    }

    public void Clear()
    {
        _elements.Clear();
    }

    public bool Contains(TElement item)
    {
        return _elements.Contains(item);
    }

    public void CopyTo(TElement[] array, int arrayIndex)
    {
        _elements.CopyTo(array, arrayIndex);
    }

    public bool Remove(TElement item)
    {
        return _elements.Remove(item);
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