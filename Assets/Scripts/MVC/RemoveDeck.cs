using Hearthstone;
using UnityEngine;
using UnityEngine.UI;

public class RemoveDeck : MonoBehaviour
{
    public Text _currentNameDeck;
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