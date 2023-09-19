public class EnemyController : MonoBehaviour
{
    private Vector3 targetPosition; // Vị trí mục tiêu để quái vật di chuyển đến
    private EnemyCharactor enemyCharactor;

    private void Start()
    {
        enemyCharactor = GetComponent<EnemyCharactor>();
        // Khởi tạo vị trí mục tiêu ban đầu
        UpdateTargetPosition();

    }

    private void Update()
    {
        if (!enemyCharactor.is_move && !enemyCharactor.is_attack)
        {
            MoveRandom();
        }

    }

    private void MoveRandom()
    {
        // Di chuyển quái vật đến vị trí mục tiêu
        enemyCharactor.Move(targetPosition);
        // Kiểm tra nếu quái vật đã đến gần đủ với vị trí mục tiêu
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Cập nhật lại vị trí mục tiêu mới
            UpdateTargetPosition();
        }
    }

    private void UpdateTargetPosition()
    {
        // Tạo một vị trí ngẫu nhiên xung quanh vị trí hiện tại
        float randomX = Random.Range(transform.position.x - 5f, transform.position.x + 5f);
        float randomY = Random.Range(transform.position.y - 5f, transform.position.y + 5f);

        // Đặt vị trí mục tiêu mới
        targetPosition = new Vector3(randomX, randomY, 0f);
    }
}
