
public interface IMixedObjectFactory<T> where T : class
{
    T Create(string typeName);

    void Destroy(string typeName, T obj);

    void Reset(string typeName, T obj);

    bool Validate(string typeName, T obj);
}
