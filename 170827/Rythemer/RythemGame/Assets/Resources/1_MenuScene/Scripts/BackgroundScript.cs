using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundScript : MonoBehaviour {

    public bool bInit;
    int tick; // event tick Count

    float fColor;
    float fScale;
    
	void Start () {
        bInit = true;
        tick = 0;
        fColor = 0f;
        fScale = 4f;

        gameObject.GetComponent<SpriteRenderer>().color = new Color(fColor, fColor, fColor, 1);
    }

    void Update()
    {
        if (bInit)
        {
            if (tick == 0)
            {
                fColor += 0.01f;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(fColor, fColor, fColor, 1);

                if (fColor >= 0.7f)
                {
                    tick++;
                }
            }
            else if (tick == 1)
            {
                fColor -= 0.01f;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(fColor, fColor, fColor, 1);

                if (fColor <= 0.4f)
                {
                    tick++;
                }
            }
            else if (tick == 2)
            {
                fColor += 0.01f;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(fColor, fColor, fColor, 1);

                if (fColor >= 0.8f)
                {
                    tick++;
                }
            }
            else if (tick == 3)
            {
                fColor -= 0.01f;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(fColor, fColor, fColor, 1);

                if (fColor <= 0.5f)
                {
                    tick++;
                }
            }
            else if (tick == 4)
            {
                fColor += 0.01f;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(fColor, fColor, fColor, 1);

                if (fColor >= 0.9f)
                {
                    tick = 0; bInit = false;
                }
            }
        }
        else
        {
            if (tick == 0)
            {
                fColor += 0.01f;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(fColor, fColor, fColor, 1);

                //fScale += 0.01f;
                //gameObject.transform.localScale = new Vector2(fScale, fScale);

                if (fColor >= 1f)
                {
                    tick++;
                }
            }
            else if (tick == 1)
            {
                fColor -= 0.01f;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(fColor, fColor, fColor, 1);

                //fScale -= 0.01f;
                //gameObject.transform.localScale = new Vector2(fScale, fScale);

                if (fColor <= 0.9f)
                {
                    tick = 0;
                }
            }
        }
    }
}
