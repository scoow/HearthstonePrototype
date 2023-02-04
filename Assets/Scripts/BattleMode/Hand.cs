using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hearthstone
{
    public class Hand : MonoBehaviour
    {
        public Players _side;//
        //private List<ChoiseCard> choiseCards;
        private Dictionary<Vector3, BattleModeCard> _cardsList;//������� serializable

        private float _offset = 2f;
        private int _cardCount = 0;

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
        public void AddCardToHand(BattleModeCard card)
        {
            Vector3 newPosition = GetLastCardPosition();
            newPosition.x += _offset;
            _cardsList.Add(newPosition, card);
            _cardCount++;
        }
        /// <summary>
        /// ������ ����� �� ����
        /// </summary>
        public void RemoveCardFromHand(BattleModeCard card)
        {
            if (_cardCount > 0)
            {
                // _cardsList.Remove(,)//�������� �������� �� ��������
                _cardCount--;
            }
        }
    }
}