using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private BattleOrderObject _battleOrder;

    private int _currentBattleIndex;

    public BattleObject GetCurrentBattle()
    {
        return _battleOrder.GetBattle(_currentBattleIndex);
    }

    public void NextBattle()
    {

    }
}
