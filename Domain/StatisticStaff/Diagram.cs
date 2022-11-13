using System.Collections;

namespace Domain.StatisticStaff;

public class Diagram<TElement, TX, TY> : IEnumerable<TElement>
    where TElement : IStatisticElement<TX, TY>
{
    public IReadOnlyCollection<TElement> Elements => _elements.AsReadOnly();
    private readonly List<TElement> _elements = new();

    public void AddNode(TElement element)
    {
        _elements.Add(element);
    }

    public IEnumerator<TElement> GetEnumerator()
    {
        return _elements.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}