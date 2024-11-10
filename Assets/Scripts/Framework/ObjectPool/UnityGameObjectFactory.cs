using UnityEngine;

public class UnityGameObjectFactory : IObjectFactory<GameObject>
{
    protected GameObject template;
    protected Transform parent;

    public UnityGameObjectFactory(GameObject template, Transform parent)
    {
        this.template = template;
        this.parent = parent;
    }

    public virtual GameObject Create()
    {
        return Object.Instantiate(template, parent);
    }

    public virtual void Reset(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
    }

    public virtual void Destroy(GameObject obj)
    {
        Object.Destroy(obj);
    }

    public virtual bool Validate(GameObject obj)
    {
        return true;
    }
}
