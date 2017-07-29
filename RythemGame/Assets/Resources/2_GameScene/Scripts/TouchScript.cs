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
                for (int i = 0; i < beat * 2; i++)
                {
                    if (Notes[i] != null)
                    {
                        //if(Notes[i].collider2D.)
                        // ---------- Judge ----------
                        // Bad (Fast)
                        if (Notes[i].GetComponent<NotesScript>().iJudgeCnt <= 30)
                        {
                            EftInit(Notes[i], 0);
                            Destroy(Notes[i]);
                            Notes[i] = null;
                            break;
                        }
                        // Great
                        else if (Notes[i].GetComponent<NotesScript>().iJudgeCnt <= 30 + 20)
                        {
                            EftInit(Notes[i], 1);
                            Destroy(Notes[i]);
                            Notes[i] = null;
                            break;
                        }
                        // Perfect
                        else if (Notes[i].GetComponent<NotesScript>().iJudgeCnt <= 30 + 20 + 10)
                        {
                            EftInit(Notes[i], 2);
                            Destroy(Notes[i]);
                            Notes[i] = null;
                            break;
                        }
                        // Great
                        else if (Notes[i].GetComponent<NotesScript>().iJudgeCnt <= 30 + 20 + 10 + 20)
                        {
                            // 3 = Great
                            EftInit(Notes[i], 3);
                            Destroy(Notes[i]);
                            Notes[i] = null;
                            break;
                        }
                        // Bad (Late)
                        else if (Notes[i].GetComponent<NotesScript>().iJudgeCnt <= 30 + 50 + 30)
                        {
                            // 4 = Bad (Late)
                            EftInit(Notes[i], 4);
                            Destroy(Notes[i]);
                            Notes[i] = null;
                            break;
                        }
                    }
                }
            }
        }
    }

    // Effect Initialize
    void EftInit(GameObject Note, int iJudge)
    {
        GameObject EFT = Instantiate(JudgeEft, new Vector3(
            Note.transform.localPosition.x,
            Note.transform.localPosition.y, -1.49f),
            Quaternion.Euler(0, 0, 0),
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
                //*/
            }
        }
    }

}