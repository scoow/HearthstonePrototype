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
        /// <summary>
        /// Перегруженная версия для работы с векторами
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async UniTask<bool> MoveCardAsync(Vector3 start, Vector3 end, float time)
        {
            float currentTime = 0f;

            while (currentTime < time)
            {
                transform.SetPositionAndRotation(Vector3.Lerp(start, end, currentTime / time), Quaternion.identity);
                currentTime += Time.deltaTime;
                await UniTask.Yield();
            }
            transform.SetPositionAndRotation(end, Quaternion.identity);
            return true;
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