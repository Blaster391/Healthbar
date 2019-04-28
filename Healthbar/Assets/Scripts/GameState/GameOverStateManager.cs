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
    public Image _gameOverScreen;
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
    private GameLog _gameLog;

    void Start()
    {
        _player = GameMaster.Find<PlayerScript>();
        _enemy = GameMaster.Find<EnemyScript>();
        _gameMaster = GameMaster.Find<GameMaster>();
        _battleManager = GameMaster.Find<BattleManager>();
        _gameLog = GameMaster.Find<GameLog>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameMaster.CurrentState == GameState.GameOver)
        {
            if(_currentFadeTime < _fadeTime)
            {
                _currentFadeTime += Time.deltaTime;
                float alpha = _currentFadeTime / _fadeTime;
                var newColour = _gameOverScreen.color;
                newColour.a = alpha;
                _gameOverScreen.color = newColour;

                newColour = _gameOverText.color;
                newColour.a = alpha;
                _gameOverText.color = newColour;

                newColour = _logText.color;
                newColour.a = alpha;
                _logText.color = newColour;


                _gameOverText.text = $"{_player.PlayerName} has been slain!";

                _logText.text = $"The beast defeated {_gameLog.EnemiesDefeated} opponents";
                _logText.text += "\n";
                _logText.text += "\n";
                
                _logText.text += $"It was vanquished by a {_battleManager.GetCurrentWave().EnemyName}";

                _logText.text += "\n";
                _logText.text += "\n";

                _logText.text += $"Legends say that it won in the battles of \n";

                if(_gameLog.BattlesWon.Count == 0)
                {
                    _logText.text += $"Nothing \n";
                }

                foreach(var battle in _gameLog.BattlesWon)
                {
                    _logText.text += $"{battle} \n";
                }
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
        _gameOverScreen.gameObject.SetActive(true);
        _exitButton.SetActive(false);
        _playAgainButton.SetActive(false);
    }

    public void StateEnd()
    {
        _gameOverScreen.gameObject.SetActive(false);
        _exitButton.SetActive(false);
        _playAgainButton.SetActive(false);
    }
}
