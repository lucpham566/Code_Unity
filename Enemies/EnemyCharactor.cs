public class EnemyCharactor : MonoBehaviour
{
    public int Health { get; protected set; }
    public int Armor { get; protected set; }
    public int Damage { get; protected set; }

    public float moveSpeed = 2.0f; // Tốc độ di chuyển của quái
    public float movementThreshold = 0.1f; // Ngưỡng vận tốc để xem quái vật có đang di chuyển

    // các trạng thái
    public int is_attack;
    public int is_move;
    private Rigidbody2D rb2d;
    private Animator animator;
    private Transform enemyTransform;

    public EnemyCharactor(int health, int armor, int damage)
    {
        Health = health;
        Armor = armor;
        Damage = damage;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyTransform = transform;
    }

    private void Update()
    {
        if (IsMoving())
        {
            animator.SetTrigger("Move");
            setFaceTarget();
        }
        if (IsGrounded())
        {
            animator.SetBool("Grounded", true);
        }
        else
        {
            animator.SetBool("Grounded", false);
        }



    }

    public void Attack(Player target)
    {
        int damageDealt = Mathf.Max(0, Damage - target.Armor);
        target.TakeDamage(damageDealt);
        Debug.Log($"{GetType().Name} attacks {target.GetType().Name} for {damageDealt} damage!");
        animator.SetTrigger("Attack");
    }

    public virtual void TakeDamage(int damage)
    {
        int damageTaken = Mathf.Max(0, damage - Armor);
        Health -= damageTaken;
        Debug.Log($"{GetType().Name} takes {damageTaken} damage!");
        animator.SetTrigger("Hurt");
        if (Health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{GetType().Name} has been defeated.");
        animator.SetTrigger("Die");
    }

    public void Move(Vector3 direction)
    {
        // Di chuyển quái vật theo hướng direction với tốc độ moveSpeed
        rb2d.velocity = direction.normalized * moveSpeed;
    }
    public bool IsMoving()
    {
        return rb2d.velocity.magnitude > movementThreshold;
    }

    bool IsGrounded()
    {
        float raycastDistance = 0.1f; // Độ dài của raycast từ quái vật xuống
        Vector3 raycastOrigin = transform.position + Vector3.up * 0.1f; // Vị trí bắt đầu raycast
        Vector3 raycastDirection = Vector3.down; // Hướng raycast (xuống)

        if (Physics.Raycast(raycastOrigin, raycastDirection, raycastDistance))
        {
            return true; // Quái vật đang ở mặt đất
        }

        return false; // Quái vật không ở mặt đất
    }

    public bool CheckJumpingAndFalling()
    {
        return rb2d.velocity.magnitude > movementThreshold;
        // Kiểm tra nếu quái vật đang nhảy (Jumping)
        if (rb.velocity.y > 0)
        {
            isJumping = true;
            isFalling = false;
            animator.SetBool("Jumping", true);
            animator.SetBool("Falling", false);
        }
        else if (rb.velocity.y < 0)
        {
            isJumping = false;
            isFalling = true;
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }
        else
        {
            isJumping = false;
            isFalling = false;
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
        }

    }

    private void setFaceTarget()
    {
        // Kiểm tra hướng vận tốc và lật lại hình ảnh
        if (rb.velocity.x > 0) // Nếu vận tốc dương, đang di chuyển sang phải
        {
            enemyTransform.localScale = new Vector3(Mathf.Abs(enemyTransform.localScale.x), enemyTransform.localScale.y, enemyTransform.localScale.z);
        }
        else if (rb.velocity.x < 0) // Nếu vận tốc âm, đang di chuyển sang trái
        {
            enemyTransform.localScale = new Vector3(-Mathf.Abs(enemyTransform.localScale.x), enemyTransform.localScale.y, enemyTransform.localScale.z);
        }
    }
}
