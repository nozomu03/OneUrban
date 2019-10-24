using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndLoad : MonoBehaviour
{
    // Start is called before the first frame update   
    private string path;    
    public void Init()
    {
        path = Application.persistentDataPath + "/save.dat";
    }

    public void Save()
    {                
        GameData gameData = new GameData(GlobalData.scene_index, GlobalData.now_position);
        real_Save(gameData);
    }

    private void real_Save(GameData gameData)
    {
        BinaryFormatter format = new BinaryFormatter();
        FileStream save = File.Create(path);
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
            GlobalData.loaded_data = new GameData(0, new Vector3(0, 0, 0));
        }
    }
}