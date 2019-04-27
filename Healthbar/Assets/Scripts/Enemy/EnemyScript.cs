﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : ITickable
{

    private PlayerScript _player;

    private EnemyObject _baseEnemy;
    public EnemyObject BaseEnemy { get { return _baseEnemy; } }

    private BattleManager _battleManager;

    private int _currentHealth = 10;
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _baseEnemy.MaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        _battleManager = GameMaster.Find<BattleManager>();
        _player = GameMaster.Find<PlayerScript>();
        Setup(_battleManager.GetCurrentWave());
    }

    public void Setup(EnemyObject enemy)
    {
        _baseEnemy = enemy;
        enemy.ParseAttackPattern();
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
        if(_currentHealth <= 0)
        {
            return;
        }

        if (_baseEnemy.IsAttackingOnBeat(_timeManager.CurrentBeat))
        {
            _player.TakeDamage(_baseEnemy.AttackDamage(_timeManager.CurrentBeat));
        }
    }
}
