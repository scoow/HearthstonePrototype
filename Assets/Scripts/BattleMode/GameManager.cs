using System.Collections.Generic;
using System.Linq;
using UnityEngine.XR;
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

        private List<Board> _boards;
        private Board _firstPlayerBoard;
        private Board _secondPlayerBoard;

        private List<Hand> _hands;
        private Hand _firstPlayerHand;
        private Hand _secondPlayerHand;
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

            _boards = FindObjectsOfType<Board>().ToList();
            _firstPlayerBoard = _boards.Where(board => board._side == Players.First).FirstOrDefault();
            _secondPlayerBoard = _boards.Where(board => board._side == Players.Second).FirstOrDefault();

            _hands = new List<Hand>();
            _hands = FindObjectsOfType<Hand>().ToList();
            _firstPlayerHand = _hands.Where(hand => hand._side == Players.First).FirstOrDefault();
            _secondPlayerHand = _hands.Where(hand => hand._side == Players.Second).FirstOrDefault();

            //секция внедрения ссылок на объекты в контейнер Zenject
            Container.BindInstance(_pageBook_Model).AsSingle();
            Container.BindInstance(_loadDeck_Controller).AsSingle();
            Container.BindInstance(_handManager).AsSingle();
            Container.BindInstance(_mulliganManager).AsSingle();
            Container.BindInstance(_mana_Controller).AsSingle();

            Container.BindInstance(_firstPlayerBoard).WithId("First");
            Container.BindInstance(_secondPlayerBoard).WithId("Second");
            Container.BindInstance(_firstPlayerHand).WithId("First");
            Container.BindInstance(_secondPlayerHand).WithId("Second");
        }
    }
}