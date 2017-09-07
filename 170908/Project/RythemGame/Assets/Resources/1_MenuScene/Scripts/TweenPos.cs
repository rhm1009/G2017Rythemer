using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPos : MonoBehaviour
{
    public bool bPlay;

    public float Delay;
    public float Duration;
    public int AnimValue;

    private void Start()
    {
        bPlay = false;
        AnimValue = 0;
    }

    private void Update()
    {
        if (!bPlay)
        {
            if (Delay <= 0f) { Delay = 0; }
            Invoke("PlayAnim", Delay);
            bPlay = true;
        }
    }

    public void PlayAnim()
    {
        if (gameObject.name == "Btn_Start")
        {
            // First Animation (appear)
            if (AnimValue == 0)
            {
                iTween.MoveBy(gameObject, iTween.Hash("x", 600, "easeType", "easeInOutExpo", "time", Duration, "loopType", "none", "delay", Delay));
                Delay = 0;
            }
            // Second Animation : right -> left (disappear)
            if (AnimValue == 1)
            {
                iTween.MoveBy(gameObject, iTween.Hash("x", -600, "easeType", "easeInOutExpo", "time", Duration, "loopType", "none", "delay", Delay));
                Delay = 0;
            }
            // Third Animation : left -> right (disappear)
            if (AnimValue == 2)
            {
                iTween.MoveBy(gameObject, iTween.Hash("x", 599.99f, "easeType", "easeInOutExpo", "time", Duration, "loopType", "none", "delay", Delay));
                Delay = 0;
            }
        }
        if (gameObject.name == "Btn_Option")
        {
            if (AnimValue == 0)
            {
                iTween.MoveBy(gameObject, iTween.Hash("x", 600, "easeType", "easeInOutExpo", "time", Duration, "loopType", "none", "delay", Delay));
                Delay = 0;
            }
            if (AnimValue == 1)
            {
                iTween.MoveBy(gameObject, iTween.Hash("x", -600, "easeType", "easeInOutExpo", "time", Duration, "loopType", "none", "delay", Delay));
                Delay = 0;
            }
        }
        if (gameObject.name == "Title")
        {
            if (AnimValue == 0)
            {
                iTween.MoveBy(gameObject, iTween.Hash("y", -200, "easeType", "easeInOutExpo", "time", Duration, "loopType", "none", "delay", Delay));
                Delay = 0;
            }
            if (AnimValue == 1)
            {
                iTween.MoveBy(gameObject, iTween.Hash("y", 200, "easeType", "easeInOutExpo", "time", Duration, "loopType", "none", "delay", Delay));
                Delay = 0;
            }
        }
        if (gameObject.name == "MusicSelect")
        {
            if (AnimValue == 0)
            {
                iTween.MoveBy(gameObject, iTween.Hash("y", 400, "easeType", "easeOutExpo", "time", Duration, "loopType", "none", "delay", Delay));
                Delay = 0;
            }
            if (AnimValue == 1)
            {
                iTween.MoveBy(gameObject, iTween.Hash("y", -400, "easeType", "easeInExpo", "time", Duration, "loopType", "none", "delay", Delay));
                Delay = 0;
            }
        }
    }
}
