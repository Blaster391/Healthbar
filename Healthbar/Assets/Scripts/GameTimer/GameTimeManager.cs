﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    [SerializeField]
    private float _tempo = 60;

    [SerializeField]
    private int _totalBeats = 8;
    public int TotalBeats { get { return _totalBeats; } }

    [SerializeField]
    private List<ITickable> _tickables;

    private int _currentBeat = 0;
    public int CurrentBeat { get { return _currentBeat; } }

    public bool AtHalfBeat { get { return _doneHalfTick; } }

    private float _lastTickTime = 0;
    public float ProgressThroughBeat { get { return _lastTickTime / (1.0f / _tempo); } }

    public void Register(ITickable tickable)
    {
        _tickables.Add(tickable);
    }

    public void Unregister(ITickable tickable)
    {
        _tickables.Remove(tickable);
    }

    // Update is called once per frame
    private bool _doneHalfTick = false;
    void Update()
    {
        _lastTickTime += Time.deltaTime;

        float tickRate = 1.0f / _tempo;
        if (!_doneHalfTick && _lastTickTime > (tickRate * 0.5f))
        {
            DoHalfTick();
        }

        if (_lastTickTime > tickRate)
        {
            _lastTickTime -= tickRate;
            DoTick();
        }
    }

    private void DoTick()
    {
        _currentBeat++;
        _doneHalfTick = false;
        if (_currentBeat >= _totalBeats)
        {
            _currentBeat = 0;
        }

        foreach (var tickable in _tickables)
        {
            tickable.Tick();
        }
    }

    private void DoHalfTick()
    {
        _doneHalfTick = true;
        foreach (var tickable in _tickables)
        {
            tickable.HalfTick();
        }
    }
}
