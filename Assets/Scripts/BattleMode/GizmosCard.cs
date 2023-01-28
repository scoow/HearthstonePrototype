using UnityEngine;

namespace Hearthstone
{
    public class GizmosCard : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position, new Vector3(2.5f, 4f, 0.1f));
        }
    }
}