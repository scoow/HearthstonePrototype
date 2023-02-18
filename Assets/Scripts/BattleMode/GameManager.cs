using Zenject;

namespace Hearthstone
{
    public class GameManager : MonoInstaller
    {
        private LoadDeck_Controller _loadDeck_Controller;
        private PageBook_Model _pageBook_Model;
        private HandManager _handManager;
        private MulliganManager _mulliganManager;

        public override void InstallBindings()
        {
            /*_pageBook_Model = GetComponent<PageBook_Model>();
            _loadDeck_Controller = GetComponent<LoadDeck_Controller>();
            _handManager = GetComponent<HandManager>();*/
            _pageBook_Model = FindObjectOfType<PageBook_Model>();
            _loadDeck_Controller = FindObjectOfType<LoadDeck_Controller>();
            _handManager = FindObjectOfType<HandManager>();
            _mulliganManager = FindObjectOfType<MulliganManager>();

            Container.BindInstance(_pageBook_Model).AsSingle();
            Container.BindInstance(_loadDeck_Controller).AsSingle();
            Container.BindInstance(_handManager).AsSingle();
            Container.BindInstance(_mulliganManager).AsSingle();
        }

        private void Awake()
        {
            /*_pageBook_Model = GetComponent<PageBook_Model>();*/
            
            _pageBook_Model.enabled=true;
            
            _loadDeck_Controller.enabled= true;
            
            _handManager.enabled= true;

        }
    }
}