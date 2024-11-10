using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMixedComponentFactory<T> : IMixedObjectFactory<T> where T : Component
{
    protected T template;
    protected Transform parent;
    protected List<T> list;

    private Dictionary<string, T> dict = new();

    public UnityMixedComponentFactory(T template, Transform parent)
    {
        this.template = template;
        this.parent = parent;
    }

    public UnityMixedComponentFactory(List<T> list, Transform parent)
    {
        this.list = list;
        this.parent = parent;

        foreach (var data in list)
        {
            dict[data.name] = data;
        }
    }

    public UnityMixedComponentFactory(Dictionary<string, T> dict, Transform parent)
    {
        this.dict = dict;
        this.parent = parent;
    }

    public T Create(string typeName)
    {
        T obj = Object.Instantiate(dict[typeName], parent);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;

        return obj;
    }

    public void Destroy(string typeName, T obj)
    {
        Object.Destroy(obj.gameObject);
    }

    public void Reset(string typeName, T obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
    }

    public bool Validate(string typeName, T obj)
    {
        return true;
    }
}
