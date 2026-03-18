using UnityEngine;
using TMPro;
using System.Collections;

// ============================================================
//  LOST PAGES — HUDController.cs
//  Прикрепи на GameObject "HUD" (дочерний Canvas).
// ============================================================
public class HUDController : MonoBehaviour
{
    [Header("Текст выбранного слова")]
    public TextMeshProUGUI wordLabel;        // "[ МОСТ ]"

    [Header("Toast (всплывающее сообщение)")]
    public TextMeshProUGUI toastLabel;
    public CanvasGroup toastGroup;

    static HUDController instance;
    Coroutine toastCoroutine;

    void Awake() { instance = this; }

    public void UpdateWordDisplay(string word)
    {
        if (wordLabel)
            wordLabel.text = string.IsNullOrEmpty(word) ? "" : $"[ {word} ]";
    }

    public static void ShowToast(string msg, Color color)
    {
        if (instance) instance.DoToast(msg, color);
    }

    void DoToast(string msg, Color color)
    {
        if (toastCoroutine != null) StopCoroutine(toastCoroutine);
        toastCoroutine = StartCoroutine(ToastRoutine(msg, color));
    }

    IEnumerator ToastRoutine(string msg, Color color)
    {
        toastLabel.text = msg;
        toastLabel.color = color;
        toastGroup.alpha = 0;

        float t = 0;
        while (t < 0.3f)
        {
            t += Time.deltaTime;
            toastGroup.alpha = t / 0.3f;
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        t = 0;
        while (t < 0.4f)
        {
            t += Time.deltaTime;
            toastGroup.alpha = 1 - t / 0.4f;
            yield return null;
        }

        toastGroup.alpha = 0;
    }
}