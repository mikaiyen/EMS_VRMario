using UnityEngine;

public class BowserController : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;    // 投擲的圓球預製件
    public Transform muzzle;               // 投擲起點
    public float throwInterval = 3f;       // 投擲間隔時間

    [Header("Health Settings")]
    public float health = 20;
    
    [Header("Animator")]
    public Animator animator;

    private float timer;                   // 計時器
    private bool hasBeenHit;               // 是否觸發過受擊
    private Transform player;              // 玩家位置
    private bool bossActivate=false;

    AudioManager am;

    void Start()
    {
        timer = throwInterval;             // 初始化計時器
        hasBeenHit = false;

        am = GameObject.FindObjectOfType<AudioManager>();

        // 找到場景中標記為 "Player" 的玩家物件
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if(!bossActivate)
        {
            checkStartBossFight();
            return;
        }
        

        // 確保 Bowser 始終面向玩家
        if (player != null)
        {
            Vector3 lookDirection = player.position - transform.position;
            lookDirection.y = 0; // 僅在水平面上旋轉
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        timer -= Time.deltaTime;

        // 每次計時器歸零時，進行投擲並重置計時器
        if (timer <= 0f)
        {
            SpawnProjectile();
            timer = throwInterval;
        }

        // 測試用：按下 "K" 鍵觸發受擊反應
        if (Input.GetKeyDown("k") && !hasBeenHit)
        {
            GetHit();
        }
    }

    //火球偵測
    private void OnTriggerEnter(Collider other)
    {        
        // 檢查是否與玩家的 Body 碰撞
        if (other.CompareTag("FireBall"))
        {
            GetHit();
        }

    }

    private void checkStartBossFight()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the distance to the player is greater than maxDistance, return early
        if (distanceToPlayer < 300f)
        {
            bossActivate=true;
            am.switchbgm(am.bossbgm);
        }
        
    }

    // 發射投擲物
    void SpawnProjectile()
    {
        am.playSFX(am.shootbullet);
        animator.SetBool("isThrow", true);
        Instantiate(projectilePrefab, muzzle.position, Quaternion.identity);
        Invoke(nameof(ResetAnimation), 1f); // 重置投擲動畫
    }

    // 處理受擊狀態
    void GetHit()
    {
        animator.SetBool("isHurt", true);
        health--;
        am.playSFX(am.enemydeath);
        Debug.Log("Bowser's health: " + health);
        HandleDeath();
        hasBeenHit = true;                 // 設定已受擊標記
        Invoke(nameof(ResetAnimation), 1f); // 重置受擊動畫
    }

    // 檢查生命值並處理死亡
    void HandleDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject, 1f);
            am.switchbgm(am.winbgm);
        }
    }

    // 重置動畫狀態及受擊標記
    void ResetAnimation()
    {
        animator.SetBool("isThrow", false);
        animator.SetBool("isHurt", false);
        hasBeenHit = false;
    }
}
