using Cysharp.Threading.Tasks;
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
        private SoundEffect_Controller _soundEffect_Controller;
        private BattleCry_Controller _battleCry_Controller;
        private VoiceHero_Controller _voiceHero_Controller;

        [Inject]
        private Mana_Controller _mana_Controller;
        [Inject]
        private IndicatorTarget _indicatorTarget;
        public Players _side; //������ �� public . ���������
        private bool _cancelDrag;
        public Action<bool> BeginDrag;
        [SerializeField]
        private CardState _card_State;//������� ��������� ����� - � ������/� ����/�� �����
        private bool _canAttackThisTurn = true;

        private void Awake()
        {
            _camera = Camera.main;
            _tempCardGO = FindObjectOfType<TempCard_Marker>();
            _layersRenderUp = GetComponent<LayerRenderUp>();

            ChangeState(CardState.Deck);//��������� ��-���������

            _mana_Controller = FindObjectOfType<Mana_Controller>();//Zenject �� �������� ��� ��� ����� ��������������� ����� ������ Monoinstaller'�
            _indicatorTarget = FindObjectOfType<IndicatorTarget>();
            _soundEffect_Controller = FindObjectOfType<SoundEffect_Controller>();
            _battleCry_Controller = FindObjectOfType<BattleCry_Controller>();
            _voiceHero_Controller = FindObjectOfType<VoiceHero_Controller>();
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
            if (_card_State != CardState.Hand) return;

            _cancelDrag = _side != _mana_Controller.WhoMovesNow() || GetComponent<Card_Model>().GetManaCostCard() > _mana_Controller.GetManaCount(_mana_Controller.WhoMovesNow());
            if (_cancelDrag)//���� �� ��� ��� - ������ �������� �����
            {
                _voiceHero_Controller.PlayVoiceHero(TalkExample.ErrorUseCard, _side);
                Debug.Log("������ ������� ��� �����");
                return;
            }
            _tempCardGO.gameObject.transform.position = transform.position - new Vector3(0, 0, 5f); ; //��������� �����
            _layersRenderUp.LayerUp(50);
            BeginDrag?.Invoke(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_card_State != CardState.Hand) return;
            if (_cancelDrag)//���� �� ��� ��� - ������ �������� �����
            {
                return;
            }
            Vector3 newPosition = eventData.position;
            newPosition.z = _camera.transform.position.z;

            newPosition = -_camera.ScreenToWorldPoint(newPosition);

            newPosition.z = -1;
            transform.position = newPosition;
            //���� �������� �� -1, ��� ��� ������ ��������� � ������������� ����������� �� ��� z
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
                transform.position = _tempCardGO.transform.position + new Vector3(0, 0, 5f);
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
            if (_card_State == CardState.Deck || _card_State == CardState.Mulligan || _side != _mana_Controller.WhoMovesNow()) return;
            _layersRenderUp.LayerUp(50);
            _soundEffect_Controller.PlaySound(_soundEffect_Controller.CardShrink);
            transform.localScale *= _scale�oefficient;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_card_State == CardState.Deck || _card_State == CardState.Mulligan || _side != _mana_Controller.WhoMovesNow()) return;
            _layersRenderUp.LayerUp(-50);
            transform.localScale /= _scale�oefficient;
        }
        /// <summary>
        /// �������� ������� ������ ��������
        /// </summary>
        /// <param name="attacker">���������</param>
        /// <param name="card">���������</param>
        public async void Attack(Card attacker, Card card)
        {            
            BattleModeCard _attackerBattleCard = attacker.GetComponent<BattleModeCard>();
            Vector3 oldPosition = attacker.transform.position;
            _ = _attackerBattleCard.MoveCardAsync(attacker.transform, card.transform, 1f);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            _ = _attackerBattleCard.MoveCardAsync(card.transform.position, oldPosition, 1f);

            Card_Controller card_Controller_attacker = attacker.GetComponent<Card_Controller>();
            Card_Controller card_Controller_card = card.GetComponent<Card_Controller>();

            int attacker_damage = attacker.GetComponent<Card_Model>().AtackDamageCard;
            int card_damage = card.GetComponent<Card_Model>().AtackDamageCard;

            Debug.Log(attacker.GetComponent<Card_Model>().NameCard + " �������� " + card.GetComponent<Card_Model>().NameCard);
            
            card_Controller_attacker.ChangeHealthValue(card_damage, ChangeHealthType.DealDamage);
            card_Controller_card.ChangeHealthValue(attacker_damage, ChangeHealthType.DealDamage);
        }
        /// <summary>
        /// �������� ������� �����
        /// </summary>
        /// <param name="attacker">���������</param>
        /// <param name="hero">���������</param>
        public async void Attack(Card attacker, Hero_Controller hero)
        {
            BattleModeCard _attackerBattleCard = attacker.GetComponent<BattleModeCard>();
            Vector3 oldPosition = attacker.transform.position;
            _ = _attackerBattleCard.MoveCardAsync(attacker.transform, hero.transform, 0.5f);//todo ������� � ��������
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            _ = _attackerBattleCard.MoveCardAsync(hero.transform.position, oldPosition, 1f);

            Hero_Controller hero_Controller_card = hero.GetComponent<Hero_Controller>();

            int attacker_damage = attacker.GetComponent<Card_Model>().AtackDamageCard;
            hero_Controller_card.ChangeHealthValue(attacker_damage, ChangeHealthType.DealDamage);

            Debug.Log(attacker.GetComponent<Card_Model>().NameCard + " �������� �����");
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
            if (_battleCry_Controller.IsActiveCry) return;

            if (!_indicatorTarget.CursorEnabled && !_battleCry_Controller.IsActiveCry)//�������� �������
            {
                if (_canAttackThisTurn && _side == _mana_Controller.WhoMovesNow())
                {
                    Debug.Log("��������� ������");
                    _indicatorTarget.SetWatcher(GetComponent<Card_Model>());
                    /*Debug.Log(transform.position);
                    Debug.Log(GetComponent<Card_Model>().NameCard);*/
                    _indicatorTarget.ChangeCursorState(true);
                    
                }
                else
                {
                    _voiceHero_Controller.PlayVoiceHero(TalkExample.AtackNextTurn, _side);
                    Debug.Log("�� ���� ��������� �� ���� ����");
                }
            }
            else
            {
                Board board = transform.parent.GetComponent<Board>();
                if (board.HasMinionWithTaunt())
                {
                    Card_Model card_Model = GetComponent<Card_Model>();
                    if (!card_Model._isProvocation)
                    {
                        _voiceHero_Controller.PlayVoiceHero(TalkExample.AtackTaunt, _side);
                        Debug.Log("����� ��������� ������ �����������");
                        return;
                    }
                }

                GameObject attacker = _indicatorTarget.GetWatcher();
                Card attackercard = attacker.GetComponent<Card>();
                if (attackercard.GetSide() == _side) return;

                _indicatorTarget.ChangeCursorState(false);
                attackercard.DisableAttack();
                Attack(attackercard, this);                
            }
        }
    }
}