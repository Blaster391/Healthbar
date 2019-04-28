using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActionbar : MonoBehaviour
{
    [SerializeField]
    private bool _shopItem = false;

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

    [SerializeField]
    private Image _cooldownImage;

    [SerializeField]
    private Text _cooldownText;
    [SerializeField]
    private Text _effectText;

    private ActionController _actionController;
    private GameTimeManager _timeManager;
    private BattleManager _battleManager;

    // Start is called before the first frame update
    void Start()
    {
        _actionController = GameMaster.Find<ActionController>();
        _timeManager = GameMaster.Find<GameTimeManager>();
        _battleManager = GameMaster.Find<BattleManager>();
    }

    private BaseAction GetAction()
    {
        if (_shopItem)
        {
            return _battleManager.GetCurrentBattle().PurchasableActions[_index];
        }

        return _actionController.Actions[_index];
    }

    // Update is called once per frame
    void Update()
    {
        if(!_shopItem &&  _index >= _actionController.Actions.Count)
        {
            _toggle.SetActive(false);
            return;
        }
        else if(_shopItem && _index >= _battleManager.GetCurrentBattle().PurchasableActions.Count)
        {
            _toggle.SetActive(false);
            return;
        }

        var action = GetAction();

        _toggle.SetActive(true);
        _disabledScribble.SetActive(!action.IsActive());
        if (_shopItem)
        {
            _disabledScribble.SetActive(false);
        }

        UpdateInputSymbols();
        UpdateCooldown();
        UpdateEffect();

 
    }

    void UpdateCooldown()
    {
        var action = GetAction();
        _cooldownText.text = action.CooldownLength.ToString();
        _cooldownImage.rectTransform.rotation = Quaternion.identity;
        _cooldownText.rectTransform.rotation = Quaternion.identity;
        if (!action.IsActive())
        {
            var remaining = action.CooldownRemaining;
            if(remaining > action.CooldownLength)
            {
                remaining = action.CooldownLength;
            }
            _cooldownText.text = remaining.ToString();

            if (_timeManager.AtHalfBeat)
            {
                _cooldownImage.rectTransform.rotation = Quaternion.Euler(0, 0, 30);
                _cooldownText.rectTransform.rotation = Quaternion.identity;
            }
            else
            {
                _cooldownImage.rectTransform.rotation = Quaternion.Euler(0, 0, -30);
                _cooldownText.rectTransform.rotation = Quaternion.identity;
            }


        }
    }

    void UpdateEffect()
    {
        var action = GetAction();
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
        var action = GetAction();

        for (int i = 0; i < _inputs.Length; ++i)
        {


            _effectText.text = action.EffectText();

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
        var action = GetAction();
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

        inputImage.color = colour;
    }
}
