using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{
    public DayNightController dayNight;

    public TextMeshProUGUI text;

    private List<Object> memories = new List<Object>();
    public Collider memoryPrefab;

    private bool isHidden = false;

    // Start is called before the first frame update
    void Start()
    {
        PlaceMemory();
    }

    void PlaceMemory() {
        Collider memory = (Collider) Instantiate(memoryPrefab);
    }

    // Update is called once per frame
    void Update()
    {

        int hour = (int)Mathf.Floor(dayNight.GetCurrentTime());
        int min = (int)((dayNight.GetCurrentTime() % 1) * 60);
        text.SetText(string.Format("Day: {0}, Time: {1}:{2}", dayNight.getCurrentDay(), hour, min));
    }

    void Sleep() {
        
    }

    void WakeUp() {

    }

    public void toggleHidden()
    {
        isHidden = !isHidden;
    }

    public bool getIsHidden()
    {
        return isHidden;
    }
}
