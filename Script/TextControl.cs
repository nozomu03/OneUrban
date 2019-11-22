using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextControl : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    string[] texts;
    [SerializeField]
    Text output;
    [SerializeField]
    private GameObject[] audio_appear = null;
    bool running = false;
    int count_limit = 0;
    int how_many= 0;
    float delay_time = 0.0f;
    string now_string;
    string temp = null;
    string cut_string = null;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        output.text = "";
        //Debug.Log(texts.Length + ":" + count_limit);
    }

    // Update is called once per frame

    
    void Update()
    {
        count_limit = texts.Length;
        //Debug.Log("continue");
        if (!running)
        {
            if(count_limit == 0)
            {
                if (output.gameObject.name.Equals("nvl_message"))
                {
                    output.gameObject.SetActive(false);
                    GlobalData.can_walk = true;
                }
            }                                  
            else if (how_many == 0 )
            {
                now_string = texts[0];
                CoroutineCaller();
            }
            else if (how_many < count_limit)
            {
                now_string = texts[how_many];
                delay_time += Time.deltaTime;
                if (delay_time >= 1.0)
                {
                    delay_time = 0.0f;
                    CoroutineCaller();
                }
            }
        }
    }

    private void CoroutineCaller()
    {
        StartCoroutine("TextPrinter");
        how_many++;

    }

    IEnumerator TextPrinter()
    {
        running = true;
        GlobalData.can_walk = false;
        int i;
        for(i=0; i <= now_string.Length; i++)
        {            
            if (i < now_string.Length && now_string[i].Equals('p'))
            {
                cut_string = now_string;
                now_string = cut_string.Substring(0, i - 1) + " " + cut_string.Substring(i + 1);
                yield return new WaitForSeconds(1.0f);
            }
            else if(i < now_string.Length && now_string[i].Equals('s'))
            {
                Debug.Log("sound system ingage");
                cut_string = now_string;
                now_string = cut_string.Substring(0, i - 1) + " " + cut_string.Substring(i + 1);
                GlobalData.audios[count].Play();
                yield return new WaitForSeconds(3.0f);
                GlobalData.audios[count].Stop();
                audio_appear[count].SetActive(true);
                count++;
            }
            temp = now_string.Substring(0, i);    
            output.text = temp;
            yield return new WaitForSeconds(0.1f);
        
        }
        if (i == now_string.Length + 1)
        {
            running = false;            
            if (how_many == count_limit)
            {
                if (output.gameObject.name.Equals("nvl_message"))
                {
                    output.gameObject.SetActive(false);
                }
                GlobalData.can_walk = true;
                output.text = "";
                this.enabled = false;
                how_many = 0;
                // this.enabled = false;
            }
        }

    }    
}
