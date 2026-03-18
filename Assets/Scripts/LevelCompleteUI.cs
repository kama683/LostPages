using UnityEngine;
using System.Collections;

public class LevelCompleteUI : MonoBehaviour
{
    public static LevelCompleteUI Instance;
    public GameObject restorePanel;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        Instance = this;

        canvasGroup = restorePanel.GetComponent<CanvasGroup>();
        restorePanel.SetActive(false);
    }

    public void ShowRestorePanel()
    {
        restorePanel.SetActive(true);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = t;
            yield return null;
        }
    }
}