using UnityEngine;

namespace Hearthstone
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private LoadDeck_Controller _loadDeck_Controller;
        public PageBook_Model _pageBook_Model;
        private HandManager _handManager;

        private void Awake()
        {
            instance= this;

            /*_pageBook_Model = GetComponent<PageBook_Model>();*/
            _pageBook_Model= GetComponent<PageBook_Model>();
            _pageBook_Model.enabled=true;
            _loadDeck_Controller = GetComponent<LoadDeck_Controller>();
            _loadDeck_Controller.enabled= true;
            _handManager = GetComponent<HandManager>();
            _handManager.enabled= true;

        }
    }
}