using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : ITickable
{

    public delegate void EnemyDamaged(int damage);
    public event EnemyDamaged OnEnemyDamaged;
    public delegate void EnemyKilled();
    public event EnemyKilled OnEnemyKilled;
    public delegate void EnemySpawned();
    public event EnemySpawned OnEnemySpawned;

    private PlayerScript _player;

    private EnemyObject _baseEnemy;
    public EnemyObject BaseEnemy { get { return _baseEnemy; } }

    private BattleManager _battleManager;

    private int _currentHealth = 10;
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _baseEnemy.MaxHealth;

    private bool _hurtThisBeat = false;

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
        _hurtThisBeat = false;
        OnEnemySpawned?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _hurtThisBeat = true;

        OnEnemyDamaged?.Invoke(damage);
        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnEnemyKilled?.Invoke();
            GameMaster.Find<GameMaster>().TransitionTo(GameState.WaveEnded);
            
        }
        //TODO raise eventy
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Tick()
    {
        _hurtThisBeat = false;
        if (_currentHealth <= 0)
        {
            return;
        }

        if (_baseEnemy.IsAttackingOnBeat(_timeManager.CurrentBeat))
        {
            _player.TakeDamage(_baseEnemy.AttackDamage(_timeManager.CurrentBeat));
        }
    }

    public bool HurtThisBeat()
    {
        return _hurtThisBeat;
    }

    public bool AttackingThisBeat()
    {
        return _baseEnemy.IsAttackingOnBeat(_timeManager.CurrentBeat);
    }

    public bool AttackingNextBeat()
    {
        return _baseEnemy.IsAttackingNextBeat(_timeManager.CurrentBeat);
    }
}
