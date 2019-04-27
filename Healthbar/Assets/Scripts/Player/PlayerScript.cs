using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : ITickable
{
    [SerializeField]
    private string _playerName = "";
    public string PlayerName { get { return _playerName; } }

    private int _maxHealth = 100;
    private int _currentHealth = 100;

    private int _blockTimer = 0;
    private int _blockStrength = 1;

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
        int damage = baseDamage;
        if (IsBlocking())
        {
            damage -= _blockStrength;
        }

        _currentHealth -= damage;
        Debug.Log("Player damaged by " + damage);
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
            Debug.Log("Player ded");
            GameMaster.Find<GameMaster>().GameOver();
        }
        //TODO raise eventy
    }

    public override void Tick()
    {
        if (IsBlocking())
        {
            _blockTimer--;
        }
    }
}
