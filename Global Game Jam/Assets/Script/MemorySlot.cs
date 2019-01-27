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

    void setDayMemory(GameObject obj)
    {
        if (nightMemory)
        {
            Debug.LogError("Night Memory already set.");
        }
        else
        {
            dayMemory = obj;
        }
    }

    private Texture getMemoryTexture(GameObject obj)
    {
        Renderer renderer = obj.transform.Find("Frame").GetComponent<Renderer>();
        return renderer.material.GetTexture("_MainTex");
    }

    bool setNightMemory(GameObject obj)
    {
        if (nightMemory)
        {
            Debug.LogError("Night Memory already set.");
            return false;
        }
        else
        {
            if (getMemoryTexture(dayMemory) != getMemoryTexture(obj))
            {
                Debug.LogError("Memories do not Match");
                return false;
            }
            else
            {
                nightMemory = obj;
                if (matchingEffect) {
                    matchingEffect.Play();
                }
                return true;
            }
        }
    }
}
