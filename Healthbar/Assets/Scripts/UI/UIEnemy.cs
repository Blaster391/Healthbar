using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemy : MonoBehaviour
{
    [SerializeField]
    private Image _enemyImage;

    [SerializeField]
    private Image _attackSignalImage;

    [SerializeField]
    private float _speed = 10;

    [SerializeField]
    private float _offscreenDistance = 200;
    public float OffscreenDistance => _offscreenDistance;

    private EnemyScript _enemy;
    private GameTimeManager _timeManager;

    bool _offscreen = false;
    bool _moveOffscreen = false;
    bool _moveOnscreen = false;

    private float _initialX = 0;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = GameMaster.Find<EnemyScript>();
        _timeManager = GameMaster.Find<GameTimeManager>();
        _initialX = _enemyImage.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy.BaseEnemy == null)
        {
            return;
        }

        UpdateSprite();
        UpdateAttackSignal();
        UpdateMotion();
    }

    private void UpdateSprite()
    {

        var appearance = _enemy.BaseEnemy.Appearance;
        if (_enemy.CurrentHealth == 0)
        {
            _enemyImage.sprite = appearance.Hurt;
            return;
        }

        if (_timeManager.CurrentBeat % 2 == 0)
        {
            _enemyImage.sprite = appearance.Idle1;
        }
        else
        {
            _enemyImage.sprite = appearance.Idle2;
        }

        if (_enemy.AttackingThisBeat() && !_timeManager.AtHalfBeat)
        {
            _enemyImage.sprite = appearance.Attacking;
        }

        if (_enemy.HurtThisBeat())
        {
            _enemyImage.sprite = appearance.Hurt;
        }
    }

    private void UpdateAttackSignal()
    {
        var colour = _attackSignalImage.color;
        colour.a = 0;

        if (_enemy.AttackingThisBeat() && !_enemy.AttackingNextBeat())
        {
            if (!_timeManager.AtHalfBeat)
            {
                //Fade off if not attack next turn
                colour.a = 1 - (_timeManager.ProgressThroughBeat);
            }
        }
        else
        {
            if (_enemy.AttackingNextBeat() && _timeManager.AtHalfBeat)
            {
                colour.a = (_timeManager.ProgressThroughBeat);
            }
        }

        _attackSignalImage.color = colour;
    }

    private void UpdateMotion()
    {
        if (_moveOnscreen)
        {
            Vector3 newPos = _enemyImage.transform.position;
            newPos.x = newPos.x - _speed * Time.deltaTime;

            if (newPos.x < _initialX)
            {
                newPos.x = _initialX;
                _moveOnscreen = false;
                _offscreen = false;
            }
            _enemyImage.transform.position = newPos;
        }
        else if (_moveOffscreen)
        {
            Vector3 newPos = _enemyImage.transform.position;
            newPos.x = newPos.x + _speed * Time.deltaTime;

            if (newPos.x > _initialX + _offscreenDistance)
            {
                newPos.x = _initialX + _offscreenDistance;
                _moveOffscreen = false;
                _offscreen = true;
            }
            _enemyImage.transform.position = newPos;
        }
    }

    public bool IsOffscreen()
    {
        return _offscreen;
    }

    public void MoveOffscreen()
    {
        if (_offscreen)
        {
            return;
        }
        _moveOffscreen = true;
    }

    public void MoveOnscreen()
    {
        if (!_offscreen)
        {
            return;
        }
        _moveOnscreen = true;
    }

    public void ForceOffscreen()
    {
        _offscreen = true;
        Vector3 newPos = _enemyImage.transform.position;
        newPos.x = _initialX + _offscreenDistance;
        _enemyImage.transform.position = newPos;
    }
}
