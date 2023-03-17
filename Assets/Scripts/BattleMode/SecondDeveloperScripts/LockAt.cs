using UnityEngine;

namespace Hearthstone
{
    public class LockAt : MonoBehaviour
    {

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                gameObject.transform.LookAt(raycastHit.point, Vector3.up);
                Debug.Log(raycastHit.point);
            }
        }
    }
}