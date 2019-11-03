using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharControl : MonoBehaviour
{
    [SerializeField]
    private Vector3 next_position;
    [SerializeField]
    private TextControl nvl_screen;
    [SerializeField]
    private float move_speed = 100f;
    [SerializeField]
    private float rotate_speed = 3f;
    [SerializeField]
    private float rotate_horiSpeed = 100f;
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private Text textbox;
    [SerializeField]
    private TextControl[] dialogs;
    [SerializeField]
    private Text searching;
    [SerializeField]
    private int need_search;
    private int now_search = 0;
    Rigidbody rb;
    float rotate = 0.0f;
    float rotate_limit = 80f;
    float rotate_temp = 0.0f;
    bool search = false;
    bool end = false;
    bool list_check = false;
    List<string> add_checker; 
    Animator anim;
    GameObject searchable_thing = null;

    // Start is called before the first frame update
    void Start()
    {
        nvl_screen.gameObject.SetActive(GlobalData.nvl_screen);
        rotate_speed = GlobalData.rotate_spped;
        if (GlobalData.run_mod)
        {
            gameObject.transform.position = new Vector3(GlobalData.now_position.x, GlobalData.now_position.y, GlobalData.now_position.z);
        }
        else
        {
            GlobalData.run_mod = true;  
        }
        rb = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        add_checker = new List<string>();
        Debug.Log(Cursor.lockState);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GlobalData.can_walk);
        if (GlobalData.can_walk)
        {
            Move();
            Call_SaveLoad();
        }
        DialogCall();
    }

    private void FixedUpdate()
    {
        if (GlobalData.can_walk)
        {
            Rotate();
        }
    }

    private void Call_SaveLoad()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            GlobalData.scene_index = SceneManager.GetActiveScene().buildIndex;
            GlobalData.temp_position = gameObject.transform.position;
            GlobalData.nvl_screen = nvl_screen.isActiveAndEnabled;
            Debug.Log(nvl_screen.isActiveAndEnabled);
            GlobalData.goSet = true;
            SceneManager.LoadScene("Save_Load");
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("H");
            transform.Translate(Vector3.forward * move_speed * Time.deltaTime);
            anim.SetBool("walking", true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * move_speed * Time.deltaTime);
            anim.SetBool("walking", true);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * move_speed * Time.deltaTime);
            anim.SetBool("walking", true);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * move_speed * Time.deltaTime);
            anim.SetBool("walking", true);
        }
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("walking", false);
        }
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * rotate_speed * Input.GetAxis("Mouse X"));
        if (camera != null)
        {           
            rotate_temp = Input.GetAxis("Mouse Y");
            rotate += rotate_temp * rotate_horiSpeed * Time.deltaTime;
            rotate = Mathf.Clamp(rotate, -rotate_limit, rotate_limit);
            //Debug.Log(rotate);
            if (rotate != 0)
            {
                rotate_temp = -rotate;
            }
            camera.transform.localEulerAngles = new Vector3(rotate_temp, 0f, 0f);
        }
    }

    private void DialogCall()
    {
        Debug.Log(anim.GetCurrentAnimatorStateInfo(0).shortNameHash);
        if (anim.GetCurrentAnimatorStateInfo(0).shortNameHash == -1259283545)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.8f)
            {
                //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
                anim.SetBool("search", false);
            }
        }
        if (search)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (end)
                {
                    GlobalData.now_position = next_position;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    anim.SetBool("search", true);
                    dialogs[int.Parse(searchable_thing.name) - 1].enabled = true;              
                    foreach (string temp in add_checker)
                    {
                        if (temp == searchable_thing.gameObject.name)
                            list_check = true;
                    }
                    if (!list_check)
                    {
                        now_search++;
                        add_checker.Add(searchable_thing.gameObject.name);
                    }
                    list_check = false;
                }                
            }
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ingage");        
        if (other.gameObject.tag == "Searchable")
        {
            searching.gameObject.SetActive(true);
            searchable_thing = other.gameObject;
            search = true;
        }
        else if(other.gameObject.tag == "mapend")
        {
            if (now_search >= need_search)
            {
                searching.gameObject.SetActive(true);
                end = true;
                search = true;                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        searching.gameObject.SetActive(false);
        searchable_thing = null;
        search = false;
    }
}
