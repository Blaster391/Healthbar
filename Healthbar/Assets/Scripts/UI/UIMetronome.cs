using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMetronome : ITickable
{
    [SerializeField]
    private Sprite _pipActive;
    [SerializeField]
    private Sprite _pipDone;
    [SerializeField]
    private Sprite _pipAwaiting;

    [SerializeField]
    private Sprite _metronomeLeft;

    [SerializeField]
    private Sprite _metronomeRight;

    [SerializeField]
    private List<Image> _pips;

    [SerializeField]
    private Image _metronome;

    private void Update()
    {
        if (_timeManager.AtHalfBeat)
        {
            _metronome.sprite = _metronomeRight;
        }
        else
        {
            _metronome.sprite = _metronomeLeft;
        }
    }

    public override void Tick()
    {
       int beat = _timeManager.CurrentBeat;
       for(int i = 0; i < _timeManager.TotalBeats; ++i)
       {
            if(i < beat)
            {
                _pips[i].sprite = _pipDone;
            }
            else if( i == beat)
            {
                _pips[i].sprite = _pipActive;
            }
            else
            {
                _pips[i].sprite = _pipAwaiting;
            }

       }
    }
}
