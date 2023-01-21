using UnityEngine;

namespace Hearthstone
{
    public class MulliganCardPosition : MonoBehaviour
    {
        [SerializeField]
        private MulliganCardPositionEnum _position;
    }

    public enum MulliganCardPositionEnum
    {
        First,
        Second,
        Third,
        Fourth
    }
}