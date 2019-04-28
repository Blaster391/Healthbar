using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPurchasableAction : MonoBehaviour
{
    [SerializeField]
    private int _index = 0;

    [SerializeField]
    private GameObject _toggle;

    [SerializeField]
    private Text _actionName;

    [SerializeField]
    private Text _actionCostText;

    private BattleManager _battleManager;
    private ActionController _actionController;
    private PlayerScript _player;

    // Start is called before the first frame update
    void Start()
    {
        _battleManager = GameMaster.Find<BattleManager>();
        _actionController = GameMaster.Find<ActionController>();
        _player = GameMaster.Find<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_battleManager.GetCurrentBattle().PurchasableActions.Count <= _index)
        {
            _toggle.SetActive(false);
        }
        else
        {
            var action = _battleManager.GetCurrentBattle().PurchasableActions[_index];
            _toggle.SetActive(true);

            _actionName.text = action.ActionName;
            _actionCostText.text = action.PurchaseCost.ToString();

            foreach (var ownedAction in _actionController.Actions)
            {
                if (ownedAction == action)
                {
                    _toggle.SetActive(false);
                }
            }
        }
    }

    public void Purchase(int slot)
    {
        var action = _battleManager.GetCurrentBattle().PurchasableActions[_index];
        if(_player.CurrentHealth <= action.PurchaseCost)
        {
            return;
        }

        //For first purchase
        if (_actionController.Actions.Count < 3)
        {
            _actionController.Actions.Add(action);
        }
        else
        {
            _actionController.Actions[slot] = action;
        }

        _player.TakeDamage(action.PurchaseCost);
    }
}
