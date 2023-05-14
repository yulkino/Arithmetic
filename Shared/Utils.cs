namespace Shared;

public static class Utils
{
    public static T PickRandom<T>(this ICollection<T> collection)//TODO debug
    {
        return collection.ElementAt(Random.Shared.Next(collection.Count - 1));
    }
}