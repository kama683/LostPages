using UnityEngine;

public class BridgeBuilder : MonoBehaviour
{
    [Header("Префаб моста (блока)")]
    public GameObject bridgePrefab; 
    
    [Header("Можем ли мы строить? (Подняли слово)")]
    public bool hasWord = false; 

    void Update()
    {
        // Если слово поднято И игрок нажал ПРАВУЮ кнопку мыши (цифра 1)
        // (Левая кнопка - это 0, Правая - 1, Колесико - 2)
        if (hasWord && Input.GetMouseButtonDown(1)) 
        {
            BuildBridge();
        }
    }

    void BuildBridge()
    {
        // 1. Узнаем, где сейчас курсор на экране монитора
        Vector3 mouseScreenPosition = Input.mousePosition;

        // 2. Переводим эти координаты в координаты игрового мира
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        
        // 3. В 2D играх координата Z всегда должна быть нулем, иначе мост появится "за камерой"
        mouseWorldPosition.z = 0f;

        // 4. Создаем наш блок в этих координатах!
        Instantiate(bridgePrefab, mouseWorldPosition, Quaternion.identity);

        // 5. Тратим слово, чтобы нельзя было построить 100 мостов одним словом
        hasWord = false; 
    }

    // Эту функцию ты сможешь вызывать из другого скрипта, когда игрок подбирает слово
    public void PickUpWord()
    {
        hasWord = true;
        Debug.Log("Слово подобрано! Кликни правой кнопкой, чтобы построить мост.");
    }
}