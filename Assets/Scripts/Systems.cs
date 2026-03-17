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
    public CanvasGroup     toastGroup;

    static HUDController instance;
    Coroutine toastCoroutine;

    void Awake() { instance = this; }

    public void UpdateWordDisplay(string word)
    {
        if (wordLabel)
            wordLabel.text = string.IsNullOrEmpty(word) ? "" : $"[ {word} ]";
    }

    // ---- статический вызов из любого скрипта ----
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
        toastLabel.text  = msg;
        toastLabel.color = color;
        toastGroup.alpha = 0;

        // fade in
        float t = 0;
        while (t < 0.3f) { t += Time.deltaTime; toastGroup.alpha = t / 0.3f; yield return null; }

        yield return new WaitForSeconds(1.5f);

        // fade out
        t = 0;
        while (t < 0.4f) { t += Time.deltaTime; toastGroup.alpha = 1 - t / 0.4f; yield return null; }

        toastGroup.alpha = 0;
    }
}

// ============================================================
//  LOST PAGES — CameraFollow.cs
//  Прикрепи на Main Camera.
// ============================================================
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float     smoothSpeed  = 5f;
    public float     offsetX      = 2f;    // камера чуть впереди игрока
    public float     offsetY      = 1f;

    [Header("Границы уровня")]
    public float minX = 0f;
    public float maxX = 24f;   // ширина уровня в юнитах
    public float minY = -5f;
    public float maxY = 5f;

    Camera cam;

    void Start() { cam = GetComponent<Camera>(); }

    void LateUpdate()
    {
        if (!target) return;

        float halfH = cam.orthographicSize;
        float halfW = halfH * cam.aspect;

        float tx = target.position.x + offsetX;
        float ty = target.position.y + offsetY;

        // Зажать в границы
        tx = Mathf.Clamp(tx, minX + halfW, maxX - halfW);
        ty = Mathf.Clamp(ty, minY + halfH, maxY - halfH);

        Vector3 desired  = new Vector3(tx, ty, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desired,
                                          smoothSpeed * Time.deltaTime);
    }
}

// ============================================================
//  LOST PAGES — AudioManager.cs
//  Прикрепи на пустой объект "AudioManager".
//  Заполни массив clips в инспекторе.
// ============================================================
public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundEntry
    {
        public string      name;
        public AudioClip   clip;
        [Range(0,1)] public float volume = 0.6f;
    }

    public SoundEntry[] sounds;
    static AudioManager instance;
    AudioSource src;

    void Awake()
    {
        instance = this;
        src = GetComponent<AudioSource>();
    }

    public static void Play(string name)
    {
        if (!instance) return;
        foreach (var s in instance.sounds)
        {
            if (s.name == name && s.clip != null)
            {
                instance.src.PlayOneShot(s.clip, s.volume);
                return;
            }
        }
        // Звук не найден — ничего не делаем (не ломаем игру)
    }
}

// ============================================================
//  LOST PAGES — HintTrigger.cs
//  Прикрепи на невидимый триггер-зону для показа подсказки.
//  Требует: BoxCollider2D (Is Trigger = true)
// ============================================================
public class HintTrigger : MonoBehaviour
{
    [TextArea]
    public string hintText = "Подсказка";

    bool shown = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (shown) return;
        if (!other.CompareTag("Player")) return;
        shown = true;
        HUDController.ShowToast(hintText, new Color(0.23f, 0.14f, 0f));
    }
}
