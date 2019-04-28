using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingStateManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _shoppingScreen;

    private PlayerScript _player;
    private EnemyScript _enemy;
    private GameMaster _gameMaster;
    private BattleManager _battleManager;

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
        if (_gameMaster.CurrentState == GameState.Shopping)
        {

        }
    }

    public void ShoppingDone()
    {
        _battleManager.NextBattle();
        _gameMaster.TransitionTo(GameState.WaveStarted);
    }

    public void StateStart()
    {
        _shoppingScreen.SetActive(true);
    }

    public void StateEnd()
    {
        _shoppingScreen.SetActive(false);
    }
}
