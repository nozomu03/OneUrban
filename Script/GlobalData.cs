using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData
{
    public static bool can_walk = true;
    public static Vector3 temp_position = new Vector3(-232.1f, 0, -104.9f);
    public static Vector3 now_position = new Vector3(-232.1f, 0, -104.9f);
    public static int scene_index = 0;
    public static bool nvl_screen = true;
    public static GameData loaded_data = null;
    public static float rotate_spped = 3f;
    public static bool goSet = false;
    public static bool run_mod = false;
    public static List<bool> frag_check = new List<bool>() { false, false, false, false, false, false, false};
}
