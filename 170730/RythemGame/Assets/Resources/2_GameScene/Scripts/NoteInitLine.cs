using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInitLine : MonoBehaviour {
    public bool bInit;
    public bool UD; // Up(true) Down(false)
    public char[] cNote;
    public int iNowNote;    // int Now Note (cNote[iNowNote])
    public float fStartTime;    // float Start Time... (Time.time)

    public float fps;
    float NITiming;     // Note Init Timing

    public GameObject Note;
    public GameObject TouchLoc; // Touch Location

    GameObject JLine;

    float bpm;
    public int beat;

    // Use this for initialization
    void Start ()
    {
        JLine = GameObject.Find("GamePanel/JudgeLine");
        TouchLoc = GameObject.Find("GamePanel/TouchLocation");
        bInit = true;

        // Load cNote data of GamePanel(Parent)
        cNote = gameObject.transform.parent.GetComponent<GamePanelSet>().cNote;
        beat = gameObject.transform.parent.GetComponent<GamePanelSet>().beat;

        // if(beat==1) beat=64; if(beat==4) beat=16; if(beat==32) beat=4; ...
        beat = 64 / beat;

        bpm = -JLine.GetComponent<JudgeLine>().bpm;
        UD = !JLine.GetComponent<JudgeLine>().UD;
        iNowNote = 0;
        NITiming = 0;
    }

    // Update is called once per frame
    void Update()
    {
        fps = 1f/Time.deltaTime;
        //  Judge Bar Moving
        gameObject.transform.localPosition = new Vector3(
            gameObject.transform.localPosition.x,
            gameObject.transform.localPosition.y + (bpm / 1000f),
            gameObject.transform.localPosition.z);

        // If, Line arrival top or bottom 
        if (gameObject.transform.localPosition.y <= -5.3f
            || gameObject.transform.localPosition.y >= 5.3f)
        {
            bpm *= -1;
            UD = !UD;
        }

        //------------ Note ------------//
        // bInit(false) = Before Music Start 1Tick
        if (!bInit)
        {
            float PlayTime = Time.time - fStartTime;
            float realBeat = fps / beat;
            //Debug.Log(realBeat);
            //Make a NOTE!!
            if(NITiming >=  (Time.deltaTime * 60f) / realBeat)
            {
                if (iNowNote < cNote.Length && cNote[iNowNote] != '0')
                {
                    GameObject BeforeNote = null;
                    // 1 = Normal Note
                    if (cNote[iNowNote] == '1')
                    {
                        float x;
                        bool b;
                        Debug.Log("Create " + iNowNote + " Note");
                        do
                        {
                            x = Random.Range(-4, 4);

                            b = false;

                            // prevent fall on
                            if (iNowNote != 0 && BeforeNote != null)
                            {
                                BeforeNote = GameObject.Find("Note[" + (iNowNote-1) + "]");
                                b = (x < BeforeNote.transform.localPosition.x - 1.5f &&
                                  x > BeforeNote.transform.localPosition.x + 1.5f);
                            }
                        } while (b);

                        Instantiate(Note,
                            new Vector3(x, gameObject.transform.localPosition.y, -1.49f),
                            Quaternion.Euler(0, 0, 0), gameObject.transform.parent);
                    }

                    // Setting Name of Note Prefab
                    GameObject nt = GameObject.Find("Note(Clone)");
                    nt.name = "Note[" + iNowNote + "]";
                    nt.GetComponent<NotesScript>().NoteNumber = iNowNote;

                    // put note object in null
                    for (int i = 0; i < beat * 2; i++)
                    {
                        if (TouchLoc.GetComponent<TouchScript>().Notes[i] == null)
                        {
                            TouchLoc.GetComponent<TouchScript>().Notes[i] = nt;
                            break;
                        }
                    }
                    // pull

                    for (int i = 0; i < beat * 2 - 1; i++)
                    {
                        // if, Note[i] -> null (this work index 0 -> null)
                        if (TouchLoc.GetComponent<TouchScript>().Notes[i] == null)
                        {
                            for (int j = i + 1; j < beat * 2; j++)
                            {
                                if (TouchLoc.GetComponent<TouchScript>().Notes[j] != null)
                                {

                                    TouchLoc.GetComponent<TouchScript>().Notes[i]
                                        = TouchLoc.GetComponent<TouchScript>().Notes[j];
                                    TouchLoc.GetComponent<TouchScript>().Notes[j] = null;
                                    break;
                                }
                            }
                        }

                        if (TouchLoc.GetComponent<TouchScript>().Notes[i + 1] == null)
                        {
                            for (int j = i + 1; j < beat * 2; j++)
                            {
                                if (TouchLoc.GetComponent<TouchScript>().Notes[j] != null)
                                {

                                    TouchLoc.GetComponent<TouchScript>().Notes[i+1]
                                        = TouchLoc.GetComponent<TouchScript>().Notes[j];
                                    TouchLoc.GetComponent<TouchScript>().Notes[j] = null;
                                    break;
                                }
                            }
                        }
                    }
                }
                iNowNote++;
                NITiming = 0;
            }
            NITiming += Time.deltaTime;
            
        }
    }
}
