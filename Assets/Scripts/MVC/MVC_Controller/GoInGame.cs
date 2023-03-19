using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Hearthstone
{
    public class GoInGame : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            StartBattleScene();
        }
        private void StartBattleScene()
        {
            SceneManager.LoadScene("BattleScene");
        }
    }
}