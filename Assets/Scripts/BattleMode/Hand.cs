using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hearthstone
{
    public class Hand : MonoBehaviour
    {
        public Players _side;//
        //private List<ChoiseCard> choiseCards;
        private List<Transform> _cardsList;
        //private Transform _;
        private float _offset = 2f;
        private int _cardCount=0;

        private void Start()
        {
            _cardsList = new();
            _cardsList.Add(transform);
        }
        public Vector3 GetLastCardPosition()
        {
            return _cardsList.Last().position;
        }
        public void AddCardToHand()
        {
            Vector3 newPosition = GetLastCardPosition();
            newPosition.x += _offset;

            Transform newTransform = Transform.Instantiate(transform);

            newTransform.position = newPosition;

            _cardsList.Add(newTransform); 
            _cardCount++;
        }
    }
}