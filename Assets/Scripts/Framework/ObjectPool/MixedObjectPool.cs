using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

public class MixedObjectPool<T> : IMixedObjectPool<T> where T : class
{
    private const int DEFAULT_MAX_SIZE_PER_TYPE = 10;

    private readonly ConcurrentDictionary<string, List<T>> entries;
    private readonly ConcurrentDictionary<string, int> typeSize;
    private readonly IMixedObjectFactory<T> factory;

    private int defaultMaxSizePerType;

    public MixedObjectPool(IMixedObjectFactory<T> factory) : this(factory, DEFAULT_MAX_SIZE_PER_TYPE)
    {
    }

    public MixedObjectPool(IMixedObjectFactory<T> factory, int defaultMaxSizePerType)
    {
        this.factory = factory;
        this.defaultMaxSizePerType = defaultMaxSizePerType;

        if (defaultMaxSizePerType <= 0)
        {
            throw new ArgumentException("The maxSize must be greater than 0.");
        }

        entries = new ConcurrentDictionary<string, List<T>>();
        typeSize = new ConcurrentDictionary<string, int>();
    }

    public T Allocate(string typeName)
    {
        if (entries.TryGetValue(typeName, out List<T> list) && list.Count > 0)
        {
            T obj = list[0];
            list.RemoveAt(0);
            return obj;
        }
        return factory.Create(typeName);
    }

    public void Free(string typeName, T obj)
    {
        if (obj == null) return;

        if (!factory.Validate(typeName, obj))
        {
            factory.Destroy(typeName, obj);
            return;
        }

        int maxSize = GetMaxSize(typeName);
        List<T> list = entries.GetOrAdd(typeName, n => new List<T>());
        if (list.Count >= maxSize)
        {
            factory.Destroy(typeName, obj);
            return;
        }

        factory.Reset(typeName, obj);
        list.Add(obj);
    }

    public int GetMaxSize(string typeName)
    {
        if (typeSize.TryGetValue(typeName, out int size))
        {
            return size;
        }
        return defaultMaxSizePerType;
    }

    public void SetMaxSize(string typeName, int value)
    {
        typeSize.AddOrUpdate(typeName, value, (key, oldValue) => value);
    }

    protected virtual void Clear()
    {
        foreach (var kv in entries)
        {
            string typeName = kv.Key;
            List<T> list = kv.Value;

            if (list == null || list.Count <= 0) continue;

            list.ForEach(e => factory.Destroy(typeName, e));
            list.Clear();
        }
        entries.Clear();
        typeSize.Clear();
    }

    public void Dispose()
    {
        Clear();
        GC.SuppressFinalize(this);
    }
}
