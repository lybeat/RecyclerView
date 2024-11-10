using UnityEngine;

public class UnityMixedGameObjectFactory : IMixedObjectFactory<GameObject>
{
    protected GameObject template;
    protected Transform parent;

    public UnityMixedGameObjectFactory(GameObject template, Transform parent)
    {
        this.template = template;
        this.parent = parent;
    }

    public GameObject Create(string typeName)
    {
        GameObject obj = Object.Instantiate(template, parent);
        GameObject model = Object.Instantiate(Resources.Load<GameObject>("ObjectPools/" + typeName), obj.transform);
        model.transform.position = Vector3.zero;
        model.transform.rotation = Quaternion.identity;

        return obj;
    }

    public void Destroy(string typeName, GameObject obj)
    {
        Object.Destroy(obj);
    }

    public void Reset(string typeName, GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
    }

    public bool Validate(string typeName, GameObject obj)
    {
        return true;
    }
}
