using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

public class GamePanelSet : MonoBehaviour {
    public GameObject SceneMng;

    public string MusicNumber;
    public TextMesh MusicInfoText;
    public AudioSource Music;
    public AudioClip AC;

    public GameObject Combo;
    private GameObject JLine;

    //input txt file
    protected string InfoTextFile = " "; // info text file IMPORT (name, writter, bpm, beat)
    protected string NoteTextFile = " "; // notes text file IMPORT

    public string[] MusicInfoStr;
    public char[] cNote;
    // 0 : Music Name
    // 1 : Writter Name
    // 2 : BPM
    // 3 : Beat Value
    public short bpm;
    public short beat;

    public int iCombo;
    public int iScore;
    public int[] iJudge = new int[4]; // 0 : perfect, 1 : great, 2 : bad, 3 : miss

    public int MusicEnd;

    // Use this for initialization
    void Start()
    {
        SceneMng = GameObject.Find("SceneMng");
        MusicNumber = SceneMng.GetComponent<SceneManager>().MusicNumber;
        SceneMng.GetComponent<SceneManager>().FisPlay = true;
        SceneMng.GetComponent<SceneManager>().AnimTime = 1.0f;

        if (FileLoad())
        {
            MusicInfoText.text = InfoTextFile;
            MusicInfoStr = InfoTextFile.Split('\n');
            MusicInfoText.text = MusicInfoStr[0] + '\n' + MusicInfoStr[1];
            bpm = short.Parse(MusicInfoStr[2]);
            beat = short.Parse(MusicInfoStr[3]);

            string[] NoteStr1;
            string NoteStr2;
            NoteStr1 = NoteTextFile.Split('\n', '\r');
            NoteStr2 = "";
            foreach (string c in NoteStr1)
            {
                NoteStr2 += c;
            }
            cNote = NoteStr2.ToCharArray();
            //byte[] arr = Encoding.ASCII.GetBytes(NoteStr2);
        }

        // if, Tutorial
        if(MusicNumber == "000")
        {
            Music.loop = true;
        }

        JLine = GameObject.Find("JudgeLine");
        JLine.GetComponent<JudgeLine>().bpm = bpm; // first : down ( use - )

        iCombo = iScore = 0;
        for(int i = 0; i < iJudge.Length; i++)
            iJudge[i] = 0;

        Combo.SetActive(false);

        // if, Tutorial
        if (MusicNumber == "000")
        {
            Instantiate(Resources.Load("2_GameScene/Prefab/TutorialPanel"), transform).name = "TutorialPanel";
            // Tutorial JudgeLineText Find  
            GameObject Tuto_JLText = GameObject.Find("TutorialPanel/Text/JudgeLineTXT");
            if (Tuto_JLText != null)
            {
                Tuto_JLText.GetComponent<FollowScript>().Obj_To = JLine;
            }
        }

        MusicEnd = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // tick end -> music start
        if (!Music.isPlaying && MusicEnd == 0 &&
            JLine.GetComponent<JudgeLine>().TickFlag == 3)
        {
            Debug.Log("Music Start");
            Music.Play();
            JLine.GetComponent<JudgeLine>().TickFlag = 4;
        }

        else if (MusicEnd == 1)
        {
            InvokeRepeating("MusicisEnd", 3.0f, 1.5f);
            MusicEnd = 2;
        }

        // Combo Text Appear & Disappear
        if(iCombo > 3)
        {
            Combo.SetActive(true);
        }
        else
        {
            Combo.SetActive(false);
        }
    }

    bool FileLoad()
    {
        // LOAD TXT FILE
        string TextFilePath = Application.dataPath + // Asset
            "/Resources/2_GameScene/Sound/Music/" + MusicNumber + "/";
        Debug.Log("Text Files Path : " + TextFilePath);
        FileInfo MusicFile;
        // Load Info txt file ----------------------------------
        MusicFile = new FileInfo(TextFilePath + "info.txt");
        try
        {
            using (StreamReader sr = new StreamReader(MusicFile.OpenRead(), Encoding.UTF8))
            {
                InfoTextFile = sr.ReadToEnd();
                sr.Close();
            }
        }
        catch (Exception e)
        {
            Debug.Log("Load Fail");
            return false;
        }
        // Load notes txt file ---------------------------------
        MusicFile = new FileInfo(TextFilePath + MusicNumber + ".txt");
        try
        {
            using (StreamReader sr = new StreamReader(MusicFile.OpenRead(), Encoding.UTF8))
            {
                NoteTextFile = sr.ReadToEnd();
                sr.Close();
            }
        }
        catch (Exception e)
        {
            Debug.Log("Load Fail");
            return false;
        }
        // -----------------------------------------------------
        // Load Music File (mp3)
        string MusicFilePath = "2_GameScene/Sound/Music/" + MusicNumber + "/";
        AC = Resources.Load<AudioClip>(MusicFilePath + MusicNumber);
        Music.clip = AC;

        return true;
    }

    void MusicisEnd()
    {
        if(MusicEnd == 2)
        {
            Debug.Log("Music End is " + MusicEnd + " " + Time.time);
            // Fade Animation
            SceneMng.GetComponent<SceneManager>().FisPlay = true;
            MusicEnd = 3;
        }
        else if(MusicEnd == 3)
        {
            Debug.Log("Music End is " + MusicEnd + " " + Time.time);
            Application.LoadLevel("1_MenuScene");
            MusicEnd = 4;
        }
    }
}