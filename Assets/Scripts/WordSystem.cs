using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ============================================================
//  LOST PAGES — WordSystem.cs
//  Прикрепи на пустой объект "WordSystem" в сцене.
//  Управляет: собранными словами, выбором (Q), применением (ЛКМ).
// ============================================================
public class WordSystem : MonoBehaviour
{
    [Header("Bridge Settings")]
    public GameObject bridgePrefab;       // префаб платформы-моста
    public float      bridgeDuration = 8f; // секунд до исчезновения

    [Header("UI")]
    public HUDController hud;

    // ---- приватное ----
    List<string> collectedWords = new List<string>();
    int          selectedIndex  = 0;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (collectedWords.Count == 0) return;

        // Q — сменить слово
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedIndex = (selectedIndex + 1) % collectedWords.Count;
            hud.UpdateWordDisplay(SelectedWord());
            AudioManager.Play("cycle");
        }

        // ЛКМ — применить слово в точку клика
        if (Input.GetMouseButtonDown(0))
        {
            ApplyWord(SelectedWord());
        }
    }

    // ---- подобрать слово ----
    public void PickupWord(string word)
    {
        if (!collectedWords.Contains(word))
        {
            collectedWords.Add(word);
            selectedIndex = collectedWords.Count - 1;
            AudioManager.Play("pickup");
            hud.UpdateWordDisplay(word);
            HUDController.ShowToast("Слово получено: " + word, new Color(0.23f,0.49f,0.26f));
        }
    }

    // ---- применить слово ----
    void ApplyWord(string word)
    {
        if (string.IsNullOrEmpty(word)) return;

        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;

        switch (word)
        {
            case "МОСТ":
            case "BRIDGE":
                SpawnBridge(worldPos);
                break;

            case "РОСТ":
            case "GROW":
                TryGrow(worldPos);
                break;
        }
    }

    // ---- МОСТ ----
    void SpawnBridge(Vector3 pos)
    {
        // Мост создаётся горизонтально в точке клика
        GameObject bridge = Instantiate(bridgePrefab,
            new Vector3(pos.x, pos.y, 0), Quaternion.identity);

        AudioManager.Play("bridge");
        HUDController.ShowToast("МОСТ создан!", new Color(0.55f, 0.37f, 0.24f));

        // Уничтожить через bridgeDuration секунд
        Destroy(bridge, bridgeDuration);
    }

    // ---- РОСТ (Уровень 2, но уже подготовлен) ----
    void TryGrow(Vector3 pos)
    {
        // ищем ближайшее растение
        GrowTarget[] targets = FindObjectsByType<GrowTarget>(FindObjectsSortMode.None);
        GrowTarget   nearest = null;
        float        minDist = 4f; // радиус действия в юнитах

        foreach (var t in targets)
        {
            float d = Vector2.Distance(t.transform.position, pos);
            if (d < minDist) { minDist = d; nearest = t; }
        }

        if (nearest != null)
        {
            nearest.Grow();
            AudioManager.Play("grow");
            HUDController.ShowToast("РОСТ!", new Color(0.23f,0.49f,0.26f));
        }
        else
        {
            AudioManager.Play("deny");
            HUDController.ShowToast("Здесь нет растений…", Color.gray);
        }
    }

    string SelectedWord() =>
        collectedWords.Count > 0 ? collectedWords[selectedIndex] : "";
}
