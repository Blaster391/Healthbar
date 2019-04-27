using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction : MonoBehaviour
{
    [SerializeField]
    private int _cooldown = 1;

    private int _currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Tick()
    {
        if(_currentTime >= 0)
        {
            _currentTime -= 1;
        }
    }

    public bool IsActive()
    {
        return _currentTime <= 0;
    }
}
