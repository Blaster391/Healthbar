using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverStateManager : MonoBehaviour
{
    [SerializeField]
    private float _fadeTime = 5;

    private float _currentFadeTime = 0;


    [SerializeField]
    public GameObject _gameOverScreen;
    [SerializeField]
    public GameObject _exitButton;
    [SerializeField]
    public GameObject _playAgainButton;

    [SerializeField]
    public Text _gameOverText;
    [SerializeField]
    public Text _logText;

    private BattleManager _battleManager;
    private EnemyScript _enemy;
    private PlayerScript _player;
    private GameMaster _gameMaster;
    private GameLog gameLog;

    void Start()
    {
        _player = GameMaster.Find<PlayerScript>();
        _enemy = GameMaster.Find<EnemyScript>();
        _gameMaster = GameMaster.Find<GameMaster>();
        _battleManager = GameMaster.Find<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameMaster.CurrentState == GameState.GameOver)
        {
            if(_fadeTime < _currentFadeTime)
            {

            }
            else
            {
                _exitButton.SetActive(true);
                _playAgainButton.SetActive(true);
            }
        }
    }

    public void StateStart()
    {
        _currentFadeTime = 0;
        _gameOverScreen.SetActive(true);
        _exitButton.SetActive(false);
        _playAgainButton.SetActive(false);
    }

    public void StateEnd()
    {
        _gameOverScreen.SetActive(false);
        _exitButton.SetActive(false);
        _playAgainButton.SetActive(false);
    }
}
