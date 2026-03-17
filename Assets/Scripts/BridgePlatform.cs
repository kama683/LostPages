using UnityEngine;

// ============================================================
//  LOST PAGES — BridgePlatform.cs
//  Прикрепи на префаб Bridge.
//  Визуальный эффект появления + исчезновения.
// ============================================================
public class BridgePlatform : MonoBehaviour
{
    public float lifetime    = 8f;   // совпади с WordSystem.bridgeDuration
    public float fadeInTime  = 0.3f;
    public float fadeOutTime = 1.0f;

    SpriteRenderer sr;
    float elapsed = 0f;
    bool  fadingOut = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        if (!fadingOut && elapsed < fadeInTime)
        {
            // Fade in
            float a = elapsed / fadeInTime;
            SetAlpha(a);
        }
        else if (!fadingOut && elapsed >= lifetime - fadeOutTime)
        {
            fadingOut = true;
        }
        else if (fadingOut)
        {
            float timeLeft = (lifetime - elapsed);
            float a = Mathf.Clamp01(timeLeft / fadeOutTime);
            SetAlpha(a);
        }
    }

    void SetAlpha(float a)
    {
        Color c = sr.color;
        c.a = a;
        sr.color = c;
    }
}
