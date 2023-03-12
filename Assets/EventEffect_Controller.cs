using Hearthstone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEffect_Controller : MonoBehaviour
{
    private Mana_Controller _manaController;
    private PageBook_Model _pageBook_Model;
    //private int _lastEffect;
    [SerializeField] private Transform _playerFirstBoard;
    [SerializeField] private Transform _playerSecondBoard;


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
        Card_Controller[] _cardPlayerFirst = _playerFirstBoard.GetComponentsInChildren<Card_Controller>(); //��� ����� �� ����� ������� ������
        Card_Controller[] _cardlayerSecond = _playerSecondBoard.GetComponentsInChildren<Card_Controller>(); //��� ����� �� ����� ������� ������

        if (_activeEventEffect != null)
        {
            foreach (int cardEffectId in _activeEventEffect) //������ ���� ������ ����� ��������
            {
                if (cardEffectId == 310) // ���� ���� ����� ������
                {
                    foreach(Card_Controller cardController in _cardPlayerFirst)
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
        Card_Controller[] _cardPlayerFirst = _playerFirstBoard.GetComponentsInChildren<Card_Controller>(); //��� ����� �� ����� ������� ������
        Card_Controller[] _cardlayerSecond = _playerSecondBoard.GetComponentsInChildren<Card_Controller>(); //��� ����� �� ����� ������� ������
        MinionType incomingMinionType = cardExample.gameObject.GetComponent<Card_Model>()._minionType; //��� ������� ���������� �������
        if (_activeEventEffect != null)
        {
            foreach (int cardEffectId in _activeEventEffect) //������ ���� ������ ����� ��������
            {
                if (cardEffectId == 212) // ���� ���� ����� ������
                {
                    foreach (Card_Controller cardController in _cardPlayerFirst)
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
        Card_Controller[] _cardPlayerFirst = _playerFirstBoard.GetComponentsInChildren<Card_Controller>(); //��� ����� �� ����� ������� ������
        Card_Controller[] _cardlayerSecond = _playerSecondBoard.GetComponentsInChildren<Card_Controller>(); //��� ����� �� ����� ������� ������

        MinionType incomingMinionType = cardExample.gameObject.GetComponent<Card_Model>()._minionType; //��� ������� ���������� �������
        if (_activeEventEffect != null)
        {
            foreach (int cardEffectId in _activeEventEffect) //������ ���� ������ ����� ��������
            {
                if (cardEffectId == 213) // ���� ���� ����� ������
                {
                    foreach (Card_Controller cardController in _cardPlayerFirst)
                    {
                        Card_Model cardModel = cardController.GetComponent<Card_Model>();
                        if (cardModel._idCard == cardEffectId && cardModel._minionType == incomingMinionType && cardModel.gameObject != cardExample.gameObject)
                        {
                            cardController.TakeAdditionalCard();
                        }
                    }
                }

                if (cardEffectId == 501)
                {
                    CardSO_Model card_Model = (CardSO_Model)_pageBook_Model._cardsDictionary[cardEffectId];
                    foreach (Card_Controller cardController in _cardPlayerFirst)
                    {
                        cardController.ChangeHealtValue(card_Model._abilityChangeHealth,ChangeHealthType.Healing);                        
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
        Card_Controller[] _cardPlayerFirst = _playerFirstBoard.GetComponentsInChildren<Card_Controller>(); //��� ����� �� ����� ������� ������
        Card_Controller[] _cardlayerSecond = _playerSecondBoard.GetComponentsInChildren<Card_Controller>(); //��� ����� �� ����� ������� ������
        if (_cardPlayerFirst == null) return;
        

        foreach(Card_Controller cardModel1 in _cardPlayerFirst)
        {     
            Card_Model card_Model1 = cardModel1.GetComponent<Card_Model>();
            if (card_Model1._idCard == 211 && ((playersTurn == Players.First && card_Model1.transform.parent == _playerFirstBoard) || (playersTurn == Players.Second && card_Model1.transform.parent == _playerSecondBoard)))
            {

                List<Card_Model> tempListCardModel = new List<Card_Model>(); //��������� ���� �������
                foreach (Card_Controller cardModel2 in _cardPlayerFirst)
                {
                    Card_Model card_Model2 = cardModel2.GetComponent<Card_Model>();
                    if (card_Model2._healthCard < card_Model2._maxHealtValue)
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