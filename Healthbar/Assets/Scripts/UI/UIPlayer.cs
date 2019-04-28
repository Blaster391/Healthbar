using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    [SerializeField]
    private Image _playerImage;

    [SerializeField]
    private Sprite _idle1;
    [SerializeField]
    private Sprite _idle2;
    [SerializeField]
    private Sprite _attack;
    [SerializeField]
    private Sprite _hurt;
    [SerializeField]
    private Sprite _guard;

    private PlayerScript _player;
    private GameTimeManager _timeManager;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameMaster.Find<PlayerScript>();
        _timeManager = GameMaster.Find<GameTimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateImage();
    }

    private void UpdateImage()
    {
        if (_timeManager.CurrentBeat % 2 == 0)
        {
            _playerImage.sprite = _idle1;
        }
        else
        {
            _playerImage.sprite = _idle2;
        }

        if (_player.IsBlocking())
        {
            _playerImage.sprite = _guard;
        }

        if (_player.HurtThisBeat())
        {
            _playerImage.sprite = _hurt;
        }

        if (_player.AttackedThisBeat())
        {
            _playerImage.sprite = _attack;
        }
    }
}
