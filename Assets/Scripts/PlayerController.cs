using UnityEngine;

// ============================================================
//  LOST PAGES — PlayerController.cs
//  Прикрепи на объект Player.
//  Требует: Rigidbody2D, BoxCollider2D, SpriteRenderer
// ============================================================
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed   = 6f;
    public float jumpForce   = 14f;

    [Header("Ground Check")]
    public Transform groundCheck;       // пустой дочерний объект у ног игрока
    public float     groundCheckRadius = 0.15f;
    public LayerMask groundLayer;       // слой "Ground"

    // ---- приватное ----
    Rigidbody2D  rb;
    SpriteRenderer sr;
    WordSystem   wordSystem;

    bool isGrounded;
    bool wasGrounded;
    bool isDead;
    float respawnX, respawnY;

    // Для анимации "чернильного" размытия при смерти
    float deathTimer = 0f;
    const float RESPAWN_DELAY = 1.8f;

    void Start()
    {
        rb         = GetComponent<Rigidbody2D>();
        sr         = GetComponent<SpriteRenderer>();
        wordSystem = FindObjectOfType<WordSystem>();

        // запомни стартовую позицию как respawn
        respawnX = transform.position.x;
        respawnY = transform.position.y;
    }

    void Update()
    {
        if (isDead)
        {
            deathTimer -= Time.deltaTime;
            // мигаем красным
            sr.color = Color.Lerp(new Color(0.8f,0,0,0.5f), Color.white,
                                  Mathf.PingPong(Time.time * 4f, 1f));
            if (deathTimer <= 0f) Respawn();
            return;
        }

        CheckGround();
        HandleMove();
        HandleJump();
        HandleInteract();
    }

    void CheckGround()
    {
        wasGrounded = isGrounded;
        isGrounded  = Physics2D.OverlapCircle(
            groundCheck.position, groundCheckRadius, groundLayer);

        // звук приземления
        if (!wasGrounded && isGrounded)
            AudioManager.Play("land");
    }

    void HandleMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(h * moveSpeed, rb.linearVelocity.y);

        if (h < 0) sr.flipX = true;
        else if (h > 0) sr.flipX = false;
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            AudioManager.Play("jump");
        }
    }

    void HandleInteract()
    {
        // E — подобрать слово (WordPickup обрабатывает OnTriggerEnter,
        // но оставим кнопку тоже)
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ничего дополнительного не нужно — триггер сам сработает
        }
    }

    // ---- вызывается врагом ----
    public void Die()
    {
        if (isDead) return;
        isDead     = true;
        deathTimer = RESPAWN_DELAY;
        rb.linearVelocity = Vector2.zero;
        AudioManager.Play("hit");
        HUDController.ShowToast("Чернила поглощают тебя…", new Color(0.8f,0,0));
    }

    void Respawn()
    {
        isDead = false;
        sr.color = Color.white;
        transform.position = new Vector2(respawnX, respawnY);
        rb.linearVelocity  = Vector2.zero;
    }

    // ---- публично для LostPage и других ----
    public void SetRespawn(float x, float y) { respawnX = x; respawnY = y; }
    public bool  IsDead => isDead;
}
