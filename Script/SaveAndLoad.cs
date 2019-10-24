using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndLoad : MonoBehaviour
{
    private string path;
    // Start is called before the first frame update   

    private void Start()
    {
        path = Application.persistentDataPath + "/save.dat";
    }
public void Save()
    {
        Debug.Log("Hello");        
        GameData gameData = new GameData(GlobalData.scene_index, GlobalData.now_position.x, GlobalData.now_position.y, GlobalData.now_position.z);
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
        }
        else
        {
            GlobalData.loaded_data = new GameData(0, 0, 0, 0);
        }
        Debug.Log(GlobalData.loaded_data.ToString());
    }
}
