using Hearthstone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEffect_Controller : MonoBehaviour
{
    private Mana_Controller _manaController;
    private PageBook_Model _pageBook_Model;
    //private int _lastEffect;
    [SerializeField] private Transform _playerBoard;
    [SerializeField] private Transform _enemyBoard;


    /// <summary>
    /// ������ �������� �� �������
    /// </summary>
    public List<int> _activeEventEffect;

    private void OnEnable()
    {
        _pageBook_Model = FindObjectOfType<PageBook_Model>();
        _manaController = FindObjectOfType<Mana_Controller>();
        _manaController.OnChangeTurn += ParseChangeTurnEvent;
    }

    private void OnDisable()
    {
        _manaController.OnChangeTurn -= ParseChangeTurnEvent;
    }

    public void AddEventEffect(int cardId) //�������� ������ � ������
    {
        _activeEventEffect.Add(cardId);
    }

    public void RemoveEventEffect(int cardId) //������� ������ �� ������
    {
        _activeEventEffect.Remove(cardId);
    }


    /// <summary>
    /// ��������� ������� �������
    /// </summary>
    /// <param name="cardExample"></param>
    public void ParseHealEvent(ApplyBattleCry cardExample)
    {

        if (_activeEventEffect != null)
        {
            foreach (int cardEffectId in _activeEventEffect)
            {   
                if (cardEffectId == 106)
                {
                    cardExample.gameObject.GetComponent<Card_Controller>().TakeAdditionalCard();
                }             
            }
        }
    }

    /// <summary>
    /// ��������� ������� �����
    /// </summary>
    public void ParseDamageEvent()
    {
        Card_Controller[] _cardControllerArray = _playerBoard.GetComponentsInChildren<Card_Controller>();

        if (_activeEventEffect != null)
        {
            foreach (int cardEffectId in _activeEventEffect) //������ ���� ������ ����� ��������
            {
                if (cardEffectId == 310) // ���� ���� ����� ������
                {
                    foreach(Card_Controller cardController in _cardControllerArray)
                    {
                        if(cardController.GetComponent<Card_Model>()._idCard == cardEffectId) //�� ������� ����� ������� � ����� � ����� �� Id
                        {
                            cardController.BerserkAbility();
                        }
                    }                    
                }
            }
        }
    }

    /// <summary>
    /// ��������� ������� ������
    /// </summary>
    public void ParseDeathEvent(Card_Controller cardExample)
    {
        Card_Controller[] _cardControllerArray = _playerBoard.GetComponentsInChildren<Card_Controller>(); //��� ����� �� �����
        MinionType incomingMinionType = cardExample.gameObject.GetComponent<Card_Model>()._minionType; //��� ������� ���������� �������
        if (_activeEventEffect != null)
        {
            foreach (int cardEffectId in _activeEventEffect) //������ ���� ������ ����� ��������
            {
                if (cardEffectId == 212) // ���� ���� ����� ������
                {
                    foreach (Card_Controller cardController in _cardControllerArray)
                    { 
                        Card_Model cardModel = cardController.GetComponent<Card_Model>();
                        if (cardModel._idCard == cardEffectId && cardModel._minionType == incomingMinionType) //�� ������� ����� ���������� ����������
                        {
                            cardController.ChangeAtackValue(cardModel._changeAtackValue);
                            cardController.ChangeHealtValue(cardModel._�hangeHealthValue);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// ��������� ������� ��������� ����� �� �����
    /// </summary>
    public void ParsePutCardInBoard(Card_Controller cardExample)
    {
        Card_Controller[] _cardControllerArray = _playerBoard.GetComponentsInChildren<Card_Controller>(); //��� ����� �� �����
        MinionType incomingMinionType = cardExample.gameObject.GetComponent<Card_Model>()._minionType; //��� ������� ���������� �������
        if (_activeEventEffect != null)
        {
            foreach (int cardEffectId in _activeEventEffect) //������ ���� ������ ����� ��������
            {
                if (cardEffectId == 213) // ���� ���� ����� ������
                {
                    foreach (Card_Controller cardController in _cardControllerArray)
                    {
                        Card_Model cardModel = cardController.GetComponent<Card_Model>();
                        if (cardModel._idCard == cardEffectId && cardModel._minionType == incomingMinionType && cardModel.gameObject != cardExample.gameObject)
                        {
                            cardController.TakeAdditionalCard();
                        }
                    }
                }
            }
            /*
             ������ ����� �����
            ������� ������� - ���� ������� ������ 213 � ��� �����, �� ����� �����
             */
        }
    }


    public void ParseChangeTurnEvent(Players playersTurn)
    {
        Card_Model[] _cardControllerArray = _playerBoard.GetComponentsInChildren<Card_Model>(); //��� ����� �� �����
        if(_cardControllerArray == null) return;
        

        foreach(Card_Model card_Model1 in _cardControllerArray)
        {            
            if(card_Model1._idCard == 211 && ((playersTurn == Players.First && card_Model1.transform.parent == _playerBoard) || (playersTurn == Players.Second && card_Model1.transform.parent == _enemyBoard)))
            {

                List<Card_Model> tempListCardModel = new List<Card_Model>(); //��������� ���� �������
                foreach (Card_Model card_Model2 in _cardControllerArray)
                {                    
                    if(card_Model2._healthCard < card_Model2._maxHealtValue)
                    {
                        tempListCardModel.Add(card_Model2);
                    }
                    
                }
                int indexWounded = Random.Range(0, tempListCardModel.Count); //�������� ������������
                tempListCardModel[indexWounded].GetComponent<Card_Controller>().ChangeHealtValue(card_Model1._�hangeHealthValue, ChangeHealthType.Healing); //����� ���

            }
        }
        
    }
}