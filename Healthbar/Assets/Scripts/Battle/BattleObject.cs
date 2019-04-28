using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Battle", menuName = "Battle/Battle")]
public class BattleObject : ScriptableObject
{
    [SerializeField]
    private AudioClip _bgm;
    public AudioClip BattleBGM => _bgm;

    [SerializeField]
    private string _battleName;
    public string BattleName { get { return _battleName; } }

    [SerializeField]
    private List<EnemyObject> _waves;

    [SerializeField]
    private List<BaseAction> _purchasableActions;
    public List<BaseAction> PurchasableActions => _purchasableActions;

    public int GetNumberOfWaves()
    {
        return _waves.Count;
    }

    public EnemyObject GetWave(int idx)
    {
        return _waves[idx];
    }

 
}
