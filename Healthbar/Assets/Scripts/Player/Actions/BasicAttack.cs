using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Action/Attack")]
public class BasicAttack : BaseAction
{
    [SerializeField]
    private int _damage = 1;

    protected override void TriggerCore()
    {
        Debug.Log("Am atak");
        Enemy.TakeDamage(_damage);
    }
}
