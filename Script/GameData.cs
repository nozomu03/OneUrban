using UnityEngine;
using UnityEditor;

[System.Serializable]
public class GameData
{
    public int now_sceneIndex = 0;
    public float player_x = 0.0f;
    public float player_y = 0.0f;
    public float player_z = 0.0f;
    public GameData(int now_sceneIndex, float player_x, float player_y, float player_z)
    {
        this.now_sceneIndex = now_sceneIndex;
        this.player_x = player_x;
        this.player_y = player_y;
        this.player_z = player_z;
    }

    public override string ToString()
    {
        return now_sceneIndex + ":" + player_x + ":" + player_y + ":" + player_z;
    }
}