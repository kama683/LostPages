using UnityEngine;

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