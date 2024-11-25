using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : GameState
{
    public override void Enter(GameManager gameManager)
    {
        SceneManager.LoadScene("Menu");
        gameManager.audioManager.PlayBGM(0);
        Debug.Log("Entering Menu");
    }

    public override void Exit(GameManager gameManager)
    {
        Debug.Log("Leaving Menu");
    }

    public override void Update(GameManager gameManager)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.SetCurrentLevel("Level 1");
            gameManager.ChangeGameStatus(new GameplayState());
        }
    }
}
