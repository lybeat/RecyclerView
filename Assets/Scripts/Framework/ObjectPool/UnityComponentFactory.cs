using UnityEngine;

public class UnityComponentFactory<T> : IObjectFactory<T> where T : Component
{
    private T template;
    private Transform parent;

    public UnityComponentFactory(T template, Transform parent)
    {
        this.template = template;
        this.parent = parent;
    }

    public T Create()
    {
        T obj = Object.Instantiate(template, parent);
        return obj;
    }

    public void Destroy(T obj)
    {
        Object.Destroy(obj.gameObject);
    }

    public void Reset(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.gameObject.transform.position = Vector3.zero;
        obj.gameObject.transform.rotation = Quaternion.identity;
    }

    public bool Validate(T obj)
    {
        return true;
    }
}
