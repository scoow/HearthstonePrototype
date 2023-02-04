using UnityEngine;

namespace Hearthstone
{
    public class EmissionMarker : MonoBehaviour
    {
        private void Start()
        {
            if(gameObject.name == "CardEmission")
            gameObject.SetActive(false);
        }
    }
}