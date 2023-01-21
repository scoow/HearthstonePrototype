using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hearthstone
{
    public class MulliganManager : MonoBehaviour
    {
        [SerializeField]
        private float _time = 2f;

        private List<MulliganCardPosition> _mulliganCardsPositions;//якори дл€ вылетающих карт
        private List<MulliganCard> _mulliganCards;//—ами карты
        private void Start()
        {
            _mulliganCardsPositions = new List<MulliganCardPosition>();
            _mulliganCardsPositions = FindObjectsOfType<MulliganCardPosition>().ToList();

            _mulliganCards = new List<MulliganCard>();
            _mulliganCards = FindObjectsOfType<MulliganCard>().ToList();
            foreach (var card in _mulliganCards)
                card.gameObject.SetActive(true);

            MulliganStage1();
        }

        private void MulliganStage1()
        {
            MulliganCard _card;
            int i = 0;
            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                _card = _mulliganCards[i];
                StartCoroutine(_card.StartMulligan(_card.transform.position, position.transform.position, _card.transform.rotation, position.transform.rotation, _time));
                i++;
            }
        }
        private void MulliganStage3()
        {
            MulliganCard _card;
            int i = 0;
            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                _card = _mulliganCards[i];
                StartCoroutine(_card.StartMulligan(_card.transform.position, position.transform.position, _card.transform.rotation, position.transform.rotation, _time));
                i++;
            }
        }
        public IEnumerator Wait(float time)
        {
            yield return new WaitForSeconds(time);
        }
    }
}