using UnityEngine;
using TMPro;

// ============================================================
//  LOST PAGES — WordPickup.cs
//  Прикрепи на объект-слово (платформа с текстом).
//  Требует: BoxCollider2D (Is Trigger = true)
// ============================================================
public class WordPickup : MonoBehaviour
{
    [Header("Слово для выдачи игроку")]
    public string word = "МОСТ";   // или BRIDGE, РОСТ, GROW и т.д.

    WordSystem ws;
    bool collected = false;

    // Для покачивания
    Vector3 startPos;
    float bobSpeed = 1.8f;
    float bobHeight = 0.12f;

    void Start()
    {
        ws = FindObjectOfType<WordSystem>();
        startPos = transform.position;
    }

    void Update()
    {
        if (!collected)
            transform.position = startPos + Vector3.up *
                Mathf.Sin(Time.time * bobSpeed) * bobHeight;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;
        ws.PickupWord(word);
        gameObject.SetActive(false); // скрываем, не уничтожаем (для рестарта)
    }
}