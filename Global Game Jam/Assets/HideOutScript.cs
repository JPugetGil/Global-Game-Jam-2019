using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOutScript : MonoBehaviour
{
    private Vector3 positionWhenHide;
    // Start is called before the first frame update
    void Start()
    {
        positionWhenHide = GetComponent<Renderer>().bounds.center;
    }


    public Vector3 getPositionWhenHide()
    {
        return positionWhenHide;

    }
}
