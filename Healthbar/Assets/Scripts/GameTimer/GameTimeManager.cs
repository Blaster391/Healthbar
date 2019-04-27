using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    [SerializeField]
    private float _tempo = 60;

    [SerializeField]
    private int _totalBeats = 8;

    [SerializeField]
    private List<ITickable> _tickables;

    private int _currentBeat = 0;

    private float _lastTickTime = 0;

    public void Register(ITickable tickable)
    {
        _tickables.Add(tickable);
    }

    public void Unregister(ITickable tickable)
    {
        _tickables.Remove(tickable);
    }

    // Update is called once per frame
    void Update()
    {
        _lastTickTime += Time.deltaTime;

        float tickRate = 1.0f / _tempo;

        if (_lastTickTime > tickRate)
        {
            _lastTickTime -= tickRate;
            DoTick();
        }
    }

    private void DoTick()
    {
        _currentBeat++;
        if(_currentBeat >= _totalBeats)
        {
            _currentBeat = 0;
        }

        foreach (var tickable in _tickables)
        {
            tickable.Tick();
        }
    }
}
