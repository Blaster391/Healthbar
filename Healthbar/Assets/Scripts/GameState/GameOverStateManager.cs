using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverStateManager : MonoBehaviour
{

    private PlayerScript _player;
    private EnemyScript _enemy;
    private GameMaster _gameMaster;

    void Start()
    {
        _player = GameMaster.Find<PlayerScript>();
        _enemy = GameMaster.Find<EnemyScript>();
        _gameMaster = GameMaster.Find<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameMaster.CurrentState == GameState.GameOver)
        {

        }
    }

    public void StateStart()
    {

    }

    public void StateEnd()
    {

    }
}
