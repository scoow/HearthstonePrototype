using UnityEngine;

namespace Hearthstone
{
    public class HandManager : MonoBehaviour
    {
        private LoadDeck_Controller _loadDeckController;
        private Transform _playerDeck;
        [SerializeField]
        private GameObject _cardPrefab;
        private void Start()
        {
            _loadDeckController = FindObjectOfType<LoadDeck_Controller>();
            _playerDeck = GameObject.Find("PlayerDeck").transform;

        }
    }
}