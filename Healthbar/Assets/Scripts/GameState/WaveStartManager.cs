using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStartManager : MonoBehaviour
{

    [SerializeField]
    private UIEnemy _uiEnemy;

    private bool _scrolling = false;

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
        if(_gameMaster.CurrentState == GameState.WaveStarted)
        {
            if (_uiEnemy.IsOffscreen())
            {
                _uiEnemy.MoveOnscreen();
            }
            else
            {
                _gameMaster.TransitionTo(GameState.Battling);
            }
        }
    }



    public void StateStart()
    {
        // _scrolling = true;
        _enemy.Setup(GameMaster.Find<BattleManager>().GetCurrentWave());
        _uiEnemy.MoveOnscreen();
    }

    public void StateEnd()
    {

    }
}
