using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMidSlider : MonoBehaviour
{
    [SerializeField]
    private float _speed = 300;

    [SerializeField]
    private float _offscreenDistance = 600;

    [SerializeField]
    private Image _image;

    private float _initialY = 0;
    // Start is called before the first frame update
    void Start()
    {
        _initialY = _image.rectTransform.position.y;
    }

    bool _moveOnscreen = false;
    bool _moveOffscreen = false;
    bool _offscreen = false;

    // Update is called once per frame
    void Update()
    {
        if (_moveOnscreen)
        {
            Vector3 newPos = _image.transform.position;
            newPos.y = newPos.y - _speed * Time.deltaTime;

            if (newPos.y < _initialY)
            {
                newPos.y = _initialY;
                _moveOnscreen = false;
                _offscreen = false;
            }
            _image.transform.position = newPos;
        }
        else if (_moveOffscreen)
        {
            Vector3 newPos = _image.transform.position;
            newPos.y = newPos.y + _speed * Time.deltaTime;

            if (newPos.y > _initialY + _offscreenDistance)
            {
                newPos.y = _initialY + _offscreenDistance;
                _moveOffscreen = false;
                _offscreen = true;
            }
            _image.transform.position = newPos;
        }
    }

    public void SlideIn()
    {
        _moveOnscreen = true;
    }

    public void SlideOut()
    {
        _moveOffscreen = true;
    }

    public bool IsOnScreen()
    {
        return !_offscreen;
    }

    public void ForceOffscreen()
    {
        Vector3 newPos = _image.rectTransform.position;
        newPos.y = _initialY + _offscreenDistance;
        _image.rectTransform.position = newPos;
        _offscreen = true;
    }
}
