using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _menuScreen;
    [SerializeField]
    private InputField _nameInput;

    private PlayerScript _player;
    private EnemyScript _enemy;
    private GameMaster _gameMaster;
    private GameLog _gameLog;

    void Start()
    {
        _player = GameMaster.Find<PlayerScript>();
        _enemy = GameMaster.Find<EnemyScript>();
        _gameMaster = GameMaster.Find<GameMaster>();
        _gameLog = GameMaster.Find<GameLog>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameMaster.CurrentState == GameState.Menu)
        {
            _menuScreen.SetActive(true);
        }
    }

    public void StateStart()
    {
        _menuScreen.SetActive(true);
    }

    public void StateEnd()
    {
        _player.PlayerName = _nameInput.text;

        if (string.IsNullOrWhiteSpace(_player.PlayerName))
        {
            _player.PlayerName = "The Squib";
        }
        _menuScreen.SetActive(false);
    }

    public void StartGame()
    {
        _gameMaster.TransitionTo(GameState.Shopping);
    }
}
