using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    [SerializeField]
    Text text;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        slider = gameObject.GetComponent<Slider>();
        slider.value = GlobalData.rotate_spped;
    }

    // Update is called once per frame
    public void ChangeEvent()
    {
        GlobalData.rotate_spped = slider.value;
        text.text = "현재 수치: " + GlobalData.rotate_spped;
    }
}
