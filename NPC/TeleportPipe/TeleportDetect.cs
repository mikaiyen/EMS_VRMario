using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDetect : MonoBehaviour
{
    private bool playerInTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // 檢查是否是玩家進入
        if (other.CompareTag("Body"))
        {
            Debug.Log("玩家進入碰撞箱");
            playerInTrigger = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 檢查玩家是否離開碰撞箱
        if (other.CompareTag("Body"))
        {
            Debug.Log("玩家離開碰撞箱");
            playerInTrigger = false;
        }
    }

    public bool InThisTPpoint()
    {
        return playerInTrigger;
    }

}
