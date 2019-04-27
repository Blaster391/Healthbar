using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Enemy")]
public class EnemyObject : ScriptableObject
{
    [SerializeField]
    private string _enemyName;

    [SerializeField]
    private string _enemyDialogue;

    [SerializeField]
    private EnemyAppearanceObject _appearance;
}
