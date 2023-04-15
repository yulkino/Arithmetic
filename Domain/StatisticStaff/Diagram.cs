﻿using System.Collections;

namespace Domain.StatisticStaff;

public class Diagram<TElement, TX, TY> : IEnumerable<TElement>
    where TElement : IStatisticElement<TX, TY>
{
    public IReadOnlyCollection<TElement> Elements => _elements.AsReadOnly();
    private List<TElement> _elements = new();

    public Diagram() { }

    internal Diagram(IEnumerable<TElement> elements) => _elements = elements.ToList();

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

public static class EnumerableExtensions
{
    public static Diagram<TElement, TX, TY> ToDiagram<TElement, TX, TY>(this IEnumerable<TElement> elements)
        where TElement : IStatisticElement<TX, TY>
    {
        return new(elements);
    }
}