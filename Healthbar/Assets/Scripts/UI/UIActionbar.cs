using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActionbar : MonoBehaviour
{
    [SerializeField]
    private int _index = 0;

    private ActionController _actionController;



    // Start is called before the first frame update
    void Start()
    {
        _actionController = GameMaster.Find<ActionController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
