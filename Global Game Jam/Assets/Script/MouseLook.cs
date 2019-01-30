using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 5.0f, smoothing = 2.0f;

    [SerializeField]
    private int maxYAxis = 80, minYAxis = 90;

    [SerializeField]
    private int maxHiddenYAxis = 45, minHiddenYAxis = 45;

    [SerializeField]
    private int maxHiddenXAxis = 45, minHiddenXAxis = 45;

    [SerializeField]
    private GameObject gameControllerObject;

    private GameController gameController;

    Vector2 mouseLook, smoothV;

    Transform character;

    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        character = transform.parent.gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var inputs = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        inputs = Vector2.Scale(inputs, new Vector2(sensitivity * smoothing * Time.deltaTime, sensitivity * smoothing * Time.deltaTime));
        smoothV.x = Mathf.Lerp(smoothV.x, inputs.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, inputs.y, 1f / smoothing);
        mouseLook += smoothV;

        //We limit the player Y axis (think about his neck)
        mouseLook.y = Mathf.Clamp(mouseLook.y, -minYAxis, maxYAxis);

        if (gameController.getIsHidden())
        {
           // mouseLook.x = Mathf.Clamp(mouseLook.x, -minHiddenXAxis , maxHiddenXAxis);
            mouseLook.y = Mathf.Clamp(mouseLook.y, -minHiddenYAxis, maxHiddenYAxis);
        }

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(mouseLook.x, character.up);
    }

}
