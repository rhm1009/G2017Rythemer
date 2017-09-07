using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelectScript : MonoBehaviour {

    public GameObject MainCamera;

    private void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
    }

    public void OnClickBtn_Music(string MusicNumber)
    {
        // Undo Button
        if (MusicNumber == "undo")
        {
            gameObject.GetComponent<TweenPos>().AnimValue = 1;
            gameObject.GetComponent<TweenPos>().bPlay = false;

            Invoke("RemovePanel", 2.5f);
        }
        // Scene Move
        else
        {
            GameObject.Find("SceneMng").GetComponent<SceneManager>().MusicNumber = MusicNumber;
            GameObject.Find("SceneMng").GetComponent<SceneManager>().FisPlay = true;
            GameObject.Find("SceneMng").GetComponent<SceneManager>().FadeIO = true;
            Invoke("SceneMove", 2f);
        }
    }

    void RemovePanel()
    {
        Destroy(gameObject);

        iTween.MoveBy(MainCamera, iTween.Hash("x", -32, "easeType", "easeInOutExpo", "time", 3, "loopType", "none", "delay", 0));

        GameObject.Find("Title").GetComponent<TweenPos>().AnimValue = 0;
        GameObject.Find("Title").GetComponent<TweenPos>().bPlay = false;

        GameObject.Find("Btn_Option").GetComponent<TweenPos>().AnimValue = 0;
        GameObject.Find("Btn_Option").GetComponent<TweenPos>().bPlay = false;

        GameObject.Find("Btn_Start").GetComponent<TweenPos>().AnimValue = 1;
        GameObject.Find("Btn_Start").GetComponent<TweenPos>().bPlay = false;

        GameObject.Find("Title").GetComponent<TweenPos>().Delay = GameObject.Find("Btn_Option").GetComponent<TweenPos>().Delay
            = GameObject.Find("Btn_Start").GetComponent<TweenPos>().Delay = 1;
    }

    void SceneMove()
    {
        Application.LoadLevel("2_GameScene");
    }
}
