using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScript : MonoBehaviour
{
    public GameObject JLine;
    public GameObject NILine;
    public GameObject[] Notes;
    public GameObject JudgeEft;

    bool bInit; // boolean Initialize
    int beat;
    bool UD; // Up down

    // Use this for initialization
    void Start()
    {
        bInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Touch
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            Touch();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Touch(true);
        }
        if (bInit)
        {
            if (JLine.GetComponent<JudgeLine>().TickFlag == 1 &&
                NILine == null)
            {
                NILine = GameObject.Find("NoteInitLine(Clone)");
                beat = NILine.GetComponent<NoteInitLine>().beat;
                // Note data's num = beat *2
                Notes = new GameObject[beat * 2];
                bInit = false;
            }
        }
        else
        {
            //--------------------------------------------------
            UD = JLine.GetComponent<JudgeLine>().UD;

            RsNoteArray();
        }
    }

    //Unimplemented
    void Touch()
    {
        Debug.Log("Touch");

        Vector2 touchPos = Input.GetTouch(0).deltaPosition;
    }

    void Touch(bool Click)
    {
        // Click position -> Ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();
        //hit ray
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            // TchLct : Touch Location
            string TchLct = hit.collider.gameObject.name;
            Debug.Log("Click, " + TchLct);

            // if, touch to TL...
            if (TchLct == "TL_L" || TchLct == "TL_R")
            {
                int max = beat * 2;
                for (int i = 0; i < max; i++)
                {
                    if (Notes[i] != null)
                    {
                        // ColEnterInfo = [ 0 : No Hit ] [ 1 : Enter ] [ 2 : Exit ]

                        if (Notes[i].GetComponent<NotesScript>().ColEnterInfo == 1)
                        {
                            if (JudgeFunc(Notes[i]))
                            {
                                Notes[i] = null;
                                break;
                            }
                        }
                        else if (Notes[i].GetComponent<NotesScript>().ColEnterInfo == 0)
                        {
                            if (JudgeFunc(Notes[i]))
                            {
                                Notes[i] = null;
                                break;
                            }
                        }
                        
                    }
                }
            }
        }
    }

    bool JudgeFunc(GameObject Note)
    {
        bool NoteIsNull = false;
        // ---------- Judge ----------
        // Bad (Fast)
        if (Note.GetComponent<NotesScript>().iJudgeCnt <= 30)
        {
            EftInit(Note, 0);
            Destroy(Note);
            NoteIsNull = true;
            GameObject.Find("GamePanel").GetComponent<GamePanelSet>().iCombo = 0;
            GameObject.Find("GamePanel").GetComponent<GamePanelSet>().iJudge[2]++;
        }
        // Great
        else if (Note.GetComponent<NotesScript>().iJudgeCnt <= 30 + 20)
        {
            EftInit(Note, 1);
            Destroy(Note);
            NoteIsNull = true;
            GameObject.Find("GamePanel").GetComponent<GamePanelSet>().iCombo++;
            GameObject.Find("GamePanel").GetComponent<GamePanelSet>().iJudge[1]++;
        }
        // Perfect
        else if (Note.GetComponent<NotesScript>().iJudgeCnt <= 30 + 20 + 10)
        {
            EftInit(Note, 2);
            Destroy(Note);
            NoteIsNull = true;
            GameObject.Find("GamePanel").GetComponent<GamePanelSet>().iCombo++;
            GameObject.Find("GamePanel").GetComponent<GamePanelSet>().iJudge[0]++;
        }
        // Great
        else if (Note.GetComponent<NotesScript>().iJudgeCnt <= 30 + 20 + 10 + 20)
        {
            // 3 = Great
            EftInit(Note, 3);
            Destroy(Note);
            NoteIsNull = true;
            GameObject.Find("GamePanel").GetComponent<GamePanelSet>().iCombo++;
            GameObject.Find("GamePanel").GetComponent<GamePanelSet>().iJudge[1]++;
        }
        // Bad (Late)
        else if (Note.GetComponent<NotesScript>().iJudgeCnt <= 30 + 50 + 10)
        {
            // 4 = Bad (Late)
            EftInit(Note, 4);
            Destroy(Note);
            NoteIsNull = true;
            GameObject.Find("GamePanel").GetComponent<GamePanelSet>().iCombo = 0;
            GameObject.Find("GamePanel").GetComponent<GamePanelSet>().iJudge[2]++;
        }
        return NoteIsNull;
    }


    // Effect Initialize
    void EftInit(GameObject Note, int iJudge)
    {
        GameObject EFT = Instantiate(JudgeEft, new Vector3(
            Note.transform.localPosition.x,
            Note.transform.localPosition.y,
            Note.transform.localPosition.z),
            Note.transform.localRotation,
            gameObject.transform.parent);
        EFT.name = "JudgeEffect[" + Note.GetComponent<NotesScript>().NoteNumber + "]";
        EFT.GetComponent<JudgeEffect>().bNoteColor = Note.GetComponent<NotesScript>().bNoteColor;
        EFT.GetComponent<JudgeEffect>().iJudge = iJudge;
        Debug.Log(Note.name + "'s Judge is " + iJudge);
    }

    // Resetting Notes Array
    void RsNoteArray()
    {
        for (int i = 0; i < beat * 2; i++)
        {
            for (int j = 0; j < beat * 2 - 1; j++)
            {
                if (Notes[j] == null)
                {
                    break;
                }
                if (Notes[j + 1] == null)
                {
                    break;
                }
                if (Notes[j].GetComponent<NotesScript>().NoteNumber >
                    Notes[j + 1].GetComponent<NotesScript>().NoteNumber)
                {
                    GameObject temp = Notes[j];
                    Notes[j] = Notes[j + 1];
                    Notes[j + 1] = temp;
                }
            }
        }
    }
}