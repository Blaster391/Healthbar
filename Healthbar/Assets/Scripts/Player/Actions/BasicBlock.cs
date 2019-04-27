using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "Action/Block")]
public class BasicBlock : BaseAction
{
    protected override void TriggerCore()
    {
        Debug.Log("Am block");
    }
}
