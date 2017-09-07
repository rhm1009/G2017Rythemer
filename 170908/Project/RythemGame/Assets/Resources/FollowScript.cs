using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {

    public GameObject Obj_From;
    public GameObject Obj_To;
    Vector3 To_Pos;

    public bool x;
    public bool y;
    public bool z;

    private void Start()
    {
        To_Pos = Obj_From.transform.position;
    }

    private void Update () {
        bool bExistence;
        bExistence = (Obj_From != null && Obj_To != null);

        if (bExistence)
        {
            if (x)
            {
                To_Pos = new Vector3(
                    Obj_To.transform.position.x,
                    To_Pos.y, To_Pos.z);
            }
            if (y)
            {
                To_Pos = new Vector3(To_Pos.x,
                    Obj_To.transform.position.y,
                    To_Pos.z);
            }
            if (z)
            {
                To_Pos = new Vector3(To_Pos.x, To_Pos.y,
                    Obj_To.transform.position.z);
            }
        }

        Obj_From.transform.position = To_Pos;
	}
}
