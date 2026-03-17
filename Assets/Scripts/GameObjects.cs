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

// ============================================================
//  LOST PAGES — LostPage.cs
//  Прикрепи на объект страницы.
//  Требует: BoxCollider2D (Is Trigger = true)
// ============================================================
public class LostPage : MonoBehaviour
{
    public string nextSceneName = "Level_2"; // или "MainMenu"

    Vector3 startPos;
    float bobSpeed = 1.2f;

    void Start() { startPos = transform.position; }

    void Update()
    {
        transform.position = startPos + Vector3.up *
            Mathf.Sin(Time.time * bobSpeed) * 0.18f;
        transform.Rotate(0, 0, 20f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        AudioManager.Play("page");
        HUDController.ShowToast("История начинает восстанавливаться…",
                                new Color(0.23f, 0.14f, 0f));

        // Загрузить следующую сцену через 2 секунды
        Invoke(nameof(LoadNext), 2f);
        gameObject.SetActive(false);
    }

    void LoadNext()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}

// ============================================================
//  LOST PAGES — GrowTarget.cs
//  Прикрепи на растение/объект для РОСТ (Level 2).
// ============================================================
public class GrowTarget : MonoBehaviour
{
    public float growScale = 3f;
    bool grown = false;

    public void Grow()
    {
        if (grown) return;
        grown = true;
        StartCoroutine(GrowRoutine());
    }

    System.Collections.IEnumerator GrowRoutine()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale   = startScale * growScale;
        float   elapsed    = 0f;
        float   duration   = 0.6f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            yield return null;
        }
        transform.localScale = endScale;
    }
}

// ============================================================
//  LOST PAGES — InkEnemy.cs
//  Прикрепи на чернильную кляксу (Level 3).
//  Требует: BoxCollider2D (Is Trigger = true)
// ============================================================
public class InkEnemy : MonoBehaviour
{
    public float patrolRange = 3f;
    public float speed       = 1.5f;

    Vector3 startPos;
    int     dir = 1;
    SpriteRenderer sr;

    void Start()
    {
        startPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * dir * speed * Time.deltaTime);

        float dist = transform.position.x - startPos.x;
        if (dist >  patrolRange) dir = -1;
        if (dist < -patrolRange) dir =  1;

        if (sr) sr.flipX = (dir < 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>()?.Die();
        }
    }
}
