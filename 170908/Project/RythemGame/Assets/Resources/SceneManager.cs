using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

    public string MusicNumber;

    public float AnimTime;

    public Image Fade;

    public bool FadeIO, FisPlay;
    enum FadeColor{ WHITE, BLACK };
    FadeColor FClr;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        FClr = FadeColor.BLACK;
        Fade.color = new Color(0, 0, 0, 0);
        FadeIO = false; FisPlay = false;
    }

    private void Awake()
    {
        Screen.SetResolution(800, 450, false);
    }

    private void Update()
    {
        transform.SetAsLastSibling();

        if (FClr == FadeColor.BLACK)
        {
            Fade.color = new Color(0, 0, 0, Fade.color.a);
        }
        else if (FClr == FadeColor.WHITE)
        {
            Fade.color = new Color(1, 1, 1, Fade.color.a);
        }

        if (FisPlay)
        {
            // Fade Out ... alpha 0->1
            if(FadeIO)
            {
                if (!(Fade.color.a >= 1f))
                    FadeOut(AnimTime);
                else
                {
                    // Trans :: Fade Out -> In
                    FadeIO = !FadeIO;
                    FisPlay = false;
                }
            }
            // Fade In ... alpha 1->0
            else
            {
                if (!(Fade.color.a <= 0f))
                    FadeIn(AnimTime);
                else
                {
                    FadeIO = !FadeIO;
                    FisPlay = false;
                }
            }
        }
    }

    void FadeIn(float time)
    {
        float AlphaValue = Time.deltaTime / (time - 0.3f);
        // -0.3 seconds is for spontaneous Animation

        Fade.color = new Color(
            Fade.color.r,
            Fade.color.g,
            Fade.color.b,
            Fade.color.a - AlphaValue);
    }
    void FadeOut(float time)
    {
        float AlphaValue = Time.deltaTime / (time - 0.3f);

        Fade.color = new Color(
            Fade.color.r,
            Fade.color.g,
            Fade.color.b,
            Fade.color.a + AlphaValue);
    }
}
