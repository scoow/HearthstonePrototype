using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class RemoveDeck : MonoBehaviour
    {
        [SerializeField] private Text _currentNameDeck;
        public Text CurrentNameDeck { get => _currentNameDeck; set => _currentNameDeck = value; }
        private IRemove _remove;

        private void Awake()
        {
            _remove = FindObjectOfType<Memory_Controller>();
        }
        public void RemoveContent(GameObject destroyGameObject)
        {
            _remove.DeleteDeckInCollection(_currentNameDeck.text);
            Destroy(destroyGameObject);
        }
    }
}