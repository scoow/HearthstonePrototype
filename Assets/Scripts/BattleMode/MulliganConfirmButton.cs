using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class MulliganConfirmButton : Button
    {
        private Image _image;

        public void Init()
        {
            _image = GetComponent<Image>();
        }
        public void ShowButton()
        {
           _image.enabled = true;
        }
        public void HideButton()
        {
            _image.enabled = false;
        }
    }
}