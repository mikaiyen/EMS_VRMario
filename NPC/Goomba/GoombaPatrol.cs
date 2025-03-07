using UnityEngine;

public class GoombaPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform pointA;                   // 起點位置
    public Transform pointB;                   // 終點位置
    public float speed = 3f;                   // 移動速度

    [Header("Sway Effect Settings")]
    public float swayAmplitude = 45f;          // 搖擺幅度（角度）
    public float swayFrequency = 5f;           // 搖擺頻率

    private Vector3 targetPosition;            // 當前目標位置
    private float swayTimer = 0f;              // 搖擺計時器

    void Start()
    {
        targetPosition = pointA.position;      // 初始目標設為點 A
    }

    void Update()
    {
        Patrol();
    }

    // 怪物在兩點間移動
    void Patrol()
    {
        MoveTowards(targetPosition);

        // 切換目標位置
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }
    }

    // 控制怪物朝向目標移動並進行搖擺
    void MoveTowards(Vector3 target)
    {
        // 計算方向並移動
        Vector3 direction = (target - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // 設置朝向目標的旋轉
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            
            // 計算搖擺效果
            swayTimer += Time.deltaTime * swayFrequency;
            float swayAngle = Mathf.Sin(swayTimer) * swayAmplitude;
            targetRotation *= Quaternion.Euler(0, 0, swayAngle);

            // 平滑地旋轉至新角度
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
