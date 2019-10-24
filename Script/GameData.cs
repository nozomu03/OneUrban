using UnityEngine;
using UnityEditor;

[System.Serializable]
public class GameData
{
    public int now_sceneIndex = 0;
    public Vector3 now_location = new Vector3(0, 0, 0);

    public GameData(int now_sceneIndex, Vector3 now_location)
    {
        this.now_sceneIndex = now_sceneIndex;
        this.now_location = now_location;
    }
}