using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeLine : MonoBehaviour
{
    public float bpm;
    public bool UD; //Up(true) Down(false)
    bool bInit;     // boolean Initalize
    public short TickFlag; // 0 = No, 1 = Time Ready, 2 = Time & Line Position Ready, 3 = end
    public SpriteRenderer LineSprite;
    public TextMesh MusicInfo;
    public AudioSource AdSrc_tick; // Audio Source - tick

    GameObject NILine; // Note Init Line

    short timeCnt; // Init time Count -> Game delta time Cnt
    short tickCnt; // tick Count (4 count)
    int reverse;   // reverse (from UD)
    // Use this for initialization
    void Start()
    {
        bInit = true; TickFlag = 0;
        UD = false;
        LineSprite.color = new Color(1, 1, 1, 0);
        MusicInfo.color = new Color(1, 1, 1, 0);
        timeCnt = 0; tickCnt = 4;
        reverse = -1;
    }

    // Update is called once per frame
    void Update()
    {
        // Initalize Animation
        if (bInit)
        {
            if (LineSprite.color.a >= 1) {
                bInit = false; timeCnt = 0;
                Invoke("TickFunc", 2f);
            }
            if (MusicInfo.color.a >= 1) { timeCnt++; }

            //Text appear
            if (timeCnt == 0)
            {
                MusicInfo.color = new Color(1, 1, 1,
                    MusicInfo.color.a + 0.01f);
            }
            //Text disappear
            if (timeCnt >= 100)
            {
                MusicInfo.color = new Color(1, 1, 1,
                   MusicInfo.color.a - 0.01f);
                MusicInfo.transform.localPosition = Vector3.Lerp(MusicInfo.transform.localPosition,
                    new Vector3(MusicInfo.transform.localPosition.x, 5f, MusicInfo.transform.localPosition.z), Time.deltaTime);
            }
            //Line appear
            if (timeCnt >= 100 && MusicInfo.color.a <= 0)
            {
                

                LineSprite.color = new Color(1, 1, 1,
                    LineSprite.color.a + 0.01f);

                
                if (gameObject.transform.localPosition.y <= -5.2f
                || gameObject.transform.localPosition.y >= 5.2f)
                {
                    UD = !UD;
                    if (UD) reverse = 1; else reverse = -1;
                    // Make Note Initalize Line
                    if (NILine == null)
                    {
                        // bpm 200 -> basic setting judge
                        if (bpm == 200)
                        {
                            NILine = Instantiate(Resources.Load<GameObject>("2_GameScene/Prefab/NoteInitLine"),
                                new Vector3(0, gameObject.transform.localPosition.y * -reverse, -1.5f), Quaternion.Euler(0, 0, 0),
                                gameObject.transform.parent);
                        }
                        else if (bpm == 100)
                        {
                            NILine = Instantiate(Resources.Load<GameObject>("2_GameScene/Prefab/NoteInitLine"),
                                new Vector3(0, 0, -1.5f), Quaternion.Euler(0, 0, 0),
                                gameObject.transform.parent);
                            //NILine.GetComponent<NoteInitLine>().UD = !NILine.GetComponent<NoteInitLine>().UD;
                        }
                    }
                }
                gameObject.transform.localPosition = new Vector3(
                 gameObject.transform.localPosition.x,
                 gameObject.transform.localPosition.y + (bpm * reverse / 1000f) ,
                 gameObject.transform.localPosition.z);
            }
        }
        // End of Init Anime
        else
        {
            //  Judge Bar Moving
            gameObject.transform.localPosition = new Vector3(
                gameObject.transform.localPosition.x,
                gameObject.transform.localPosition.y + (bpm * reverse / 1000f) ,
                gameObject.transform.localPosition.z);
            
            // If, Line arrival top or bottom 
            if (gameObject.transform.localPosition.y <= -5.2f
                || gameObject.transform.localPosition.y >= 5.2f)
            {
                Debug.Log("Tick! " + Time.time);
                UD = !UD;
                reverse *= -1;
                
                if (tickCnt > 0)
                {
                    AdSrc_tick.Play();
                    if(tickCnt==1)
                        NILine.GetComponent<NoteInitLine>().bInit = false;
                    tickCnt--;
                }
                if (tickCnt <= 0)
                {
                    TickFunc();
                }
            }

        }
    }

    void TickFunc()
    {
        // Tick Time Ready
        if (TickFlag == 0)
        {
            TickFlag = 1;
            return;
        }
        // Tick get Ready ( 4 count )
        else if (TickFlag == 1)
        {
            TickFlag = 2;
            return;
        }
        // ---------------------
        else if (TickFlag == 2 && tickCnt == 0)
        {
            TickFlag = 3;
            NILine.GetComponent<NoteInitLine>().fStartTime = Time.time;
            //NILine.GetComponent<NoteInitLine>().NITiming = 10;
        }
    }

    // Enter
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Notes")
        {
            // Prevention reversue judge
            if (col.gameObject.GetComponent<NotesScript>().bNoteColor == UD)
                col.gameObject.GetComponent<NotesScript>().ColEnterInfo = 1;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Notes")
        {
            // Prevention reversue judge
            if (col.gameObject.GetComponent<NotesScript>().bNoteColor == UD)
                col.gameObject.GetComponent<NotesScript>().ColEnterInfo = 1;
        }
    }

    // Exit
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Notes")
        {
            // Prevention reversue judge
            if (col.gameObject.GetComponent<NotesScript>().ColEnterInfo == 1)
                col.gameObject.GetComponent<NotesScript>().ColEnterInfo = 2;
        }
    }

    
}