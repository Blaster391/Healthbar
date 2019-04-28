using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Battling,
    WaveStarted,
    WaveEnded,
    Shopping,
    GameOver
}

public class GameMaster : MonoBehaviour
{
    private GameState _state = GameState.WaveStarted;
    public GameState CurrentState => _state;

    public static T Find<T>()
    {
        return GameObject.Find("GameMaster").GetComponent<T>();
    }

    public void TransitionTo(GameState state)
    {
        switch (_state)
        {
            case GameState.Menu:
                GameMaster.Find<MainMenuManager>().StateEnd();
                break;
            case GameState.Battling:
                GameMaster.Find<BattleManager>().StateEnd();
                break;
            case GameState.WaveStarted:
                GameMaster.Find<WaveStartManager>().StateEnd();
                break;
            case GameState.WaveEnded:
                GameMaster.Find<WaveEndManager>().StateEnd();
                break;
            case GameState.Shopping:
                GameMaster.Find<ShoppingStateManager>().StateEnd();
                break;
            case GameState.GameOver:
                GameMaster.Find<GameOverStateManager>().StateEnd();
                break;
            default:
                break;
        }

        _state = state;

        switch (_state)
        {
            case GameState.Menu:
                GameMaster.Find<MainMenuManager>().StateStart();
                break;
            case GameState.Battling:
                GameMaster.Find<BattleManager>().StateStart();
                break;
            case GameState.WaveStarted:
                GameMaster.Find<WaveStartManager>().StateStart();
                break;
            case GameState.WaveEnded:
                GameMaster.Find<WaveEndManager>().StateStart();
                break;
            case GameState.Shopping:
                GameMaster.Find<ShoppingStateManager>().StateStart();
                break;
            case GameState.GameOver:
                GameMaster.Find<GameOverStateManager>().StateStart();
                break;
            default:
                Debug.LogError("Invalid state???");
                break;
        }
    }

    public void GameOver()
    {
        TransitionTo(GameState.GameOver);
    }
}
