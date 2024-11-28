using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private Dictionary<System.Type, MonoBehaviour> services = new();

    private static ServiceLocator _instance;
    public static ServiceLocator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ServiceLocator>();
                if (_instance == null)
                {
                    var newGO = new GameObject("ServiceLocator");
                    _instance = newGO.AddComponent<ServiceLocator>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Register<T>(T service) where T : MonoBehaviour
    {
        var type = typeof(T);
        if (!services.ContainsKey(type))
        {
            services[type] = service;
        }
        else
        {
            Debug.LogWarning($"Service {type.Name} already registered. Replacing.");
            services[type] = service;
        }
    }

    public T Get<T>() where T : MonoBehaviour
    {
        var type = typeof(T);
        if (services.TryGetValue(type, out var service))
        {
            return service as T;
        }

        Debug.LogError($"Service {type.Name} not found.");
        return null;
    }
}
