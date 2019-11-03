using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveAndLoad : MonoBehaviour
{
    private float time = 0.0f;
    [SerializeField]    
    private Text text;
    private string path;
    // Start is called before the first frame update   
    private void Start()
    {
        GlobalData.now_position = GlobalData.temp_position;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        path = Application.persistentDataPath + "/save.dat";
    }

    private void Update()
    {
        if (text.enabled)
        {
            Debug.Log(time);
            if (time < 3.0f)
            {
                time += Time.deltaTime;
            }
            else
            {
                time = 0.0f;
                text.enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(GlobalData.scene_index);
        }
    }  

    public void Save()
    {
        time = 0.0f;
        Debug.Log("Hello");
        GameData gameData = new GameData(GlobalData.scene_index, GlobalData.now_position.x, GlobalData.now_position.y, GlobalData.now_position.z, GlobalData.rotate_spped, GlobalData.nvl_screen);
        text.enabled = true;
        real_Save(gameData);
    }

    private void real_Save(GameData gameData)
    {
        BinaryFormatter format = new BinaryFormatter();
        FileStream save = File.Create(path);
        Debug.Log(gameData.ToString());
        format.Serialize(save, gameData);
        save.Close();
    }

    public void real_Load()
    {
        if (File.Exists(path))
        {
            BinaryFormatter format = new BinaryFormatter();
            FileStream save = File.Open(path, FileMode.Open);
            GlobalData.loaded_data = (GameData)format.Deserialize(save);
            save.Close();
            Debug.Log("data: " + GlobalData.loaded_data.ToString());            
            SceneManager.LoadScene(GlobalData.loaded_data.now_sceneIndex);
        }
        else
        {
            GlobalData.loaded_data = new GameData(1, -232.1f, 0, -104.9f, 3.0f, true);
            SceneManager.LoadScene(1);            
        }
        GlobalData.now_position = new Vector3(GlobalData.loaded_data.player_x, GlobalData.loaded_data.player_y, GlobalData.loaded_data.player_z);
        GlobalData.rotate_spped = GlobalData.loaded_data.rotate_speed;
        GlobalData.nvl_screen = GlobalData.loaded_data.nvl_screen;
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
}
