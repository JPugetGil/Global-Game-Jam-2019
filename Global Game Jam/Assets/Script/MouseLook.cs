using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 5.0f, smoothing = 2.0f;

    [SerializeField]
    private int maxYAxis = 90, minYAxis = 80;

    [SerializeField]
    private int maxHiddenXAxis = 45, minHiddenXAxis = 45;

    [SerializeField]
    private GameObject gameControllerObject;

    private GameController gameController;

    Vector2 mouseLook, smoothV;

    GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        character = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var inputs = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        inputs = Vector2.Scale(inputs, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, inputs.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, inputs.y, 1f / smoothing);
        mouseLook += smoothV;

        //We limit the player Y axis (think about his neck)
        float valueConstrainedY = Mathf.Max(-mouseLook.y, -maxYAxis);
        valueConstrainedY = Mathf.Min(valueConstrainedY, minYAxis);

        float valueConstrainedX = mouseLook.x;
        if (gameController.getIsHidden())
        {
            valueConstrainedX = Mathf.Max(mouseLook.x, -maxHiddenXAxis);
            valueConstrainedX = Mathf.Min(valueConstrainedX, minHiddenXAxis);
        }

        transform.localRotation = Quaternion.AngleAxis(valueConstrainedY, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(valueConstrainedX, character.transform.up);
    }

}
