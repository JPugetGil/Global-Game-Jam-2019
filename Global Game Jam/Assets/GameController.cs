using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private float timeOfDay;


    // how fast time of day changes
    public float timeFactor = 1;

    public Light lights;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay = (timeFactor * Time.time) % 24;

        lights.intensity = Mathf.InverseLerp(0, 24, timeOfDay);
    }
}
