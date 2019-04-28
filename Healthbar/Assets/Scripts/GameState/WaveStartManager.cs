using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveStartManager : MonoBehaviour
{

    [SerializeField]
    private UIEnemy _uiEnemy;
    [SerializeField]
    private GameObject _uiSpeechbubble;
    [SerializeField]
    private Text _uiSpeechText;

    [SerializeField]
    private float _speechTime;

    private float _speechBubbleTime = 0;

    private PlayerScript _player;
    private EnemyScript _enemy;
    private GameMaster _gameMaster;
    private InputController _inputController;

    void Start()
    {
        _player = GameMaster.Find<PlayerScript>();
        _enemy = GameMaster.Find<EnemyScript>();
        _gameMaster = GameMaster.Find<GameMaster>();
        _inputController = GameMaster.Find<InputController>();
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
                _uiSpeechbubble.SetActive(true);
                _speechBubbleTime -= Time.deltaTime;
                _uiSpeechText.text = _enemy.BaseEnemy.EnemyDialogue;

                if (_inputController.ConfirmPressed())
                {
                    _speechBubbleTime = -1;
                }

                if(_speechBubbleTime < 0)
                {
                    _uiSpeechbubble.SetActive(false);
                    _gameMaster.TransitionTo(GameState.Battling);
                }
            }
        }
    }



    public void StateStart()
    {
        // _scrolling = true;
        _enemy.Setup(GameMaster.Find<BattleManager>().GetCurrentWave());
        _uiEnemy.MoveOnscreen();
        _speechBubbleTime = _speechTime;
    }

    public void StateEnd()
    {
        _uiSpeechbubble.SetActive(false);
    }
}
