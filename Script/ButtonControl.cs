using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{

    public void OK_Click()
    {
        SceneManager.LoadScene("Save_Load");
    }
    
    public void Go_Preference()
    {
        SceneManager.LoadScene("Preference");
    }
}
