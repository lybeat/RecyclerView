using System;
using System.Threading;

public class ObjectPool<T> : IObjectPool<T> where T : class
{
    private int maxSize;
    private int initialSize;
    protected readonly T[] entries = null;
    protected readonly IObjectFactory<T> factory;

    public ObjectPool(IObjectFactory<T> factory) : this(factory, Environment.ProcessorCount * 2)
    {
    }

    public ObjectPool(IObjectFactory<T> factory, int maxSize) : this(factory, 0, maxSize)
    {
    }

    public ObjectPool(IObjectFactory<T> factory, int initialSize, int maxSize)
    {
        this.factory = factory;
        this.initialSize = initialSize;
        this.maxSize = maxSize;
        this.entries = new T[maxSize];

        if (maxSize < initialSize)
        {
            throw new ArgumentException("The maxSize must be greater than or equal to the initialSize.");
        }

        for (int i = 0; i < initialSize; i++)
        {
            entries[i] = factory.Create();
        }
    }

    public int MaxSize => maxSize;

    public int InitialSize => initialSize;

    public virtual T Allocate()
    {
        for (var i = 0; i < entries.Length; i++)
        {
            T value = entries[i];
            if (value == null) continue;

            if (Interlocked.CompareExchange(ref entries[i], null, value) == value)
            {
                return value;
            }
        }

        return factory.Create();
    }

    public virtual void Free(T obj)
    {
        if (obj == null) return;

        if (!factory.Validate(obj))
        {
            factory.Destroy(obj);
            return;
        }

        factory.Reset(obj);

        for (var i = 0; i < entries.Length; i++)
        {
            if (Interlocked.CompareExchange(ref entries[i], obj, null) == null)
            {
                return;
            }
        }

        factory.Destroy(obj);
    }

    object IObjectPool.Allocate()
    {
        return Allocate();
    }

    public void Free(object obj)
    {
        Free((T)obj);
    }

    protected virtual void Clear()
    {
        for (var i = 0; i < entries.Length; i++)
        {
            var value = Interlocked.Exchange(ref entries[i], null);

            if (value != null)
            {
                factory.Destroy(value);
            }
        }
    }

    public void Dispose()
    {
        Clear();
        GC.SuppressFinalize(this);
    }
}
