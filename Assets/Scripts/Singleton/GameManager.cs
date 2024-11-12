using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
            ChangeGameStatus(new MainMenuState(), true);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        audioManager = FindObjectOfType<AudioManager>();
        
        if (audioManager == null)
        {
            GameObject audioManagerObject = new GameObject("AudioManager");
            audioManager = audioManagerObject.AddComponent<AudioManager>();
        }
    }

    private void Update()
    {
        stateMachine.Update(this);
    }

    public void ChangeGameStatus(GameState newStatus, bool loadScene)
    {
        stateMachine.ChangeState(newStatus, this);

        if (loadScene)
        {
            if (newStatus is MainMenuState)
            {
                SceneManager.LoadScene("Menu");
                audioManager.PlayBGM(0);
            }
            else if (newStatus is GameplayState)
            {
                SceneManager.LoadScene("Level 2");
                audioManager.PlayBGM(1);
            }
            else if (newStatus is VictoryState)
            {
                SceneManager.LoadScene("VictoryScene");
                audioManager.PlayBGM(2);
            }
            else if (newStatus is DefeatState)
            {
                SceneManager.LoadScene("DefeatScene");
                audioManager.PlayBGM(3);
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void CompletePuzzle()
    {
        ChangeGameStatus(new GameplayState(), false);
    }
}