using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTeleport : MonoBehaviour
{
    public GameObject pipe1; // pipe1 的位置
    public GameObject pipe2; // pipe2 的位置
    private GameObject player;

    AudioManager am;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        am = GameObject.FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        bool tpPressed = Input.GetKeyDown("t");  //這行用來模擬傳送時的腿部動作
        if (tpPressed)  //測試傳送
        {
            ChechTP();
        }
    }
    

    private void ChechTP()
    {
        // 從 pipe1 和 pipe2 獲取 TeleportDetect 腳本
        TeleportDetect tp1 = pipe1.GetComponentInChildren<TeleportDetect>();
        TeleportDetect tp2 = pipe2.GetComponentInChildren<TeleportDetect>();

        // 檢查哪個 teleport point 有玩家在其中，並傳送到另一個 pipe 的位置
        if (tp1 != null && tp1.InThisTPpoint())
        {
            TeleportPlayer(player, pipe2.transform.Find("TP").transform.position);
        }
        else if (tp2 != null && tp2.InThisTPpoint())
        {
            TeleportPlayer(player, pipe1.transform.Find("TP").transform.position);
        }
    }

    private void TeleportPlayer(GameObject player, Vector3 targetPosition)
    {
        // 傳送玩家到指定位置
        player.transform.position = targetPosition;
        am.playSFX(am.teleportSound);
    }
}