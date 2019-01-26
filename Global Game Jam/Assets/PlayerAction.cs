using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private float animationTime = 2f;

    private float lastAnimation = 0.0f;
 // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lastAnimation > 0)
        {
            lastAnimation -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && lastAnimation <= 0.0f)
        {
            lastAnimation = animationTime;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.CompareTag("memories"))
                {
                    PickUpObject();
                }
                else if (hit.transform.CompareTag("hideout"))
                {
                    Hide();
                }
            }
        }
    }
    
    
    private void PickUpObject()
    {
        Debug.Log("Try to pick an object");
        /*Move hand */
    }


    private void Hide()
    {
        Debug.Log("Hide");
        /*Animation to hide*/
    }
}
