using System.Collections;
using UnityEngine;
using TMPro;

public class WordSystem : MonoBehaviour
{
    public static WordSystem Instance;

    public TextMeshProUGUI hintText;
    public TextMeshProUGUI currentWordText;

    public GameObject bridgePrefab;
    public Transform bridgeContainer;
    public LayerMask bridgeLayer;

    private string currentWord = "";
    private Camera mainCam;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mainCam = Camera.main;

        if (hintText != null)
            hintText.text = "";

        if (currentWordText != null)
            currentWordText.text = "";
    }

    private void Update()
    {
        if (currentWord == "BRIDGE" && Input.GetMouseButtonDown(0))
        {
            TryPlaceBridge();
        }
    }

   public void PickupWord(string wordName)
    {
        currentWord = wordName;

        if (currentWordText != null)
            currentWordText.text = "[ " + currentWord + " ]";

        if (hintText != null)
        {
            // Заодно я немного изменил текст подсказки, чтобы он напоминал про правую кнопку!
            hintText.text = "Слово " + currentWord + " подобрано! Кликни ПРАВОЙ кнопкой мыши.";
            StartCoroutine(HideHintAfterSeconds(3f));
        }

        // --- САМОЕ ГЛАВНОЕ: Ищем BridgeBuilder и включаем галочку! ---
        FindAnyObjectByType<BridgeBuilder>().PickUpWord();
    }
    public void ShowTemporaryHint(string message, float duration)
{
    if (hintText != null)
    {
        hintText.text = message;
        StartCoroutine(HideHintAfterSeconds(duration));
    }
}

    private void TryPlaceBridge()
    {
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 point = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        Collider2D hit = Physics2D.OverlapPoint(point, bridgeLayer);

        if (hit != null)
        {
            Vector3 spawnPos = new Vector3(
                hit.bounds.center.x,
                hit.bounds.center.y,
                0f
            );

            Instantiate(
                bridgePrefab,
                spawnPos,
                Quaternion.identity,
                bridgeContainer
            );

            currentWord = "";

            if (currentWordText != null)
                currentWordText.text = "";
        }
    }
    


    private IEnumerator HideHintAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (hintText != null)
            hintText.text = "";
    }
}