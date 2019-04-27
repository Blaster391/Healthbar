using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Battle", menuName = "Battle/Battle")]
public class BattleObject : ScriptableObject
{
    [SerializeField]
    private string _battleName;

    [SerializeField]
    private List<EnemyObject> _waves;

}
