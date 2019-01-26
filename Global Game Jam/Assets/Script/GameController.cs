using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{
    public DayNightController dayNight;

    public TextMeshProUGUI text;

    private List<Transform> memories = new List<Transform>();
    public Collider memoryPrefab;

    public List<Texture2D> textures = new List<Texture2D>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < textures.Count; i++) {
            PlaceMemory(transform.position + new Vector3(i * 1.0f, 0.0f, 0.0f), textures[i]);
        }
    }

    void PlaceMemory(Vector3 pos, Texture2D texture) {
        Transform mem = (Transform) Instantiate(memoryPrefab).transform;
        mem.transform.position = pos;
        Renderer renderer = mem.Find("Frame").GetComponent<Renderer>();
        renderer.material = Instantiate(renderer.material);
        renderer.material.SetTexture("_MainTex", texture);
        memories.Add(mem);
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
}
