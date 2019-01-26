﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private float animationTime = 2f;


    [SerializeField]
    private GameObject gameControllerObject, canvas;

    private float lastAnimation = 0.0f;

    private bool isHidden = false;
    private Rigidbody rigidBody;

    private Vector3 positionWhenGetOut;
    private GameController gameController;
    private dialogScript textContainter;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        gameController = gameControllerObject.GetComponent<GameController>();
        textContainter = canvas.GetComponentInChildren<dialogScript>();
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
            Debug.Log("FIRE!");
          

            if (gameController.getIsHidden())
            {
                lastAnimation = animationTime;
                getOut();
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 2))
                {
                    if (hit.transform.CompareTag("memories"))
                    {
                        lastAnimation = animationTime;
                        PickUpObject(hit.transform.gameObject);
                    }
                    else if (hit.transform.CompareTag("hideout"))
                    {
                        lastAnimation = animationTime;
                        Hide(hit.transform.gameObject);
                    }
                    else if (hit.transform.CompareTag("spot"))
                    {
                        lastAnimation = animationTime;
                        Put(hit.transform.gameObject);
                    }
                }
            }
        }
    }
    
    
    private void PickUpObject(GameObject objet)
    {
        Debug.Log("Try to pick an object");
        /*Move hand */
        // objet.GetComponent<>().getPickUpText();
        textContainter.setText("This is my best memory!");
    }

    private void Put(GameObject objet)
    {
        Debug.Log("Put");
        /*Animation to hide*/
        // objet.GetComponent<>().getPutText();
    }


    private void Hide(GameObject objet)
    {
        gameController.toggleHidden();
        //TODO stop moving
        //Ignore the collisions between layer 0 (default) and layer 8 (custom layer you set in Inspector window)
        Physics.IgnoreLayerCollision(9, 10);
        Vector3 objetPosition = objet.GetComponent<HideOutScript>().getPositionWhenHide();
        Debug.Log("Hide");
        /*Animation to hide*/
        positionWhenGetOut = transform.position;
        transform.position = new Vector3(objetPosition.x, objetPosition.y, objetPosition.z);
    }



    private void getOut()
    {
        gameController.toggleHidden();
        Debug.Log("getOut");
        /*Animation to hide*/
        Physics.IgnoreLayerCollision(9, 10, false);
        transform.position = positionWhenGetOut;
    }
}
