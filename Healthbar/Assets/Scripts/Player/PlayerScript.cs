﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : ITickable
{
    public delegate void PlayerDamaged(int damage);
    public event PlayerDamaged OnPlayerDamaged;
    public delegate void PlayerKilled();
    public event PlayerKilled OnPlayerKilled;

    [SerializeField]
    private string _playerName = "";
    public string PlayerName { get { return _playerName; } set{ _playerName = value; } }

    [SerializeField]
    private int _maxHealth = 20;
    private int _currentHealth = 20;
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    private int _blockTimer = 0;
    private int _blockStrength = 1;
    public int BlockStrength => _blockStrength;

    bool _hurtThisBeat = false;
    bool _attackedThisBeat = false;
    public bool IsBlocking()
    {
        return _blockTimer > 0; 
    }

    public void EnableBlock(int blockLength, int blockStrength)
    {
        _blockTimer = blockLength;
        _blockStrength = blockStrength;
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int baseDamage)
    {
        _hurtThisBeat = true;
        int damage = baseDamage;
        if (IsBlocking())
        {
            damage -= _blockStrength;
            if(damage <= 0)
            {
                damage = 0;
                _hurtThisBeat = false;
            }
        }

        _currentHealth -= damage;
        OnPlayerDamaged?.Invoke(damage);
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            GameMaster.Find<GameMaster>().GameOver();
            OnPlayerKilled?.Invoke();
        }
    }

    public override void Tick()
    {
        _hurtThisBeat = false;
        _attackedThisBeat = false;
        if (IsBlocking())
        {
            _blockTimer--;
        }
    }

    public bool HurtThisBeat()
    {
        return _hurtThisBeat;
    }

    public void SetAttackedThisBeat(bool val)
    {
        _attackedThisBeat = val;
    }

    public bool AttackedThisBeat()
    {
        return _attackedThisBeat;
    }
}
