using Cysharp.Threading.Tasks;
using System;
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

        [SerializeField]
        private GameObject _playerDeck;


        private List<MulliganCardPosition> _mulliganCardsPositions;//якори дл€ вылетающих карт
        private List<MulliganCard> _mulliganCards;//—ами карты
        private async void Start()
        {
            _mulliganCardsPositions = new List<MulliganCardPosition>();
            _mulliganCardsPositions = FindObjectsOfType<MulliganCardPosition>().ToList();
            _mulliganCardsPositions.Sort((c1, c2) => string.Compare(c1.gameObject.name, c2.gameObject.name));//todo улучшить сортировку

            _mulliganCards = new List<MulliganCard>();
            _mulliganCards = FindObjectsOfType<MulliganCard>().ToList();
            _mulliganCards.Sort((c1, c2) => string.Compare(c1.gameObject.name, c2.gameObject.name));
            foreach (var card in _mulliganCards)
                card.gameObject.SetActive(true);

            MulliganStage1();
            await UniTask.Delay(TimeSpan.FromSeconds(5));
            MulliganStage3();
        }

        private async void MulliganStage1()
        {
            MulliganCard _card;
            int i = 0;
            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                _card = _mulliganCards[i];
                _ = _card.StartMulliganAsync(_card.transform.position, position.transform.position, _card.transform.rotation, position.transform.rotation, _time);
                await UniTask.Delay(TimeSpan.FromSeconds(0.5));
                i++;
            }
        }

        private async void MulliganStage3()
        {
            MulliganCard _card;
            int i = 0;
            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                _card = _mulliganCards[i];
                _ = _card.StartMulliganAsync(position.transform.position, _playerDeck.transform.position, position.transform.rotation, _playerDeck.transform.rotation, _time);
                await UniTask.Delay(TimeSpan.FromSeconds(0.5));
                i++;
            }
        }
        public IEnumerator Wait(float time)
        {
            yield return new WaitForSeconds(time);
        }
    }
}