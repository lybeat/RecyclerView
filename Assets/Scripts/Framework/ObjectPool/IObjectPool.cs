using System;

public interface IObjectPool : IDisposable
{
    /// <summary>
    /// 从池子中分配一个可用对象，没有的话就创建一个
    /// </summary>
    /// <returns></returns>
    object Allocate();

    /// <summary>
    /// 将对象回收到池子中去，如果池中的对象数量已经超过了 maxSize，则直接销毁该对象
    /// </summary>
    /// <param name="obj"></param>
    void Free(object obj);
}

public interface IObjectPool<T> : IObjectPool, IDisposable where T : class
{
    new T Allocate();

    void Free(T obj);
}
