using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharControl : MonoBehaviour
{
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
    private Text seraching;
    Rigidbody rb;
    float rotate = 0.0f;
    float rotate_limit = 80f;
    float rotate_temp = 0.0f;
    bool search = false;
    GameObject searchable_thing = null;

    // Start is called before the first frame update
    void Start()
    {
        nvl_screen.gameObject.SetActive(GlobalData.nvl_screen);
        rotate_speed = GlobalData.rotate_spped;
        gameObject.transform.position = new Vector3(GlobalData.now_position.x, GlobalData.now_position.y, GlobalData.now_position.z);
        rb = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
            SceneManager.LoadScene("Save_Load");
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("H");
            transform.Translate(Vector3.forward * move_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * move_speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * move_speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * move_speed * Time.deltaTime);
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
        if (search)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
//                Debug.Log(searchable_thing.name);
                dialogs[int.Parse(searchable_thing.name)-1].enabled = true;                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ingage");
        if (other.gameObject.tag == "Searchable")
        {
            seraching.gameObject.SetActive(true);
            searchable_thing = other.gameObject;
            search = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        seraching.gameObject.SetActive(false);
        searchable_thing = null;
        search = false;
    }
}
