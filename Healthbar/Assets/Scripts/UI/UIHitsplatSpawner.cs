using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHitsplatSpawner : MonoBehaviour
{
    [SerializeField]
    private UIHitsplat _splatPrefab;

    [SerializeField]
    private bool _enemySplatter;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private Sprite _blockedImage;
    [SerializeField]
    private Sprite _damageImage;

    private void OnEnable()
    {
        if (_enemySplatter)
        {
            GameMaster.Find<EnemyScript>().OnEnemyDamaged += OnDamage;
        }
        else
        {
            GameMaster.Find<PlayerScript>().OnPlayerDamaged += OnDamage;
        }

    }

    private void OnDisable()
    {
        if (_enemySplatter)
        {
            GameMaster.Find<EnemyScript>().OnEnemyDamaged -= OnDamage;
        }
        else
        {
            GameMaster.Find<PlayerScript>().OnPlayerDamaged -= OnDamage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDamage(int damage)
    {
        if (!_enemySplatter)
        {
            if (GameMaster.Find<PlayerScript>().IsBlocking())
            {
                var splat = Instantiate(_splatPrefab);
                Vector3 offset = new Vector3(Random.Range(-_radius, _radius), Random.Range(-_radius, _radius), 0);
                splat.transform.SetParent(transform,false);
                splat.transform.position = gameObject.transform.position + offset;
                splat.Initialize(GameMaster.Find<PlayerScript>().BlockStrength.ToString(), _blockedImage);

            }
        }

        if(damage != 0)
        {
            var splat = Instantiate(_splatPrefab);
            Vector3 offset = new Vector3(Random.Range(-_radius, _radius), Random.Range(-_radius, _radius), 0);
            splat.transform.SetParent(transform, false);
            splat.transform.position = gameObject.transform.position + offset;
            splat.Initialize(damage.ToString(), _damageImage);
        }

    }
}
