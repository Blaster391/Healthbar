using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Enemy")]
public class EnemyObject : ScriptableObject
{
    [SerializeField]
    private string _enemyName;
    public string EnemyName {get { return _enemyName; } }

    [SerializeField]
    private string _enemyDialogue;
    public string EnemyDialogue { get { return _enemyDialogue; } }

    [SerializeField]
    private EnemyAppearanceObject _appearance;
    public EnemyAppearanceObject Appearance { get { return _appearance; } }

    [SerializeField]
    private int _maxHealth;
    public int MaxHealth { get { return _maxHealth; } }
}
