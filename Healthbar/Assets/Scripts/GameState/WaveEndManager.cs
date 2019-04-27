using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEndManager : MonoBehaviour
{


    [SerializeField]
    private float _scrollSpeed = 10;


    private PlayerScript _player;
    private EnemyScript _enemy;
    private BattleManager _battleManager;
    private GameMaster _gameMaster;

    bool _scrolling = false;
    float _initialX = 0;

    void Start()
    {
        _player = GameMaster.Find<PlayerScript>();
        _enemy = GameMaster.Find<EnemyScript>();
        _battleManager = GameMaster.Find<BattleManager>();
        _gameMaster = GameMaster.Find<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameMaster.CurrentState == GameState.WaveEnded)
        {
            if (_scrolling)
            {
                Vector3 newPos = _enemy.transform.position;
                newPos.x = _scrollSpeed * Time.deltaTime;

                if (newPos.x > _initialX + _enemy.OffscreenDistance)
                {
                    newPos.x = _initialX + _enemy.OffscreenDistance;
                    _scrolling = false;
                }
                _enemy.transform.position = newPos;
            }
            else
            {
                if (_battleManager.OnFinalWaveOfBattle())
                {
                    _gameMaster.TransitionTo(GameState.Shopping);
                }
                else
                {
                    _battleManager.NextWave();
                    _gameMaster.TransitionTo(GameState.WaveStarted);
                }
            }


        }
    }
    public void StateStart()
    {
       // _scrolling = true;
        _initialX = _enemy.transform.position.x;

       

    }

    public void StateEnd()
    {

    }
}
