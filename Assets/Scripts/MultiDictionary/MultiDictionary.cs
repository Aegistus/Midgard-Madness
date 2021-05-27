using System;
using System.Collections;
using System.Collections.Generic;

///<summary> A Dictionary that can contain multiple values per key and will return a random value when given a key. </summary>
public class MultiDictionary<TKey, TValue> : IEnumerable
{
    private Dictionary<TKey, List<TValue>> dictionary = new Dictionary<TKey, List<TValue>>();
    readonly Random random = new Random();

    public TValue this[TKey key]
    {
        get
        {
            int randIndex = random.Next(0, dictionary[key].Count);
            return dictionary[key][randIndex];
        }
        set
        {
            dictionary[key].Add(value);
        }
    }

    public void Add(TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key].Add(value);
        }
        else
        {
            dictionary.Add(key, new List<TValue>() { value });
        }
    }

    public IEnumerator GetEnumerator()
    {
        return ((IEnumerable)dictionary).GetEnumerator();
    }

    public void Remove(TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key].Remove(value);
        }
    }

    public bool ContainsKey(TKey key)
    {
        return dictionary.ContainsKey(key);
    }
}
