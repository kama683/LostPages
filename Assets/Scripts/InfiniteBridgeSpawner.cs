using UnityEngine;

public class InfiniteBridgeSpawner : MonoBehaviour
{
    [Header("Префаб моста (блока)")]
    public GameObject bridgePrefab; 
    
    [Header("Можем ли мы строить? (Подняли слово)")]
    public bool hasWord = false; 

    void Update()
    {
        // Если слово поднято И нажата правая кнопка мыши (1)
        if (hasWord && Input.GetMouseButtonDown(1)) 
        {
            BuildBridge();
        }
    }

    void BuildBridge()
    {
        // Узнаем позицию мыши и переводим в координаты мира
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f;

        // Создаем мост
        Instantiate(bridgePrefab, mouseWorldPosition, Quaternion.identity);
    }

    // Вызываем при подборе слова
    public void PickUpWord()
    {
        hasWord = true;
        Debug.Log("Слово подобрано! Кликай ПКМ для бесконечной стройки.");
    }
}