using System.Linq;
using UnityEngine;
using System.Collections;

namespace Hearthstone
{
    
    public class MulliganCard : MonoBehaviour
    {
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private Quaternion _startRotation;
        private Quaternion _endRotation;

        private float _speed = 4;
        private float _time;

        private void Start()
        {
            _endPosition = new Vector3(-6.5f, 0.75f, -5f);
            _endRotation = Quaternion.Euler(0f, 0f, 0f);

            StartCoroutine(StartMulligan(transform.position, _endPosition, transform.rotation, _endRotation, 4f));
        }
        private IEnumerator StartMulligan(Vector3 startPosition, Vector3 endPosition, Quaternion startRotation, Quaternion endRotation, float time)
        {
            float currentTime = 0f;

            while (currentTime < time)
            {
                transform.SetPositionAndRotation(Vector3.Lerp(startPosition, endPosition, currentTime / time), Quaternion.Lerp(startRotation, endRotation, currentTime));
                currentTime += Time.deltaTime;
                yield return null;
            }
            transform.SetPositionAndRotation(endPosition, endRotation);
        }
    }
}