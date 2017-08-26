using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeEffect : MonoBehaviour {

    public bool bNoteColor;  // true : red(Up)   false : blue(Down)
    public Animator EftAnim; // Effect Animation
    public SpriteRenderer NoteSprite; // Ring Sprite
    public int iJudge;

    public int frame;

	// Use this for initialization
	void Start () {
        frame = 0;
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
            NoteSprite.color = new Color(0, 0, 0, 1);
        }
        // Judge : Great
        else if (iJudge == 1 || iJudge == 3)
        {
            NoteSprite.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        // Judge : Perfect!!
        else if (iJudge == 2)
        {
            NoteSprite.color = new Color(1, 1, 1, 1);
        }
        // Judge : Bad (Late)
        else if (iJudge == 4)
        {
            NoteSprite.color = new Color(0, 0, 0, 1);
        }
    }
}
