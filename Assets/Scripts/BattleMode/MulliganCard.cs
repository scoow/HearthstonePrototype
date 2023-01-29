using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Hearthstone
{
    public class MulliganCard : MonoBehaviour
    {
        public bool Selected { get; set; }

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