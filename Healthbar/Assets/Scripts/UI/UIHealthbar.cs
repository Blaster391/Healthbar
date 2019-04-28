using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthbar : MonoBehaviour
{

    private float _startingOffset;

    [SerializeField]
    private Text _healthText;

    [SerializeField]
    private float _endingOffset;
    [SerializeField]
    private bool _enemyHealth = false;

    [SerializeField]
    private Image _bar;

    private int _maxHealth = 100;
    public int _currentHealth = 100;



    // Start is called before the first frame update
    void Start()
    {
        _startingOffset = _bar.gameObject.transform.position.x;

    }

    void UpdateHealths()
    {
        if (_enemyHealth)
        {
            if (GameMaster.Find<EnemyScript>().BaseEnemy == null)
            {
                return;
            }

            _currentHealth = GameMaster.Find<EnemyScript>().CurrentHealth;
            _maxHealth = GameMaster.Find<EnemyScript>().MaxHealth;
        }
        else
        {
            _currentHealth = GameMaster.Find<PlayerScript>().CurrentHealth;
            _maxHealth = GameMaster.Find<PlayerScript>().MaxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealths();
        _healthText.text = _currentHealth.ToString();

        float distance =  _endingOffset;
        float percentageHealth = (float)_currentHealth / _maxHealth;

        Vector3 pos = _bar.gameObject.transform.position;
        pos.x = _startingOffset + (1 - percentageHealth) * distance;
        _bar.gameObject.transform.position = pos;
    }
}
