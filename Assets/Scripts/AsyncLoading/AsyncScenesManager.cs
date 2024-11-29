using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncScenesManager : MonoBehaviour
{
    [Header("Scenes to load")] 
    [SerializeField] private string permanentScene = "Permanent Scene";
    
    private AsyncOperation permanentSceneLoadOperation;
    private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public void Start()
    {
        LoadPermanentSceneAsync();
    }
    
    public void LoadPermanentSceneAsync()
    {
        if (SceneManager.GetSceneByName(permanentScene).isLoaded)
            return;
        
        permanentSceneLoadOperation = SceneManager.LoadSceneAsync(permanentScene, LoadSceneMode.Additive);
        permanentSceneLoadOperation.allowSceneActivation = true;
        
        permanentSceneLoadOperation.completed += (AsyncOperation op) =>
        {
            Debug.Log("Escena permanente cargada correctamente.");
            DontDestroyOnLoad(SceneManager.GetSceneByName(permanentScene).GetRootGameObjects()[0]);
        };
    }
    
    public void UnloadSceneAsync(string sceneName)
    {
        Scene sceneToUnload = SceneManager.GetSceneByName(sceneName);
        if (sceneToUnload.isLoaded)
        {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneToUnload);
            unloadOperation.completed += (AsyncOperation op) =>
            {
                Debug.Log($"Escena {sceneName} descargada.");
            };
        }
        else
        {
            Debug.LogWarning($"La escena {sceneName} no está cargada para ser descargada.");
        }
    }
    
    public void LoadNewLevel(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
    }
    
    public bool IsPermanentSceneLoaded()
    {
        return SceneManager.GetSceneByName(permanentScene).isLoaded;
    }
    
    public bool IsLevelLoaded(string levelName)
    {
        Scene scene = SceneManager.GetSceneByName(levelName);
        
        return scene.isLoaded;
    }
}
