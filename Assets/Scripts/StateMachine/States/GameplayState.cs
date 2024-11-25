using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayState : GameState
{
    public override void Enter(GameManager gameManager)
    {
        if (SceneManager.GetActiveScene().name != gameManager.currentLevel)
        {
            SceneManager.LoadScene(gameManager.currentLevel);
        }
        gameManager.audioManager.PlayBGM(1);
        Debug.Log("Joining game");
    }

    public override void Exit(GameManager gameManager)
    {
        Debug.Log("Leaving game");
    }

    public override void Update(GameManager gameManager)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.ChangeGameStatus(new PauseState());
        }
    }
}
