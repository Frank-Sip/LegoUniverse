using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private Dictionary<string, MonoBehaviour> services = new();
    
    private static ServiceLocator _instance;
    public static ServiceLocator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ServiceLocator>();
            }

            if (_instance == null)
            {
                var newGO = new GameObject("ServiceLocator");
                _instance = newGO.AddComponent<ServiceLocator>();
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
    }
    
    public T GetService<T>(string serviceName) where T : MonoBehaviour
    {
        if (services.TryGetValue(serviceName, out var service))
        {
            return service as T;
        }

        Debug.LogError($"Service {serviceName} not found");
        return null;
    }
    
    public void SetService(string serviceName, MonoBehaviour service)
    {
        if (service == null)
        {
            Debug.LogError("Attempted to set a null service.");
            return;
        }

        if (!services.ContainsKey(serviceName))
        {
            services.Add(serviceName, service);
        }
        else
        {
            Debug.LogWarning($"Service {serviceName} already exists and will be replaced.");
            services[serviceName] = service;
        }
    }
    
    public bool IsInitialized()
    {
        return _instance != null;
    }
}
