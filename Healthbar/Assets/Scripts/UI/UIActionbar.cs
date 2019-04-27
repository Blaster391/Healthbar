using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActionbar : MonoBehaviour
{
    [SerializeField]
    private int _index = 0;

    [SerializeField]
    private GameObject _toggle;
    [SerializeField]
    private GameObject _disabledScribble;

    [SerializeField]
    private GameObject[] _inputs;
    [SerializeField]
    private Image _actionSymbol;


    [SerializeField]
    private Sprite _attackSymbol;
    [SerializeField]
    private Sprite _defenceSymbol;
    [SerializeField]
    private Sprite _leftInputDefault;
    [SerializeField]
    private Sprite _leftInputActive;
    [SerializeField]
    private Sprite _rightInputDefault;
    [SerializeField]
    private Sprite _rightInputActive;

    private ActionController _actionController;

    // Start is called before the first frame update
    void Start()
    {
        _actionController = GameMaster.Find<ActionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_index >= _actionController.Actions.Count)
        {
            _toggle.SetActive(false);
            _disabledScribble.SetActive(true);
            return;
        }

        var action = _actionController.Actions[_index];

        _toggle.SetActive(true);
        _disabledScribble.SetActive(!action.IsActive());

        UpdateInputSymbols();

        switch (action.ActionType())
        {
            case ActionType.Attack:
                _actionSymbol.sprite = _attackSymbol;
                break;
            case ActionType.Defence:
                _actionSymbol.sprite = _defenceSymbol;
                break;
            default:
                Debug.Log("Missing action type symbol");
                break;
        }
    }

    void UpdateInputSymbols()
    {
        for (int i = 0; i < _inputs.Length; ++i)
        {
            var action = _actionController.Actions[_index];
            if (i < action.ActionPattern.Length)
            {
                _inputs[i].SetActive(true);
                if (action.ActionPattern[i] == InputType.Left)
                {
                    SetImage(i, _leftInputDefault, _leftInputActive);
                }
                else
                {
                    SetImage(i, _rightInputDefault, _rightInputActive);
                }
            }
            else
            {
                _inputs[i].SetActive(false);
            }
        }
    }

    void SetImage(int idx, Sprite defaultSprite, Sprite activeSprite)
    {
        var action = _actionController.Actions[_index];
        var currentInput = _actionController.CurrentInputs;

        Image inputImage = _inputs[idx].GetComponent<Image>();
        inputImage.sprite = defaultSprite;

        var colour = inputImage.color;
        colour.a = 1.0f;

        if(idx < action.ActionPattern.Length && currentInput.Count > idx)
        {
            if (action.ActionPattern[idx] == currentInput[idx])
            {
                inputImage.sprite = activeSprite;
            }
            else
            {
                colour.a = 0.25f;
            }
        }else if (currentInput.Count > action.ActionPattern.Length)
        {
            colour.a = 0.25f;
        }


        //if (currentInput.Count > action.ActionPattern.Length)
        //{
        //    colour.a = 0.25f;
        //}
        //else
        //{
        //    //if (action.ActionPattern[idx] == currentInput[idx])
        //    //{
        //    //    inputImage.sprite = activeSprite;
        //    //}
        //    //else
        //    //{ 
        //    //    colour.a = 0.25f;
        //    //}
        //}

        inputImage.color = colour;
    }
}
