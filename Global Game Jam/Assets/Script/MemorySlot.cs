using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySlot : MonoBehaviour
{
    private GameObject dayMemory;
    private GameObject nightMemory;

    public ParticleSystem matchingEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDayMemory(GameObject obj)
    {
        if (nightMemory)
        {
            Debug.LogError("Night Memory already set.");
        }
        else
        {
            dayMemory = obj;
            dayMemory.transform.parent = transform;
            dayMemory.transform.localPosition = Vector3.zero;
            dayMemory.transform.localRotation = Quaternion.identity;
        }
    }

    private Texture GetMemoryTexture(GameObject obj)
    {
        Renderer renderer = obj.transform.Find("Frame").GetComponent<Renderer>();
        return renderer.material.GetTexture("_MainTex");
    }

    public bool SetNightMemory(GameObject obj)
    {
        if (nightMemory)
        {
            Debug.LogError("Night Memory already set.");
            return false;
        }
        else
        {
            if (GetMemoryTexture(dayMemory) != GetMemoryTexture(obj))
            {
                Debug.LogError("Memories do not match");
                return false;
            }
            else
            {
                Debug.LogError("Memories match!");
                nightMemory = obj;
                nightMemory.transform.parent = transform;
                nightMemory.transform.localPosition = Vector3.zero;
                nightMemory.transform.localRotation = Quaternion.identity;

                if (matchingEffect)
                {
                    matchingEffect.Play();
                }

                return true;
            }
        }
    }

    public GameObject GetDayMemory()
    {
        return dayMemory;
    }
    public bool IsMatched()
    {
        return (dayMemory && nightMemory);
    }
}
