using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public DayNightController dayNight;

    bool day = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (day) {
            if (dayNight.GetCurrentTime() < 18) {
                dayNight.SetCurrentTime(18);
                dayNight.SetPaused(false);
            } else if (dayNight.GetCurrentTime() > 19) {
                day = false;
            }
        } else {
            if (dayNight.GetCurrentTime() < 6  || dayNight.GetCurrentTime() < 24) {
                dayNight.SetCurrentTime(6);
                dayNight.SetPaused(false);
            } else if (dayNight.GetCurrentTime() > 7) {
                day = true;
            }
        }
    }
}
