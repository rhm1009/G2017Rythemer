using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour {

    public GameObject Title;
    public GameObject Btn_Start;
    public GameObject Btn_Option;
    public GameObject MP_Prefab; // MusicSelect Panel Prefab
    public GameObject MusicPanel; // MusicSelect Panel

    public GameObject MainCamera;

    float fTime;

    private void Start()
    {
        Btn_Start.GetComponent<Button>().interactable = false;
        Btn_Option.GetComponent<Button>().interactable = false;


        fTime = Btn_Option.GetComponent<TweenPos>().Duration 
                        + Btn_Option.GetComponent<TweenPos>().Delay;
        Invoke("ButtonSet", fTime);
    }

    private void Update()
    {
    }

    public void OnClickBtn_Start()
    {
        Title.GetComponent<TweenPos>().AnimValue = 1;
        Btn_Option.GetComponent<TweenPos>().AnimValue = 1;
        
        Btn_Start.GetComponent<TweenPos>().AnimValue = 2;

        if (GameObject.Find("MusicSelect") == null)
        {
            MusicPanel = Instantiate(MP_Prefab, Btn_Start.transform.parent);
            MusicPanel.transform.localPosition = new Vector3(0, -400);

            MusicPanel.name = "MusicSelect";
        }

        iTween.MoveBy(MainCamera, iTween.Hash("x", 32, "easeType", "easeInOutExpo", "time", 3, "loopType", "none", "delay", 2));

    }

    void ButtonSet()
    {
        Btn_Start.GetComponent<Button>().interactable = true;
        Btn_Option.GetComponent<Button>().interactable = true;
    }
}
