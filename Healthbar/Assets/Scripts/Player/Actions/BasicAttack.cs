using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Action/Attack")]
public class BasicAttack : BaseAction
{
    [SerializeField]
    private int _damage = 1;

    public override ActionType ActionType()
    {
        return global::ActionType.Attack;
    }

    public override string EffectText()
    {
        return _damage.ToString();
    }

    protected override void TriggerCore()
    {
        Enemy.TakeDamage(_damage);
    }
}
