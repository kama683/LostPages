using UnityEngine;

public class MenuFloat : MonoBehaviour
{
    public float speed = 1.2f;
    public float height = 8f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = startPos + new Vector3(
            0f,
            Mathf.Sin(Time.time * speed) * height,
            0f
        );
    }
}