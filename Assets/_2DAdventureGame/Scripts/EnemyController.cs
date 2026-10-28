using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    [Min(0f)] public float speed = 1f;
    public bool vertical = true;
    [Min(0.02f)] public float changeTime = 3f;
    Rigidbody2D rigidbody2d;
    Animator animator;
    float timer;
    int direction = 1;
    bool broken = true;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = Mathf.Max(0.02f, changeTime);
    }

    void FixedUpdate()
    {
        if (!broken) return;
        Vector2 heading = vertical ? Vector2.up : Vector2.right;
        animator.SetFloat("Move X", heading.x * direction);
        animator.SetFloat("Move Y", heading.y * direction);
        rigidbody2d.MovePosition(rigidbody2d.position
            + heading * (speed * direction * Time.fixedDeltaTime));
        timer -= Time.fixedDeltaTime;
        if (timer <= 0f)
        {
            direction = -direction;
            timer += Mathf.Max(Time.fixedDeltaTime, changeTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!broken) return;
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null) player.ChangeHealth(-1);
    }

    public void Fix()
    {
        if (!broken) return;
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
    }
}
