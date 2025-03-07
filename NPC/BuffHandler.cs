using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    public float scaleMultiplier = 1.5f; // Scale multiplier for mushroom effect
    public float buffDuration = 15f;


    private Vector3 originalScale; // Store the original scale of the player
    public bool isStar;
    public bool isMushroom;

    AudioManager am;

    void Start()
    {
        originalScale = transform.localScale; // Save the original scale
        isStar=false;
        isMushroom=false;
        am = GameObject.FindObjectOfType<AudioManager>();
    }

    // This method will be called when the mushroom collision is detected
    public void ApplyMushroomBuff()
    {
        if(isMushroom)
        {
            return;
        }

        am.playSFX(am.collectitem);

        // Scale up the player
        transform.localScale = originalScale * scaleMultiplier;
        isMushroom=true;
        // Reset the scale after 
        Invoke(nameof(ResetBuff), buffDuration);
    }

    public void ApplyStarBuff()
    {
        am.playSFX(am.collectitem);
        am.switchbgm(am.starbgm);
        isStar=true;
        Invoke(nameof(ResetBuff), buffDuration);
    }



    public void ResetBuff()
    {
        if(isStar)
        {
            am.switchbgm(am.mainbgm);
        }
        isMushroom=false;
        isStar=false;
        transform.localScale = originalScale; // Reset to the original scale
    }

    
}
