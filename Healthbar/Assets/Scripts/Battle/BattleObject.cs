using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Battle", menuName = "Battle/Battle")]
public class BattleObject : ScriptableObject
{
    [SerializeField]
    private string _battleName;
    public string BattleName { get { return _battleName; } }

    [SerializeField]
    private List<EnemyObject> _waves;
    public int GetNumberOfWaves()
    {
        return _waves.Count;
    }

    public EnemyObject GetWave(int idx)
    {
        return _waves[idx];
    }
}
