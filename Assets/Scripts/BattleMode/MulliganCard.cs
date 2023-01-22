using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;
using System;

namespace Hearthstone
{
    public class MulliganCard : MonoBehaviour
    {
        public IEnumerator StartMulligan(Vector3 startPosition, Vector3 endPosition, Quaternion startRotation, Quaternion endRotation, float time)
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
        public async UniTaskVoid StartMulliganAsync(Vector3 startPosition, Vector3 endPosition, Quaternion startRotation, Quaternion endRotation, float time)
        {
            float currentTime = 0f;

            while (currentTime < time)
            {
                transform.SetPositionAndRotation(Vector3.Lerp(startPosition, endPosition, currentTime / time), Quaternion.Lerp(startRotation, endRotation, currentTime));
                currentTime += Time.deltaTime;
                await UniTask.Yield();
            }
            transform.SetPositionAndRotation(endPosition, endRotation);
        }
    }
}