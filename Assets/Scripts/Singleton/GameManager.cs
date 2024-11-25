using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string currentLevel { get; private set; } = "Level 1";
    public GameObject pauseMenu;
    private StateMachine stateMachine = new StateMachine();
    private static GameManager instance;

    public AudioManager audioManager;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject managerObject = new GameObject("GameManager");
                    instance = managerObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            ChangeGameStatus(new MainMenuState());
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        stateMachine.Update(this);
    }

    public void ChangeGameStatus(GameState newStatus)
    {
        stateMachine.ChangeState(newStatus, this);
    }

    public void SetCurrentLevel(string levelName)
    {
        currentLevel = levelName;
    }

    public void DeactivatePauseOverlay()
    {
        pauseMenu.SetActive(false);
    }

    public void ActivePauseOverlay()
    {
        pauseMenu.SetActive(true);
    }
}