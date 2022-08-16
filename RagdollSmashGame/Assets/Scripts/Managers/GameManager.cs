using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState state;
    public static event Action<GameState> onGameStateChanged;

    public enum GameState
    {
        STARTMENU,
        START,
        FINISH,
        WIN,
        LOSE
    }
    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.WIN:
                {
                    ReferenceManager.instance.WinScene.SetActive(true);
                    break;
                }
            case GameState.LOSE:
                {
                    ReferenceManager.instance.LoseScene.SetActive(true);
                    break;
                }
            case GameState.FINISH:
                {
                    break;
                }
            default:
                print("No State");
                break;
        }

        onGameStateChanged?.Invoke(newState);
    }
}
