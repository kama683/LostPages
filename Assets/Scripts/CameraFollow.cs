using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("За кем следить?")]
    public Transform target; 

    [Header("Настройки камеры")]
    public float smoothSpeed = 5f; // Насколько плавно камера догоняет игрока
    public Vector3 offset = new Vector3(0f, 1f, -10f); // Смещение (обязательно Z = -10 для 2D!)

    // Мы используем LateUpdate вместо Update. 
    // Это нужно, чтобы камера двигалась строго ПОСЛЕ того, как сдвинулся игрок.
    // Если использовать Update, камера может "дергаться".
    void LateUpdate()
    {
        if (target != null)
        {
            // Высчитываем точку, где ДОЛЖНА быть камера (позиция игрока + смещение)
            Vector3 desiredPosition = target.position + offset;

            // Плавно передвигаем камеру из текущей точки в нужную
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            
            // Применяем новые координаты
            transform.position = smoothedPosition;
        }
    }
}