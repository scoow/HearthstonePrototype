using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hearthstone
{
    public class PlayButton : MonoBehaviour
    {
        private Button _playButton;
        private void Awake()
        {
            _playButton= GetComponent<Button>();
            _playButton.onClick.AddListener(EnterChooseHeroScreen);
        }
        private void EnterChooseHeroScreen()
        {
            SceneManager.LoadScene("ChooseHero");
            Time.timeScale = 1;
        }
    }
}