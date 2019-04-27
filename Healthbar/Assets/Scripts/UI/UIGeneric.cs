using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGeneric : MonoBehaviour
{
    [SerializeField]
    private Text _battleText;
    [SerializeField]
    private Text _playerNameText;
    [SerializeField]
    private Text _enemyNameText;

    private BattleManager _battleManager;
    private EnemyScript _enemy;
    private PlayerScript _player;

    // Start is called before the first frame update
    void Start()
    {
        _battleManager = GameMaster.Find<BattleManager>();
        _enemy = GameMaster.Find<EnemyScript>();
        _player = GameMaster.Find<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        _enemyNameText.text = _enemy.BaseEnemy.EnemyName;
        _playerNameText.text = _player.PlayerName;
        _battleText.text = _battleManager.GetCurrentBattle().BattleName;
    }
}
