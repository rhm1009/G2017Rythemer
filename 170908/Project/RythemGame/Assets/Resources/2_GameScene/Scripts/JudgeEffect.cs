using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeEffect : MonoBehaviour {

    public bool bNoteColor;  // true : red(Up)   false : blue(Down)
    public Animator EftAnim; // Effect Animation
    public SpriteRenderer NoteSprite; // Ring Sprite
    public int iJudge;

    public int frame;

    public short ColorLevel;

	// Use this for initialization
	void Start () {
        frame = 0;
        ColorLevel = 0;
        if (bNoteColor)
        {
            NoteSprite.color = new Color(1, 0.2f, 0.2f, 1);
        }
        else
        {
            NoteSprite.color = new Color(0.2f, 0.2f, 1, 1);
        }
    }

    // Update is called once per frame
    void Update() {
        if (EftAnim.enabled)
        {
            frame++;
            if (frame >= 40)
            {
                EftAnim.enabled = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }
        // ------------- Judge Effect -----------
        // Judge : Bad (Fast)
        if (iJudge == 0)
        {
            //red
            if (bNoteColor)
            {
                NoteSprite.color -= new Color(0.02f, 0, 0, 0);
            }
            //blue
            else
            {
                NoteSprite.color -= new Color(0, 0, 0.02f, 0);
            }
        }
        // Judge : Great
        else if (iJudge == 1 || iJudge == 3)
        {
            //red
            if (bNoteColor)
            {
                NoteSprite.color += new Color(0, 0.02f, 0.02f, 0);
            }
            //blue
            else
            {
                NoteSprite.color += new Color(0.02f, 0.02f, 0, 0);
            }
        }
        // Judge : Perfect!!
        else if (iJudge == 2)
        {
            // red
            if (bNoteColor)
            {
                //rainbow color animation
                // [ =r= +g+ =b= ]
                if (ColorLevel == 0)
                {
                    NoteSprite.color += new Color(0, 0.08f, 0, 0);
                    if (NoteSprite.color.g > 1.0f) ColorLevel = 1;
                }
                // [ -r- =g= =b= ]
                else if (ColorLevel == 1)
                {
                    NoteSprite.color -= new Color(0.08f, 0, 0, 0);
                    if (NoteSprite.color.r <= 0.4f) ColorLevel = 2;
                }
                // [ =r= =g= +b+ ]
                else if (ColorLevel == 2)
                {
                    NoteSprite.color += new Color(0, 0, 0.08f, 0);
                    if (NoteSprite.color.b > 1.0f) ColorLevel = 3;
                }
                // [ =r= -g- =b= ]
                else if (ColorLevel == 3)
                {
                    NoteSprite.color -= new Color(0, 0.08f, 0, 0);
                    if (NoteSprite.color.g <= 0.4f) ColorLevel = 4;
                }
                // [ +r+ =g= =b= ]
                else if (ColorLevel == 4)
                {
                    NoteSprite.color += new Color(0.08f, 0, 0, 0);
                    if (NoteSprite.color.r > 1.0f) ColorLevel = 5;
                }
                // [ =r= =g= -b- ]
                else if (ColorLevel == 5)
                {
                    NoteSprite.color -= new Color(0, 0, 0.08f, 0);
                    if (NoteSprite.color.b <= 0.4f) ColorLevel = 0;
                }
            }
            // blue
            else
            {
                //rainbow color animation
                // [ =r= +g+ =b= ]
                if (ColorLevel == 0)
                {
                    NoteSprite.color += new Color(0, 0.08f, 0, 0);
                    if (NoteSprite.color.g > 1.0f) ColorLevel = 1;
                }
                // [ =r= =g= -b- ]
                else if (ColorLevel == 1)
                {
                    NoteSprite.color -= new Color(0, 0, 0.08f, 0);
                    if (NoteSprite.color.b <= 0.4f) ColorLevel = 2;
                }
                // [ +r+ =g= =b= ]
                else if (ColorLevel == 2)
                {
                    NoteSprite.color += new Color(0.08f, 0, 0, 0);
                    if (NoteSprite.color.r > 1.0f) ColorLevel = 3;
                }
                // [ =r= -g- =b= ]
                else if (ColorLevel == 3)
                {
                    NoteSprite.color -= new Color(0, 0.08f, 0, 0);
                    if (NoteSprite.color.g <= 0.4f) ColorLevel = 4;
                }
                // [ =r= =g= +b+ ]
                else if (ColorLevel == 4)
                {
                    NoteSprite.color += new Color(0, 0, 0.08f, 0);
                    if (NoteSprite.color.b > 1.0f) ColorLevel = 5;
                }
                // [ -r- =g= =b= ]
                else if (ColorLevel == 5)
                {
                    NoteSprite.color -= new Color(0.08f, 0, 0, 0);
                    if (NoteSprite.color.r <= 0.4f) ColorLevel = 0;
                }
            }
        }
        // Judge : Bad (Late)
        else if (iJudge == 4)
        {
            // No Animation
        }
    }
}
