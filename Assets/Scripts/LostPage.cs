using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostPage : MonoBehaviour
{
    public string nextSceneName = "Pages";
    public string nextLevelAfterPages = "Level_2";
    public float delayBeforeLoad = 4f;

    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;

        if (collision.CompareTag("Player"))
        {
            collected = true;

            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
                playerController.enabled = false;

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
            }

            if (WordSystem.Instance != null)
            {
                WordSystem.Instance.ShowTemporaryHint("История восстановлена", 2f);
            }

            if (LevelCompleteUI.Instance != null)
            {
                LevelCompleteUI.Instance.ShowRestorePanel();
            }

            // открываем ещё один закрытый кусок
            int unlockedClosedPieces = PlayerPrefs.GetInt("UnlockedClosedPieces", 0);
            unlockedClosedPieces = Mathf.Clamp(unlockedClosedPieces + 1, 0, 3);
            PlayerPrefs.SetInt("UnlockedClosedPieces", unlockedClosedPieces);

            // запоминаем, какой уровень запускать дальше из Pages
            PlayerPrefs.SetString("NextLevel", nextLevelAfterPages);
            PlayerPrefs.Save();

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.enabled = false;

            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
                col.enabled = false;

            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene(nextSceneName);
    }
}