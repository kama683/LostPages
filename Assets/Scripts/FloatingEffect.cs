using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float floatSpeed = 2f;     // скорость движения
    public float floatHeight = 0.15f; // насколько высоко поднимается
    

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );
    }
}