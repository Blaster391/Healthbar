using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHitsplat : MonoBehaviour
{
    [SerializeField]
    private float _timeToLive = 2;

    [SerializeField]
    private Text _value;
    [SerializeField]
    private Image _image;

    private float _currentLife = 0;

    public void Initialize(string value, Sprite image)
    {
        _value.text = value;
        _image.sprite = image;
    }



    // Update is called once per frame
    void Update()
    {
        _currentLife += Time.deltaTime;

        if(_currentLife > _timeToLive)
        {
            Destroy(gameObject);
        }
    }
}
