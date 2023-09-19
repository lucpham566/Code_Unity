public class BaseEnemy: EnemyCharactor
{
    public int Health { get; protected set; }
    public int Armor { get; protected set; }
    public int Damage { get; protected set; }
    public float moveSpeed = 3.0f;

    private Rigidbody2D rb;
    private Animator animator;

    public EnemyCharactor(int health, int armor, int damage)
    {
        Health = health;
        Armor = armor;
        Damage = damage;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Attack(Player target)
    {
        int damageDealt = Mathf.Max(0, Damage - target.Armor);
        target.TakeDamage(damageDealt);
        Debug.Log($"{GetType().Name} attacks {target.GetType().Name} for {damageDealt} damage!");
    }

    public virtual void TakeDamage(int damage)
    {
        int damageTaken = Mathf.Max(0, damage - Armor);
        Health -= damageTaken;
        Debug.Log($"{GetType().Name} takes {damageTaken} damage!");
        animator.SetTrigger("Attack");
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
}
