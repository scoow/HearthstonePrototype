using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Hearthstone
{
    public class BattleModeCard : MonoBehaviour
    {
        public bool Selected { get; set; }
        /// <summary>
        /// �������������� ����� � ������
        /// </summary>
        public Players _side;
        /// <summary>
        /// ����������� �������� ����� �� �������� ����
        /// </summary>
        /// <param name="startPosition">��������� �������</param>
        /// <param name="endPosition">�������� �������</param>
        /// <param name="startRotation">��������� ��������</param>
        /// <param name="endRotation">�������� ��������</param>
        /// <param name="time">����� ��������</param>
        /// <returns></returns>
        public async UniTaskVoid MoveCardAsync(Vector3 startPosition, Vector3 endPosition, Quaternion startRotation, Quaternion endRotation, float time)
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
        public void SetSide(Players side)
        {
            this._side = side;
        }
    }
}