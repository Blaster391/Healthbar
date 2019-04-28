using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAppearance", menuName = "Enemy/Appearance")]
public class EnemyAppearanceObject : ScriptableObject
{
    [SerializeField]
    private Sprite _idle1;
    public Sprite Idle1 => _idle1;

    [SerializeField]
    private Sprite _idle2;
    public Sprite Idle2 => _idle2;

    [SerializeField]
    private Sprite _attacking;
    public Sprite Attacking => _attacking;

    [SerializeField]
    private Sprite _hurt;
    public Sprite Hurt => _hurt;

}
