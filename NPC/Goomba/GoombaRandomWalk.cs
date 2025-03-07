using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;


public class Goomba : MonoBehaviour
{
    public float wanderRadius = 10f;          // 怪物的遊蕩範圍
    public float wanderSpeed = 2f;            // 遊蕩時的速度
    public float chaseSpeed = 5f;             // 追逐玩家的速度
    public float detectionRange = 10f;        // 偵測玩家的範圍
    public Transform player;                  // 玩家目標
    public float swayAmplitude = 45f;         // 左右搖擺的幅度（角度）
    public float swayFrequency = 5f;          // 左右搖擺的頻率

    private Vector3 wanderTarget;             // 遊蕩目標位置
    private bool isChasing = false;
    private float swayTimer = 0f;             // 搖擺計時器

    void Start()
    {
        SetRandomWanderTarget();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // 進入追逐模式
            isChasing = true;
        }
        else if (distanceToPlayer > detectionRange * 1.5f)  // 偵測範圍的1.5倍範圍外停止追逐
        {
            // 退出追逐模式，返回遊蕩
            if(isChasing==true){
                isChasing = false;
                SetRandomWanderTarget();
            }
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Wander();
        }
    }

    void SetRandomWanderTarget()
    {
        // 設置一個在範圍內的隨機位置作為遊蕩目標
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y;  // 保持高度不變
        wanderTarget = randomDirection;
    }

    void Wander()
    {
        // 向遊蕩目標移動
        MoveTowards(wanderTarget, wanderSpeed);

        if (Vector3.Distance(transform.position, wanderTarget) < 0.5f)
        {
            SetRandomWanderTarget();  // 到達目標後設置新的隨機遊蕩目標
        }
    }

    void ChasePlayer()
    {
        // 追逐玩家
        MoveTowards(player.position, chaseSpeed);
    }

    void MoveTowards(Vector3 target, float speed)
    {
        // 讓怪物移動到目標並朝向移動的方向
        Vector3 direction = (target - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        // 設置旋轉，讓怪物的 Z 軸朝向移動的方向
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

            // 左右搖晃效果
            swayTimer += Time.deltaTime * swayFrequency;
            float swayAngle = Mathf.Sin(swayTimer) * swayAmplitude;
            targetRotation *= Quaternion.Euler(0, 0, swayAngle);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            Debug.Log("怪物觸碰到了玩家！");
        }
    }
}
