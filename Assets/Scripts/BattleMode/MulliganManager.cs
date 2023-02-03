using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Описание стадий муллигана:
1. 3 или 4 карты вылетают из колоды
2. игрок выбирает, какие хочет убрать
3. выбранные карты уходят, на их место приходят случайные из колоды
4. оставшиеся карты попадают в руку
 */

namespace Hearthstone
{
    public class MulliganManager : MonoBehaviour
    {
        [SerializeField]
        private float _time = 2f;

        [SerializeField]
        private GameObject _playerDeck;

        private List<MulliganCardPosition> _mulliganCardsPositions;//Якори для вылетающих карт
        private List<MulliganCard> _mulliganCards;//Сами карты
        private MulliganConfirmButton _mulliganConfirmButton;

        private List<Hand> _hands;
        private Hand _firstPlayerHand;
        private async void Start()
        {
            _mulliganConfirmButton = FindObjectOfType<MulliganConfirmButton>();
            _mulliganConfirmButton.Init();
            _mulliganConfirmButton.HideButton();
            _mulliganConfirmButton.onClick.AddListener(MulliganStage3);

            _hands = new List<Hand>();
            _hands = FindObjectsOfType<Hand>().ToList();
            _firstPlayerHand = _hands.Where(hand => hand._side == Players.First).FirstOrDefault();

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


            MulliganStage2();
        }

        private async void MulliganStage1()
        {
            MulliganCard _card;
            int i = 0;
            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                _card = _mulliganCards[i];
                position.SetCurrentCard(_card);//
                _ = _card.StartMulliganAsync(_card.transform.position, position.transform.position, _card.transform.rotation, position.transform.rotation, _time);
                await UniTask.Delay(TimeSpan.FromSeconds(0.5));
                i++;
            }
        }

        private void MulliganStage2()
        {
            _mulliganConfirmButton.ShowButton();
        }

        private async void MulliganStage3()
        {
            _mulliganConfirmButton.HideButton();
            MulliganCard _card;
            int i = 0;
            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                _card = _mulliganCards[i];
                if (_card.Selected)
                {
                    _ = _card.StartMulliganAsync(position.transform.position, _playerDeck.transform.position, position.transform.rotation, _playerDeck.transform.rotation, _time);
                    await UniTask.Delay(TimeSpan.FromSeconds(0.5));
                }
                i++;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(5));
            MulliganStage4();
        }
        private async void MulliganStage4()
        {
            _mulliganConfirmButton.HideButton();
            MulliganCard _card;
            int i = 0;
            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                position.SwitchRenderer(false);
                _card = _mulliganCards[i];
                if (_card.Selected)
                {
                    _ = _card.StartMulliganAsync(_card.transform.position, position.transform.position, _card.transform.rotation, position.transform.rotation, _time);
                    await UniTask.Delay(TimeSpan.FromSeconds(0.5));
                }
                i++;
            }
            i = 0;
            foreach (MulliganCardPosition position in _mulliganCardsPositions)
            {
                _card = _mulliganCards[i];
                _ = _card.StartMulliganAsync(position.transform.position, _firstPlayerHand.transform.position, position.transform.rotation, _firstPlayerHand.transform.rotation, _time);
                await UniTask.Delay(TimeSpan.FromSeconds(0.5));
                i++;
            }
        }

    }
}