using UnityEngine;

public class SingletonComponent<T> : MonoBehaviour where T : MonoBehaviour
{
    public static bool IsAwakened { get; private set; }

    public static bool IsStarted { get; private set; }

    public static bool IsDestroyed { get; private set; }

    static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                if (IsDestroyed) return null;

                _instance = FindExistInstance() ?? CreateNewInstance();
            }
            return _instance;
        }
    }

    static T CreateNewInstance()
    {
        var obj = new GameObject("__" + typeof(T).Name + " (Singleton)");

        return obj.AddComponent<T>();
    }

    static T FindExistInstance()
    {
        T[] existInstances = FindObjectsOfType<T>();

        if (existInstances == null || existInstances.Length == 0)
            return null;

        return existInstances[0];
    }

    protected virtual void SingletonAwakened() { }

    protected virtual void SingletonStarted() { }

    protected virtual void SingletonDestroyed() { }

    void Awake()
    {
        T instance = GetComponent<T>();

        if (_instance == null)
        {
            _instance = instance;
        }
        else if (instance != _instance)
        {
            Debug.LogWarning($"Found a duplicated instance of a Singleton in GameObject {gameObject.name}");

            return;
        }

        if (!IsAwakened)
        {
            SingletonAwakened();
            IsAwakened = true;
        }

        DontDestroyOnLoad(_instance.gameObject);
    }

    void Start()
    {
        if (!IsStarted)
        {
            SingletonStarted();
            IsStarted = true;
        }
    }

    void OnDestroy()
    {
        IsDestroyed = true;
        SingletonDestroyed();
    }
}
