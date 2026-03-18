using UnityEngine;

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