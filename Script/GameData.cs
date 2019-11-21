using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int now_sceneIndex = 0;
    public float player_x = 0.0f;
    public float player_y = 0.0f;
    public float player_z = 0.0f;
    public float rotate_speed = 3.0f;
    public bool nvl_screen = true;
    public List<bool> frag_check = new List<bool>() { false, false, false, false, false, false, false };

    public GameData(int now_sceneIndex, float player_x, float player_y, float player_z, float rotate_speed, bool nvl_screen, List<bool> frag_check)
    {
        this.now_sceneIndex = now_sceneIndex;
        this.player_x = player_x;
        this.player_y = player_y;
        this.player_z = player_z;
        this.rotate_speed = rotate_speed;
        this.nvl_screen = nvl_screen;
        this.frag_check = frag_check;
    }

    public override string ToString()
    {
        return now_sceneIndex + ":" + player_x + ":" + player_y + ":" + player_z;
    }
}