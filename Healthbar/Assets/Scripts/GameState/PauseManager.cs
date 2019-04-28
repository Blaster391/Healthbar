using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _pauseScreen;

    private GameMaster _master;

    void Start()
    {
        _master = GameMaster.Find<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PausePressed())
        {
            _master.Paused = !_master.Paused;
        }
        _pauseScreen.SetActive(_master.Paused);

    }

    private bool PausePressed()
    {
        return Input.GetButtonDown("Pause");
    }

    public void ResumePressed()
    {
        _master.Paused = false;
    }
}
