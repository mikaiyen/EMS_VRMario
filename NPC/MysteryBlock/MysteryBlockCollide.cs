using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBlockCollide : MonoBehaviour
{
    public GameObject mushroomPrefab;  // 蘑菇預製件
    public GameObject starPrefab;      // 星星預製件
    public Transform spawnPoint;       // 生成點（在 Mystery Block 上方的位置）
    private bool isUsed;              // 用來檢查是否已經觸發過

    AudioManager am;

    void Start()
    {
        isUsed = false;
        am = GameObject.FindObjectOfType<AudioManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if(isUsed)
            {
                return;
            }
            Debug.Log("mysterybox 啟動");

            am.playSFX(am.hitmysterybox);

            // 隨機選擇生成蘑菇或星星
            GameObject selectedPrefab = Random.value > 0.5f ? mushroomPrefab : starPrefab;

            // 在 Mystery Block 上方生成選定的物件
            Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);
            isUsed=true;
        }
    }
}