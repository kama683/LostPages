using UnityEngine;

// ============================================================
//  LOST PAGES — InkEnemy.cs
//  Прикрепи на чернильную кляксу (Level 3).
//  Требует: BoxCollider2D (Is Trigger = true)
// ============================================================
public class InkEnemy : MonoBehaviour
{
    public float patrolRange = 3f;
    public float speed       = 1.5f;

    Vector3 startPos;
    int     dir = 1;
    SpriteRenderer sr;

    void Start()
    {
        startPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * dir * speed * Time.deltaTime);

        float dist = transform.position.x - startPos.x;
        if (dist >  patrolRange) dir = -1;
        if (dist < -patrolRange) dir =  1;

        if (sr) sr.flipX = (dir < 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>()?.Die();
        }
    }
}