using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HideOutType
{
        WARDROBE,
        TABLE
};

public class HideOutScript : MonoBehaviour
{

    [SerializeField]
    private HideOutType hideOutType = HideOutType.WARDROBE;

    private Vector3 positionWhenHide;
    // Start is called before the first frame update
    void Start()
    {
        if (hideOutType == HideOutType.WARDROBE)
        {
            positionWhenHide = GetComponent<Renderer>().bounds.center;
        }
        else if(hideOutType == HideOutType.TABLE)
        {
            positionWhenHide = GetComponent<Renderer>().bounds.center;
            positionWhenHide.y = 0;
        }
    }


    public Vector3 getPositionWhenHide()
    {
        return positionWhenHide;
    }

    public HideOutType getHideOutType()
    {
       return hideOutType;
    }
}
