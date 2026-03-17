using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene")]
    public string firstLevelName = "Level_1";
    public float startDelay = 0.35f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip startClickClip;
    [Range(0f, 1f)] public float clickVolume = 0.6f;

    private bool isStarting = false;

    public void StartGame()
    {
        if (isStarting) return;
        isStarting = true;
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        if (audioSource != null && startClickClip != null)
        {
            audioSource.PlayOneShot(startClickClip, clickVolume);
        }

        yield return new WaitForSeconds(startDelay);
        SceneManager.LoadScene(firstLevelName);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
}