using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Hearthstone
{
    public class Card : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private Camera _camera;
        private LayerRenderUp _layersRenderUp;
        private TempCard_Marker _tempCardGO;
        [SerializeField]
        private float _scale�oefficient;

        [Inject]
        private Mana_Controller _mana_Controller;
        [Inject]
        private IndicatorTarget _indicatorTarget;
        [SerializeField]
        private Players _side;
        private bool _cancelDrag;
        public Action<bool> BeginDrag;
        [SerializeField]
        private CardState _card_State;//������� ��������� ����� - � ������/� ����/�� �����
        private bool _canAttackThisTurn;

        private void Awake()
        {
            _camera = Camera.main;
            _tempCardGO = FindObjectOfType<TempCard_Marker>();
            _layersRenderUp = GetComponent<LayerRenderUp>();
            // _side = GetComponent<BattleModeCard>().GetSide();

            ChangeState(CardState.Deck);//��������� ��-���������

            _mana_Controller = FindObjectOfType<Mana_Controller>();//Zenject �� ��������. ������?
            _indicatorTarget = FindObjectOfType<IndicatorTarget>();//Zenject �� ��������. ������?
        }
        public void EnableAttack()
        {
            _canAttackThisTurn = true;
        }
        public void DisableAttack()
        {
            _canAttackThisTurn = false;
        }
        public void ChangeState(CardState newState)
        {
            _card_State = newState;
        }
        public CardState GetState()
        {
            return _card_State;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_card_State == CardState.Deck) return;
            /*            if (_card_State == CardState.Board)
                        {
                           // _indicatorTarget.CursorBattleCryOn(this.transform);
                            _cursorEnabled = true;
                            return;
                        }*/

            _cancelDrag = _side != _mana_Controller.WhoMovesNow() || GetComponent<Card_Model>().GetManaCostCard() > _mana_Controller.GetManaCount(_mana_Controller.WhoMovesNow());
            if (_cancelDrag)//���� �� ��� ��� - ������ �������� �����
            {
                Debug.Log("������ ������� ��� �����");
                return;
            }

            _tempCardGO.gameObject.transform.position = transform.position - new Vector3(0, 0, 5f); ;//��������� �����
            _layersRenderUp.LayerUp(50);

            BeginDrag?.Invoke(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            /*            if (_cursorEnabled)
                        {
                            return;
                        }*/
            if (_card_State == CardState.Deck) return;
            if (_cancelDrag)//���� �� ��� ��� - ������ �������� �����
            {
                return;
            }
            Vector3 newPosition = eventData.position;
            newPosition.z = _camera.transform.position.z;

            newPosition = -_camera.ScreenToWorldPoint(newPosition);

            newPosition.z = -1;
            transform.position = newPosition;
            //����������� ������ ���� �������� �� -1
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_card_State == CardState.Deck) return;
            if (_cancelDrag)//���� �� ��� ��� - ������ �������� �����
            {
                return;
            }
            CardHolder _parent = transform.parent.GetComponent<CardHolder>();
            _layersRenderUp.LayerUp(-50);
            if (_parent is Hand)
            {
                //transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
                transform.position = _tempCardGO.transform.position;
            }
            _tempCardGO.transform.position = new Vector3(100, 0);//������� ��������� ����� �� ������� ������
            BeginDrag?.Invoke(false);
        }
        /// <summary>
        /// ���������� ��������
        /// </summary>
        /// <param name="cardHolder">��������</param>
        public void SetParent(CardHolder cardHolder)
        {
            transform.parent = cardHolder.transform;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_card_State == CardState.Deck || _side != _mana_Controller.WhoMovesNow()) return;
            _layersRenderUp.LayerUp(50);
            transform.localScale *= _scale�oefficient;
            transform.position += new Vector3(0, 0, 5f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_card_State == CardState.Deck || _side != _mana_Controller.WhoMovesNow()) return;
            _layersRenderUp.LayerUp(-50);
            transform.localScale /= _scale�oefficient;
            transform.position -= new Vector3(0, 0, 5f);
        }

        /*public void OnDrop(PointerEventData eventData)
        {
            if (_card_State != CardState.Board) return;
            if (!eventData.pointerDrag.TryGetComponent<IndicatorTarget>(out var attacker)) return;
            //if (attacker.GetSide() == _side) return;

            Debug.Log("�����");


            Attack(attacker, this);
        }*/

        private void Attack(Card attacker, Card card)
        {
            Card_Controller card_Controller_attacker = attacker.GetComponent<Card_Controller>();
            Card_Controller card_Controller_card = card.GetComponent<Card_Controller>();

            int attacker_damage = attacker.GetComponent<Card_Model>()._atackDamageCard;
            int card_damage = card.GetComponent<Card_Model>()._atackDamageCard;

            card_Controller_attacker.ChangeHealtValue(card_damage, ChangeHealthType.DealDamage);
            card_Controller_card.ChangeHealtValue(attacker_damage, ChangeHealthType.DealDamage);

            /*BattleModeCard _attackerBattleCard = attacker.GetComponent<BattleModeCard>();
            _ = _attackerBattleCard.MoveCardAsync(attacker.transform, card.transform, 1f);
            _ = _attackerBattleCard.MoveCardAsync(card.transform, attacker.transform, 1f);*/
        }

        public void SetSide(Players side)
        {
            this._side = side;
        }
        public Players GetSide()
        {
            return this._side;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_card_State != CardState.Board) return;


            if (!_indicatorTarget.CursorEnabled)

            {
                if (_canAttackThisTurn)
                {
                    Debug.Log("��������� ������");
                    _indicatorTarget.CursorEnabled = true;
                    _indicatorTarget.SetWatcher(this.transform);
                }
                else
                {
                    Debug.Log("�� ���� ��������� �� ���� ����");
                }

            }
            else
            {
                GameObject attacker = _indicatorTarget.GetWatcher();
                Card attackercard = attacker.GetComponent<Card>();
                if (attackercard.GetSide() == _side) return;
                Debug.Log("��������� �����" + attackercard + "    " + this);
                _indicatorTarget.ChangeCursorState(false);

                attackercard.DisableAttack();
                Attack(attackercard, this);

            }
        }
    }
}