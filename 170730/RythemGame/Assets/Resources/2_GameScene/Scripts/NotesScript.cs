using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesScript : MonoBehaviour {

    // 0 : main ring , 1 : bubble
    public SpriteRenderer[] NoteSprite = new SpriteRenderer[2];

    GameObject JLine; // Judge Line
    GameObject NILine; // Note Init Line
    GameObject GPanel; // Game Panel
    GameObject TouchLoc; // Touch Location
    public bool bNoteColor; // true : red(Up)   false : blue(Down)
    public int iJudgeCnt; // int Judge Count... 
    float frame; // Animation Frame
    public int NoteNumber;

    public bool END;

    // Use this for initialization
    void Start ()
    {
        JLine = GameObject.Find("JudgeLine");
        NILine = GameObject.Find("NoteInitLine(Clone)");
        GPanel = GameObject.Find("GamePanel");
        TouchLoc = GameObject.Find("TouchLocation");
        gameObject.transform.localScale = new Vector3(0,0,1);
        iJudgeCnt = 0;
        frame = 0;

        bNoteColor = NILine.GetComponent<NoteInitLine>().UD;
        if (bNoteColor) {
            NoteSprite[0].color = new Color(1, 0.2f, 0.2f, 0);
            NoteSprite[1].color = new Color(1, 1, 1, 0);
        }else
        {
            NoteSprite[0].color = new Color(0.2f, 0.2f, 1, 0);
            NoteSprite[1].color = new Color(1, 1, 1, 0);
        }

        END = false;
    }
	
	// Update is called once per frame
	void Update () {
        // Animation Code... iJudge Count ++...
        // 30(Fast, Bad) + 20(Great) + 10(Perfect) + 20(Great) + 30(Late, Bad) (110)
        // Appear Anim, [30]frame
        if (iJudgeCnt <= 30)
        {
            iJudgeCnt++;
            frame = 30f;
            // scale
            gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x + 1f / frame,
                gameObject.transform.localScale.y + 1f / frame, 1);
            // color
            NoteSprite[0].color = new Color(
                NoteSprite[0].color.r,
                NoteSprite[0].color.g,
                NoteSprite[0].color.b,
                NoteSprite[0].color.a + 1f / frame);
            NoteSprite[1].color = new Color(
                NoteSprite[1].color.r,
                NoteSprite[1].color.g,
                NoteSprite[1].color.b,
                NoteSprite[1].color.a + 1f / frame);

            if (gameObject.transform.localScale.x > 1)
            {
                gameObject.transform.localScale = Vector3.one;
            }
        }
        // great(fast) timing [20]frame
        else if (iJudgeCnt <= 30 + 20)
        {
            iJudgeCnt++;
            frame = 50f;
            gameObject.transform.rotation = Quaternion.Lerp(
                gameObject.transform.localRotation, Quaternion.Euler(0, 0, 90), frame / (frame * 10f));
        }
        // perfect timing [10]frame
        else if (iJudgeCnt <= 30 + 20 + 10)
        {
            iJudgeCnt++;
            frame = 50f;
            gameObject.transform.rotation = Quaternion.Lerp(
                gameObject.transform.localRotation, Quaternion.Euler(0, 0, 90), frame / (frame * 10f));
        }
        // great(late) timing [20]frame
        else if (iJudgeCnt <= 30 + 20 + 10 + 20)
        {
            iJudgeCnt++;
            frame = 50f;
            gameObject.transform.rotation = Quaternion.Lerp(
                gameObject.transform.localRotation, Quaternion.Euler(0, 0, 90), frame / (frame * 10f));
        }
        // disappear Anim, [30]frame
        else if (iJudgeCnt <= 30 + 50 + 30)
        {
            iJudgeCnt++;
            frame = 30f;
            // scale
            gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x + 0.5f / frame,
                gameObject.transform.localScale.y + 0.5f / frame, 1);
            // color
            NoteSprite[0].color = new Color(
                NoteSprite[0].color.r,
                NoteSprite[0].color.g,
                NoteSprite[0].color.b,
                NoteSprite[0].color.a - 1f / frame);
            NoteSprite[1].color = new Color(
                NoteSprite[1].color.r,
                NoteSprite[1].color.g,
                NoteSprite[1].color.b,
                NoteSprite[1].color.a - 1f / frame);
            // rotate
            gameObject.transform.rotation = Quaternion.Lerp(
                gameObject.transform.localRotation, Quaternion.Euler(0, 0, 180), frame / (frame * 10f));
        }
        // End of Animation
        else
        {
            for (int i = 0; i < GPanel.GetComponent<GamePanelSet>().beat * 2; i++) {
                if(TouchLoc.GetComponent<TouchScript>().Notes[i] != null &&
                    TouchLoc.GetComponent<TouchScript>().Notes[i].name
                    == "Note[" + NoteNumber + "]")
                {
                    TouchLoc.GetComponent<TouchScript>().Notes[i] = null;
                }
            }
            Destroy(gameObject);
        }
	}
}
