using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLog : MonoBehaviour
{
    public int EnemiesDefeated { get; private set; } = 0;
    public List<string> BattlesWon { get; private set; } = new List<string>();

    public void Log(EnemyObject _enemy)
    {
        EnemiesDefeated++;
    }

    public void Log(BattleObject _enemy)
    {
        BattlesWon.Add(_enemy.BattleName);
    }
}
