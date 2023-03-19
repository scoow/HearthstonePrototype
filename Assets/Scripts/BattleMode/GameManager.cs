using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Hearthstone
{
    /// <summary>
    /// Класс для внедрения зависимостей
    /// </summary>
    public class GameManager : MonoInstaller
    {
        private LoadDeck_Controller _loadDeck_Controller;
        private PageBook_Model _pageBook_Model;
        private HandManager _handManager;
        private MulliganManager _mulliganManager;
        private Mana_Controller _mana_Controller;
        private BattleCry_Controller _battleCry_Controller;
        private IndicatorTarget _indicatorTarget;

        private List<Board> _boards;
        private Board _firstPlayerBoard;
        private Board _secondPlayerBoard;

        private List<Hand> _hands;
        private Hand _firstPlayerHand;
        private Hand _secondPlayerHand;

        private Hero_Controller _firstPlayerHero;
        private Hero_Controller _secondPlayerHero;

        private TempCard_Marker _tempCardGO;
        private TempMinion_Marker _tempMinionGO;

        private EndTurnButton _endTurnButton;
        /// <summary>
        /// Находит нужные объекты на сцене и помещает в DI-контейнер
        /// </summary>
        public override void InstallBindings()
        {
            //секция поиска объектов на сцене
            _pageBook_Model = FindObjectOfType<PageBook_Model>();
            _loadDeck_Controller = FindObjectOfType<LoadDeck_Controller>();
            _handManager = FindObjectOfType<HandManager>();
            _mulliganManager = FindObjectOfType<MulliganManager>();
            _mana_Controller = FindObjectOfType<Mana_Controller>();
            _battleCry_Controller = FindObjectOfType<BattleCry_Controller>();
            _indicatorTarget = FindObjectOfType<IndicatorTarget>();

            _boards = FindObjectsOfType<Board>().ToList();
            _firstPlayerBoard = _boards.Where(board => board._side == Players.First).FirstOrDefault();
            _secondPlayerBoard = _boards.Where(board => board._side == Players.Second).FirstOrDefault();

            _hands = new List<Hand>();
            _hands = FindObjectsOfType<Hand>().ToList();
            _firstPlayerHand = _hands.Where(hand => hand._side == Players.First).FirstOrDefault();
            _secondPlayerHand = _hands.Where(hand => hand._side == Players.Second).FirstOrDefault();

            _firstPlayerHero = FindObjectsOfType<Hero_Controller>().First(hero => hero.Side == Players.First);
            _secondPlayerHero = FindObjectsOfType<Hero_Controller>().First(hero => hero.Side == Players.Second);

            _tempCardGO = FindObjectOfType<TempCard_Marker>();
            _tempMinionGO = FindObjectOfType<TempMinion_Marker>();

            _endTurnButton = FindObjectOfType<EndTurnButton>();
            //секция внедрения ссылок на объекты в контейнер Zenject
            Container.BindInstance(_pageBook_Model).AsSingle();
            Container.BindInstance(_loadDeck_Controller).AsSingle().NonLazy();
            Container.BindInstance(_handManager).AsSingle();
            Container.BindInstance(_mulliganManager).AsSingle();
            Container.BindInstance(_mana_Controller).AsSingle();
            Container.BindInstance(_battleCry_Controller).AsSingle();
            Container.BindInstance(_indicatorTarget).AsSingle();

            Container.BindInstance(_firstPlayerBoard).WithId("First");
            Container.BindInstance(_secondPlayerBoard).WithId("Second");
            Container.BindInstance(_firstPlayerHand).WithId("First");
            Container.BindInstance(_secondPlayerHand).WithId("Second");
            Container.BindInstance(_firstPlayerHero).WithId("First");
            Container.BindInstance(_secondPlayerHero).WithId("Second");

            Container.BindInstance(_tempCardGO).AsSingle();
            Container.BindInstance(_tempMinionGO).AsSingle();

            Container.BindInstance(_endTurnButton).AsSingle();
        }
/*        public void InjectCards()
        {
            List<Card> cards = new();
            cards = FindObjectsOfType<Card>().ToList();
            foreach (Card card in cards)
            {
                DiContainer.Inject(_mana_Controller, cards);
            }
        }*/
    }
}