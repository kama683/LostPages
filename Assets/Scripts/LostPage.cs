using UnityEngine;

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