using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartcleCon : MonoBehaviour
{    
    [SerializeField]
    ParticleSystem smoke;    
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        smoke = smoke.GetComponent<ParticleSystem>();
        anim = gameObject.GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(anim.GetBool("smoking"));
        kemuri();
    }

    private void kemuri()
    {
       if (anim.GetBool("smoking"))
       {
           smoke.gameObject.SetActive(true);
       }
       else
       {
           smoke.gameObject.SetActive(false);
       }
    }
}
