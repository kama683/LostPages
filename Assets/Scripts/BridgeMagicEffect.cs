using UnityEngine;

public class BridgeMagicEffect : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color baseColor;

    public float pulseSpeed = 2f;
    public float pulseAmount = 0.15f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
    }

    private void Update()
    {
        float pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;

        Color c = baseColor;
        c.r += pulse;
        c.g += pulse;
        c.b += pulse;

        sr.color = c;
    }
}