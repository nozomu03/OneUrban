using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FemaleCon : MonoBehaviour
{
    [SerializeField]
    private GameObject[] trigger = null;
    [SerializeField]
    private Vector3[] waypoint = null;
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private ParticleSystem particle = null;    
    private Animator anim;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            Map1Con();
        }
    }

    private void Map1Con()
    {
        Debug.Log(trigger[0].gameObject.activeSelf);
        if (trigger[0].gameObject.activeSelf)
        {
            Vector3 temp = transform.position;
            particle.gameObject.SetActive(false);
            anim.SetBool("smoking", false);
            anim.SetBool("walking", true);
            if (count < waypoint.Length)
            {
                Debug.Log("TTT");
                float frame = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(temp, waypoint[count], frame);
                Debug.Log(Vector3.Distance(waypoint[count], transform.position));
                if (Vector3.Distance(waypoint[count], transform.position) == 0f)
                {
                    if (count == 0)
                        transform.rotation = Quaternion.Euler(0, 90, 0);
                    count++;
                }
            }
            else
            {
                anim.SetBool("walking", false);
                trigger[0].gameObject.tag = "Searchable";
            }
        }
    }
}
