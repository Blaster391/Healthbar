using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private BattleOrderObject _battleOrder;

    private int _currentBattleIndex = 0;
    public int BattleNumber => _currentBattleIndex;
    private int _currentWaveIndex = 0;
    public int WaveNumber => _currentWaveIndex;

    public BattleObject GetCurrentBattle()
    {
        return _battleOrder.GetBattle(_currentBattleIndex);
    }

    public EnemyObject GetCurrentWave()
    {
        return GetCurrentBattle().GetWave(_currentWaveIndex);
    }


    public bool OnFinalWaveOfBattle()
    {
        return _currentWaveIndex == (GetCurrentBattle().GetNumberOfWaves() - 1);
    }

    public void NextWave()
    {
        _currentWaveIndex++;
    }

    public void NextBattle()
    {
        _currentWaveIndex = 0;
        _currentBattleIndex++;
    }

    public void StateStart()
    {
        GameMaster.Find<GameTimeManager>().ResetTicks();
    }

    public void StateEnd()
    {

    }
}
