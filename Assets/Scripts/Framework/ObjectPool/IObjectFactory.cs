
public interface IObjectFactory<T> where T : class
{
    /// <summary>
    /// 创建对象
    /// </summary>
    /// <returns></returns>
    T Create();

    /// <summary>
    /// 销毁对象
    /// </summary>
    /// <param name="obj"></param>
    void Destroy(T obj);

    /// <summary>
    /// 重置对象
    /// </summary>
    /// <param name="obj"></param>
    void Reset(T obj);

    /// <summary>
    /// 验证对象
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    bool Validate(T obj);
}
