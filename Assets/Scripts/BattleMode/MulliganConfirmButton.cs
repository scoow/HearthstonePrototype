using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class MulliganConfirmButton : Button
    {
        private Image _image;
        //private Button _button;

        public void Init()
        {
           
            _image = GetComponent<Image>();
            //_button = GetComponent<Button>();
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