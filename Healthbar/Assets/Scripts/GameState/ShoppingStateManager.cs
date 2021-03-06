﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingStateManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _shoppingScreen;

    [SerializeField]
    private GameObject _doneButton;

    private PlayerScript _player;
    private ActionController _actionController;
    private EnemyScript _enemy;
    private GameMaster _gameMaster;
    private BattleManager _battleManager;
    private InputController _inputController;
    private GameLog _gameLog;
    private GenericAudio _audioSystem;

    void Start()
    {
        _player = GameMaster.Find<PlayerScript>();
        _enemy = GameMaster.Find<EnemyScript>();
        _gameMaster = GameMaster.Find<GameMaster>();
        _battleManager = GameMaster.Find<BattleManager>();
        _inputController = GameMaster.Find<InputController>();
        _gameLog = GameMaster.Find<GameLog>();
        _actionController = GameMaster.Find<ActionController>();
        _audioSystem = GameMaster.Find<GenericAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameMaster.CurrentState == GameState.Shopping)
        {
            if (_inputController.ConfirmPressed())
            {
                ShoppingDone();
            }

            _doneButton.SetActive(_actionController.Actions.Count >= 2);
        }
    }

    public void ShoppingDone()
    {
        if(_actionController.Actions.Count < 2)
        {
            return;
        }

        _gameLog.Log(_battleManager.GetCurrentBattle());
        _battleManager.NextBattle();
        _gameMaster.TransitionTo(GameState.WaveStarted);
    }

    public void StateStart()
    {
        foreach(var action in _battleManager.GetCurrentBattle().PurchasableActions)
        {
            action.ParsePattern();
        }

        _shoppingScreen.SetActive(true);
        _audioSystem.ShopOpen();
    }

    public void StateEnd()
    {
        _shoppingScreen.SetActive(false);
    }
}
