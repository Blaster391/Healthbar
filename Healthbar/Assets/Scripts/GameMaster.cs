using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static T Find<T>()
    {
        return GameObject.Find("GameMaster").GetComponent<T>();
    }

    public void GameOver()
    {
        Debug.Log("Game done");

        //TODO what do
    }
}
