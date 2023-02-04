using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hearthstone
{
    public class CardHolder : MonoBehaviour
    {
        public Players _side;
        protected Dictionary<Vector3, BattleModeCard> _cardsList;//������� serializable

        protected float _offset = 2f;
        protected int _cardCount = 0;

        private void Start()
        {
            _cardsList = new();
        }
        /// <summary>
        /// �������� ������� ��������� ����� � ����
        /// </summary>
        /// <returns></returns>
        public Vector3 GetLastCardPosition()
        {
            if (_cardCount == 0)
                return transform.position;
            else
                return _cardsList.Last().Key;
        }
        /// <summary>
        /// �������� ����� � ����
        /// </summary>
        public void AddCard(BattleModeCard card)
        {
            Vector3 newPosition = GetLastCardPosition();
            newPosition.x += _offset;
            _cardsList.Add(newPosition, card);
            _cardCount++;
        }
        /// <summary>
        /// ������ ����� �� ����
        /// </summary>
        public void RemoveCard(BattleModeCard card)
        {
            if (_cardCount > 0)
            {
                // _cardsList.Remove(,)//�������� �������� �� ��������
                _cardCount--;
            }
        }
    }
}