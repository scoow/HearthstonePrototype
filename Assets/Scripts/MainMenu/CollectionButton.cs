using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hearthstone
{
    public class CollectionButton : MonoBehaviour
    {
        private Button _collectionButton;
        private void Awake()
        {
            _collectionButton = GetComponent<Button>();
            _collectionButton.onClick.AddListener(EnterCollection);
        }
        private void EnterCollection()
        {
            SceneManager.LoadScene("DeckColection");
            Time.timeScale = 1;
        }
    }
}