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

        /// <summary>
        /// Находит нужные объекты на сцене и помещает в DI-контейнер
        /// </summary>
        public override void InstallBindings()
        {
            _pageBook_Model = FindObjectOfType<PageBook_Model>();
            _loadDeck_Controller = FindObjectOfType<LoadDeck_Controller>();
            _handManager = FindObjectOfType<HandManager>();
            _mulliganManager = FindObjectOfType<MulliganManager>();

            Container.BindInstance(_pageBook_Model).AsSingle();
            Container.BindInstance(_loadDeck_Controller).AsSingle();
            Container.BindInstance(_handManager).AsSingle();
            Container.BindInstance(_mulliganManager).AsSingle();
        }
    }
}