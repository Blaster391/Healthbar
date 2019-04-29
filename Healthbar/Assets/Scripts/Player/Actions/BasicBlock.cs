using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "Action/Block")]
public class BasicBlock : BaseAction
{
    [SerializeField]
    private int _blockLength = 1;
    [SerializeField]
    private int _blockStrength = 1;

    public override ActionType ActionType()
    {
        return global::ActionType.Defence;
    }

    public override string EffectText()
    {
        return _blockStrength.ToString() + "-" + _blockLength.ToString();
    }

    protected override void TriggerCore()
    {
        //One because will count down on first tick
        Player.EnableBlock(_blockLength + 1, _blockStrength);
    }
}
