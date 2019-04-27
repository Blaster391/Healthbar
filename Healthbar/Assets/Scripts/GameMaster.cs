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
    private GameState _state = GameState.Battling;
    public GameState CurrentState => _state;

    public static T Find<T>()
    {
        return GameObject.Find("GameMaster").GetComponent<T>();
    }

    public void TransitionTo(GameState state)
    {
        _state = state;
    }

    public void GameOver()
    {
        Debug.Log("Game done");
        TransitionTo(GameState.GameOver);
        //TODO what do
    }
}
