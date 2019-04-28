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
    private Image _waveAnnouncementPanel;
    [SerializeField]
    private Text _waveAnnouncementText;
    [SerializeField]
    private Text _battleAnnouncementText;

    [SerializeField]
    private float _speechTime = 3;
    [SerializeField]
    float _announcementTime = 5;


    private float _currentAnnouncementTime = 0;
    private float _speechBubbleTime = 0;

    private PlayerScript _player;
    private EnemyScript _enemy;
    private GameMaster _gameMaster;
    private InputController _inputController;
    private BattleManager _battleManager;


    void Start()
    {
        _player = GameMaster.Find<PlayerScript>();
        _enemy = GameMaster.Find<EnemyScript>();
        _gameMaster = GameMaster.Find<GameMaster>();
        _inputController = GameMaster.Find<InputController>();
        _battleManager = GameMaster.Find<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_gameMaster.CurrentState == GameState.WaveStarted)
        {
            if(_currentAnnouncementTime < _announcementTime)
            {
                _waveAnnouncementPanel.gameObject.SetActive(true);
                float alpha = _currentAnnouncementTime / (_announcementTime / 2);
                if(_currentAnnouncementTime > (_announcementTime / 2))
                {
                    alpha = 1 - (_currentAnnouncementTime - 1);
                }
                var panelColour = _waveAnnouncementPanel.color;
                panelColour.a = alpha;
                _waveAnnouncementPanel.color = panelColour;

               _currentAnnouncementTime += Time.deltaTime;

                _battleAnnouncementText.text = _battleManager.GetCurrentBattle().BattleName;
                _waveAnnouncementText.text = "Wave " + (_battleManager.WaveNumber + 1).ToString();

                var textColour = _battleAnnouncementText.color;
                textColour.a = alpha;
                _battleAnnouncementText.color = textColour;

                textColour = _waveAnnouncementText.color;
                textColour.a = alpha;
                _waveAnnouncementText.color = textColour;
            }
            else
            {
                _waveAnnouncementPanel.gameObject.SetActive(false);
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

                    if (_speechBubbleTime < 0)
                    {
                        _uiSpeechbubble.SetActive(false);
                        _gameMaster.TransitionTo(GameState.Battling);
                    }
                }
            }
        }
    }



    public void StateStart()
    {
        // _scrolling = true;
        _uiEnemy.ForceOffscreen();

        _enemy.Setup(GameMaster.Find<BattleManager>().GetCurrentWave());
       // _uiEnemy.MoveOnscreen();
        _speechBubbleTime = _speechTime;
    }

    public void StateEnd()
    {
        _uiSpeechbubble.SetActive(false);
        _waveAnnouncementPanel.gameObject.SetActive(false);
    }
}
