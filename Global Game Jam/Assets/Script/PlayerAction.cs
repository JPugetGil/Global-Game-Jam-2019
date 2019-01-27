using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private float animationTime = 0.5f;


    [SerializeField]
    private GameObject gameControllerObject, canvas;

    private float lastAnimation = 0.0f;
    public float distance = 5.0f;
    private bool isHidden = false;
    private Rigidbody rigidBody;

    private Vector3 positionWhenGetOut;
    private GameController gameController;
    private dialogScript textContainter;

    private GameObject memory;
    private GameObject cardboard;

    public Transform memorySlot;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        gameController = gameControllerObject.GetComponent<GameController>();
        textContainter = canvas.GetComponentInChildren<dialogScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool day = (DayNightController.Instance.isDay());

        if (lastAnimation > 0)
        {
            lastAnimation -= Time.deltaTime;
        }

        //Drop auto if it's day
        if (day && memory)
        {
            Drop();
        }


        if (Input.GetButtonUp("Fire1") && lastAnimation <= 0.0f)
        {
            Debug.Log("FIRE!");

            if (gameController.getIsHidden())
            {
                lastAnimation = animationTime;
                getOut();
            }
            else
            {
                if (cardboard)
                {
                    lastAnimation = animationTime;
                    Throw();
                }
                else
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, distance))
                    {
                        Debug.Log(hit.transform);
                        if (!day && memory && hit.transform.CompareTag("memorySlot"))
                        {
                            lastAnimation = animationTime;
                            PutInSlot(hit.transform.gameObject);
                        }
                        else if (!day && hit.transform.CompareTag("memories"))
                        {
                            lastAnimation = animationTime;
                            PickUpObject(hit.transform.gameObject);
                        }
                        else if (hit.transform.CompareTag("hideout"))
                        {
                            lastAnimation = animationTime;
                            Hide(hit.transform.gameObject);
                        }
                        else if (hit.transform.CompareTag("cardboard"))
                        {
                            lastAnimation = animationTime;
                            PickUpCardboard(hit.transform.gameObject);
                        }
                    }
                }
            }
        }

        if (Input.GetButtonUp("Fire2"))
        {
            if (!day && memory)
            {
                Drop();
            }
        }
    }

        private void PickUpObject(GameObject objet)
    {
        Debug.Log("Try to pick an object");
        memory = objet;
        objet.transform.parent = memorySlot.transform;
        objet.transform.localPosition = Vector3.zero;
        /*Move hand */
        string text = gameController.memoryText[DayNightController.Instance.getSpecificIndex(objet)];
        textContainter.setText(text);
    }

    private void PickUpCardboard(GameObject objet)
    {
        Debug.Log("PickUpCardboard");
        /*Animation to hide*/
        // objet.GetComponent<>().getPutText();
        cardboard = objet;
        objet.transform.parent = memorySlot.transform;
        objet.transform.position = Vector3.zero;
    }
    private void Drop()
    {
        Debug.Log("Drop");
        /*Animation to hide*/
        // objet.GetComponent<>().getPutText();
        memory.transform.parent = null;
        memory.transform.position = transform.position;
        memory = null;
    }

    private void PutInSlot(GameObject objet)
    {
        Debug.Log("Put in slot");
        /*Animation to hide*/
        // objet.GetComponent<>().getPutText();
        if (objet.GetComponent<MemorySlot>().SetNightMemory(memory))
            {
            memory = null;
        }
    }

    private void Throw()
    {
        Debug.Log("Throw");
        /*Animation to hide*/
        // objet.GetComponent<>().getPutText();
        cardboard.transform.parent = null;
        cardboard.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 100));
        cardboard = null;
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
