using UnityEngine;

namespace Hearthstone
{
    public class GizmosCard : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position, new Vector3(2f, 3.5f, 0.1f));
        }
    }
}