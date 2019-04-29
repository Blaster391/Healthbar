using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveStartManager : MonoBehaviour
{

    [SerializeField]
    private UIEnemy _uiEnemy;
    [SerializeField]
    private UIMidSlider _uiMid;

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
    private bool _slideSoundPlayed = false;
    private bool _slideSoundPlayed2 = false;
    private bool _speechSoundPlayed = false;
    private PlayerScript _player;
    private EnemyScript _enemy;
    private GameMaster _gameMaster;
    private InputController _inputController;
    private BattleManager _battleManager;
    private GenericAudio _audioSystem;


    void Start()
    {
        _player = GameMaster.Find<PlayerScript>();
        _enemy = GameMaster.Find<EnemyScript>();
        _gameMaster = GameMaster.Find<GameMaster>();
        _inputController = GameMaster.Find<InputController>();
        _battleManager = GameMaster.Find<BattleManager>();
        _audioSystem = GameMaster.Find<GenericAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_gameMaster.CurrentState == GameState.WaveStarted)
        {
            if(_currentAnnouncementTime < _announcementTime)
            {
                _waveAnnouncementPanel.gameObject.SetActive(true);

                float quarterTime = (_announcementTime / 4);

                float alpha = _currentAnnouncementTime / quarterTime;

                if (_currentAnnouncementTime > quarterTime)
                {
                    alpha = 1; 
                }

                if(_currentAnnouncementTime > quarterTime * 3)
                {
                    alpha = 1 - (_currentAnnouncementTime - quarterTime * 3);
                }
                var panelColour = _waveAnnouncementPanel.color;
                panelColour.a = alpha * 0.75f;
                _waveAnnouncementPanel.color = panelColour;

               _currentAnnouncementTime += Time.deltaTime;

                _battleAnnouncementText.text = _battleManager.GetCurrentBattle().BattleName;

                _waveAnnouncementText.text = "Wave " + (_battleManager.WaveNumber + 1 + _battleManager.EndGameCount).ToString() + " / " + _battleManager.GetCurrentBattle().GetNumberOfWaves();
                if (_battleManager.OnFinalWaveOfBattle())
                {
                    _waveAnnouncementText.text = "Wave " + (_battleManager.WaveNumber + 1 + _battleManager.EndGameCount).ToString() + " / " + "???";
                }


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
                    if (!_slideSoundPlayed)
                    {
                        _audioSystem.EnemySlide();
                        _slideSoundPlayed = true;
                    }

                    _uiEnemy.MoveOnscreen();
                }
                else
                {
                    _uiSpeechbubble.SetActive(true);
                    _speechBubbleTime -= Time.deltaTime;
                    _uiSpeechText.text = _enemy.BaseEnemy.EnemyDialogue;

                    if (!_speechSoundPlayed)
                    {
                        _speechSoundPlayed = true;
                        _audioSystem.EnemyEnterSound();
                    }

                    if (_inputController.ConfirmPressed())
                    {
                        _speechBubbleTime = -1;
                    }

                    if (_speechBubbleTime < 0)
                    {
                        _uiSpeechbubble.SetActive(false);
                        if (!_uiMid.IsOnScreen())
                        {
                            if (!_slideSoundPlayed2)
                            {
                                _audioSystem.EnemySlide();
                                _slideSoundPlayed2 = true;
                            }
                            _uiMid.SlideIn();
                        }
                        else
                        {
                            _gameMaster.TransitionTo(GameState.Battling);
                        }
                    }
                }
            }
        }
    }



    public void StateStart()
    {
        // _scrolling = true;
        _uiEnemy.ForceOffscreen();
        _uiMid.ForceOffscreen();

        _enemy.Setup(GameMaster.Find<BattleManager>().GetCurrentWave());
       // _uiEnemy.MoveOnscreen();
        _speechBubbleTime = _speechTime;
        _slideSoundPlayed = false;
        _slideSoundPlayed2 = false;
        _speechSoundPlayed = false;

        _currentAnnouncementTime = 0;

        _audioSystem.BattleStart();
    }

    public void StateEnd()
    {
        _uiSpeechbubble.SetActive(false);
        _waveAnnouncementPanel.gameObject.SetActive(false);
    }
}
