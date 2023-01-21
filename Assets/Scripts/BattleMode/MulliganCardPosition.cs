using UnityEngine;

namespace Hearthstone
{
    public class MulliganCardPosition : MonoBehaviour
    {
        [SerializeField]
        private MulliganCardPositionEnum _position;
        [SerializeField]
        private bool _selected;
    }

    public enum MulliganCardPositionEnum
    {
        First,
        Second,
        Third,
        Fourth
    }
}