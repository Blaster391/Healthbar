using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : ITickable
{

    private EnemyObject _baseEnemy;
    public EnemyObject BaseEnemy { get { return _baseEnemy; } }

    private BattleManager _battleManager;

    private int _currentHealth = 10;

    // Start is called before the first frame update
    void Start()
    {
        _battleManager = GameMaster.Find<BattleManager>();
        Setup(_battleManager.GetCurrentWave());
    }

    public void Setup(EnemyObject enemy)
    {
        _baseEnemy = enemy;
        _currentHealth = enemy.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if(_currentHealth < 0)
        {
            _currentHealth = 0;
            Debug.Log("Am ded");
        }
        //TODO raise eventy
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Tick()
    {
        
    }
}
