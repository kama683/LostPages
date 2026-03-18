using System.Collections;
using UnityEngine;

public class BridgeLifetime : MonoBehaviour
{
    public float lifetime = 8f;
    public float fadeDuration = 0.5f;

    private SpriteRenderer sr;
    private Color startColor;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (sr != null)
            startColor = sr.color;

        StartCoroutine(LifeRoutine());
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(lifetime);

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);

            if (sr != null)
            {
                Color c = startColor;
                c.a = alpha;
                sr.color = c;
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}