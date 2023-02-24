using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Hearthstone
{
    public class BattleModeCard : MonoBehaviour
    {
        public bool Selected { get; set; }
        /// <summary>
        /// принадлежность карты к игроку
        /// </summary>
        public Players _side;
        /// <summary>
        /// Асинхронное движение карты по игровому полю
        /// </summary>
        /// <param name="startPosition">начальная позиция</param>
        /// <param name="endPosition">конечная позиция</param>
        /// <param name="startRotation">начальное вращение</param>
        /// <param name="endRotation">конечное вращение</param>
        /// <param name="time">время движения</param>
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