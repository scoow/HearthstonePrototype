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
        public async UniTaskVoid MoveCardAsync(Transform start, Transform end, float time)
        {
            float currentTime = 0f;

            while (currentTime < time)
            {
                transform.SetPositionAndRotation(Vector3.Lerp(start.position, end.position, currentTime / time), Quaternion.Lerp(start.rotation, end.rotation, currentTime));
                currentTime += Time.deltaTime;
                await UniTask.Yield();
            }
            transform.SetPositionAndRotation(end.position, end.rotation);
        }
        public void SetSide(Players side)
        {
            this._side = side;
        }
        public Players GetSide()
        {
            return this._side;
        }
    }
}