using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLog : MonoBehaviour
{
    public List<BattleObject> BattlesWon { get; set; } = new List<BattleObject>();
    public EnemyObject DefeatedBy { get; set; }
}
