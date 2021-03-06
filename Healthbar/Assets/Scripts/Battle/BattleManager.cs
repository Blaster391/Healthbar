﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private BattleOrderObject _battleOrder;

    public delegate void NextBattleEvent();
    public event NextBattleEvent OnNextBattle;

    private int _currentBattleIndex = 0;
    public int BattleNumber => _currentBattleIndex;
    private int _currentWaveIndex = 0;
    public int WaveNumber => _currentWaveIndex;

    public int EndGameCount = 0;

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
        OnNextBattle?.Invoke();
    }

    public void StateStart()
    {
        GameMaster.Find<GameTimeManager>().ResetTicks();
        GameMaster.Find<ActionController>().ResetCooldowns();
        GameMaster.Find<ActionController>().CurrentInputs.Clear();
    }

    public void StateEnd()
    {
        GameMaster.Find<GameTimeManager>().ResetTicks();
        GameMaster.Find<ActionController>().ResetCooldowns();
        GameMaster.Find<ActionController>().CurrentInputs.Clear();
    }

    public bool AtEndOfGame()
    {
        return OnFinalWaveOfBattle() && (_battleOrder.Battles.Count == (_currentBattleIndex + 1));
    }
}
