using UnityEngine;

public class BridgeAppear : MonoBehaviour
{
    public float speed = 6f;

    private Vector3 targetScale;

    private void Start()
    {
        targetScale = transform.localScale;

        transform.localScale = new Vector3(0f, targetScale.y, targetScale.z);
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * speed
        );
    }
}