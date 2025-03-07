using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 5f;               // 發射物的移動速度
    public float lifetime = 5f;            // 發射物的存活時間

    private Vector3 targetPosition;        // 玩家初始位置
    private Vector3 direction;             // 發射物的移動方向

    AudioManager am;

    void Start()
    {
        am = GameObject.FindObjectOfType<AudioManager>();
        // 自動尋找玩家（假設玩家標籤為 "Player"）
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            targetPosition = player.transform.position;
            direction = (targetPosition - transform.position).normalized;
            transform.LookAt(targetPosition);  // 設置發射物面向目標
        }

        Destroy(gameObject, lifetime);      // 在指定時間後自動銷毀
    }

    void Update()
    {
        MoveProjectile();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 檢查是否碰到玩家或地面
        if (other.CompareTag("Body"))
        {
            Debug.Log("Player 被擊中");
            am.playSFX(am.gethit);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // 任意碰撞都銷毀發射物
    }

    private void MoveProjectile()
    {
        // 移動發射物並檢查是否接近目標位置
        transform.position += direction * speed * Time.deltaTime;
        
        // 如果發射物接近目標，則銷毀
        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            Destroy(gameObject);
        }
    }
}
