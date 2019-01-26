using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{

	private static GameController _instance;

    public static GameController Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI centerText;

    private List<Transform> dayMemories = new List<Transform>();
    public Collider dayMemoryPrefab;

    private List<Transform> nightMemories = new List<Transform>();
    public Collider nightMemoryPrefab;

    public List<Texture2D> memoryImages = new List<Texture2D>();
    public List<string> memoryText = new List<string>();
    private bool isHidden = false;

    // Start is called before the first frame update

    Vector3 RandomPosition() {
         Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
         // check placement
        return position;
    }
    void Start()
    {
        for (int i = 0; i < memoryImages.Count; i++)
        {
            Transform dayMemory = (Transform)Instantiate(dayMemoryPrefab).transform;
            PlaceMemory(dayMemory, transform.position + RandomPosition(), memoryImages[i]);
            dayMemories.Add(dayMemory);
            DayNightController.Instance.AddDayObject(dayMemory.gameObject);

            Transform nightMemory = (Transform)Instantiate(nightMemoryPrefab).transform;
            PlaceMemory(nightMemory, transform.position + RandomPosition(), memoryImages[i]);
            nightMemories.Add(nightMemory);
            DayNightController.Instance.AddNightObject(nightMemory.gameObject);
        }

                    centerText.enabled = false;

    }

    void PlaceMemory(Transform mem, Vector3 pos, Texture2D texture)
    {

        mem.transform.position = pos;
        Renderer renderer = mem.Find("Frame").GetComponent<Renderer>();
        renderer.material = Instantiate(renderer.material);
        renderer.material.SetTexture("_MainTex", texture);

    }

    // Update is called once per frame
    void Update()
    {

        int hour = (int)Mathf.Floor(DayNightController.Instance.GetCurrentTime());
        int min = (int)((DayNightController.Instance.GetCurrentTime() % 1) * 60);
        timeText.SetText(string.Format("Day: {0}, Time: {1}:{2}", DayNightController.Instance.getCurrentDay(), hour, min));

        int matches = 0;

        for (int i = 0; i < memoryImages.Count; i++)
        {
            Vector3 dayPos = dayMemories[i].position;
            Vector3 nightPos = nightMemories[i].position;
            float distance = (dayPos - nightPos).magnitude;
            if (distance < 1.0f) {
                matches += 1;
            }
        }

        if (matches == memoryImages.Count && DayNightController.Instance.getCurrentDay() < 5) {
            centerText.SetText("You Won!");
            centerText.enabled = true;
            Debug.Log("You Won!");
        } else if (DayNightController.Instance.getCurrentDay() > 5) {
            centerText.SetText("You Lost!");
            centerText.enabled = true;
             Debug.Log("Game Over!");
        }
    }

    void EnableMemory(Transform memory, bool enable) {
        foreach(Renderer renderer in memory.GetComponentsInChildren<Renderer>()) {
            renderer.enabled = enable;
        }
        foreach(Light light in memory.GetComponentsInChildren<Light>()) {
            light.enabled = enable;
        }


    }

    void Sleep()
    {

    }

    void WakeUp()
    {

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
