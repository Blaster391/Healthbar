using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStartManager : MonoBehaviour
{


    private float _initialX = 0;

    [SerializeField]
    private float _speed = 10;

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
            if (_scrolling)
            {
                Vector3 newPos = _enemy.transform.position;
                newPos.x = _speed * Time.deltaTime;

                if (newPos.x > _initialX - _enemy.OffscreenDistance)
                {
                    newPos.x = _initialX - _enemy.OffscreenDistance;
                    _scrolling = false;
                }
                _enemy.transform.position = newPos;
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
        _initialX = _enemy.transform.position.x;
    }

    public void StateEnd()
    {

    }
}
