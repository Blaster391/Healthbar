using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Enemy")]
public class EnemyObject : ScriptableObject
{
    [SerializeField]
    private AudioClip _enterSound;
    public AudioClip EnterSound => _enterSound;

    [SerializeField]
    private string _enemyName;
    public string EnemyName {get { return _enemyName; } }

    [SerializeField]
    private string _enemyDialogue;
    public string EnemyDialogue { get { return _enemyDialogue; } }

    [SerializeField]
    private EnemyAppearanceObject _appearance;
    public EnemyAppearanceObject Appearance { get { return _appearance; } }

    [SerializeField]
    private int _maxHealth;
    public int MaxHealth { get { return _maxHealth; } }

    [SerializeField]
    private string _attackPatternString = "x-x-x-x-x-x-x-x";
    private int[] _attackPattern;

    public bool IsAttackingOnBeat(int idx)
    {
        return _attackPattern[idx] != 0;
    }

    public bool IsAttackingNextBeat(int idx)
    {
        int i = idx + 1;
        if(i >= GameMaster.Find<GameTimeManager>().TotalBeats)
        {
            i = 0;
        }

        return _attackPattern[i] != 0;
    }

    public int AttackDamage(int idx)
    {
        return _attackPattern[idx];
    }

    public void ParseAttackPattern()
    {
        _attackPattern = new int[GameMaster.Find<GameTimeManager>().TotalBeats];
        string[] splitPattern = _attackPatternString.Split('-');
        if(_attackPattern.Length != splitPattern.Length)
        {
            Debug.Log("God damn it josh");
        }

        for(int i = 0; i < _attackPattern.Length; ++i)
        {
            if(splitPattern.Length == i)
            {
                break;
            }

            if(splitPattern[i].Equals("X", System.StringComparison.CurrentCultureIgnoreCase))
            {
                _attackPattern[i] = 0;
            }
            else
            {
                _attackPattern[i] = int.Parse(splitPattern[i]);
            }
        }
    }
}
