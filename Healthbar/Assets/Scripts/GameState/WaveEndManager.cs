using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEndManager : MonoBehaviour
{
    [SerializeField]
    private UIEnemy _uiEnemy;

    [SerializeField]
    private UIMidSlider _uiMid;

    private PlayerScript _player;
    private EnemyScript _enemy;
    private BattleManager _battleManager;
    private GameMaster _gameMaster;
    private GameTimeManager _timeManager;
    private GameLog _gameLog;

    void Start()
    {
        _player = GameMaster.Find<PlayerScript>();
        _enemy = GameMaster.Find<EnemyScript>();
        _battleManager = GameMaster.Find<BattleManager>();
        _gameMaster = GameMaster.Find<GameMaster>();
        _timeManager = GameMaster.Find<GameTimeManager>();
        _gameLog = GameMaster.Find<GameLog>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameMaster.CurrentState == GameState.WaveEnded)
        {
            if (!_uiEnemy.IsOffscreen())
            {
                _uiEnemy.MoveOffscreen();
            }
            else
            {
                if (_uiMid.IsOnScreen())
                {
                    _uiMid.SlideOut();
                }
                else
                {
                    if (_battleManager.AtEndOfGame())
                    {
                        _battleManager.EndGameCount++;
                        _gameMaster.TransitionTo(GameState.WaveStarted);
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


        }
    }
    public void StateStart()
    {
        _uiEnemy.MoveOffscreen();
        _timeManager.ResetTicks();

        //Turn off blocking
        _player.EnableBlock(0, 0);
        _gameLog.Log(_battleManager.GetCurrentWave());
    }

    public void StateEnd()
    {
        _player.SetAttackedThisBeat(false);
    }
}
