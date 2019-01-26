using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogScript : MonoBehaviour
{

    [SerializeField]
    private float readingSpeed = 0.1f;
    private float lastSecondSpeed = 1f;

    private string text = "";
    private float textTime = 0.0f;
    private float lastSecondTime = 0.0f;
    private int letterIndex = 0;

    private TextMeshProUGUI textContainer;
    private bool active = false;

    private void Start()
    {
        textContainer =  GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (active)
        {
            //Debug.Log(textTime);
            if (textTime > 0)
            {
                textTime -= Time.deltaTime;
            }
            else
            {
                textTime = readingSpeed;
                if (letterIndex < text.Length)
                {
                    textContainer.text += text[letterIndex];
                    letterIndex++;
                }
                else
                {
                    active = false;
                    lastSecondTime = lastSecondSpeed;
                }
            }
        }
        else
        {
            if (lastSecondTime > 0)
            {
                lastSecondTime -= Time.deltaTime;
            }
            else
            {
                textContainer.text = "";
            }
        }
    }

    public void setText(string p_text)
    {
        active = true;
        letterIndex = 0;
        text = p_text;
    }
}
