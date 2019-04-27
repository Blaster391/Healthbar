using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleOrder", menuName = "Battle/Order")]
public class BattleOrderObject : ScriptableObject
{
    [SerializeField]
    private List<BattleObject> _battles;

    public BattleObject GetBattle(int idx)
    {
        return _battles[idx];
    }
}
