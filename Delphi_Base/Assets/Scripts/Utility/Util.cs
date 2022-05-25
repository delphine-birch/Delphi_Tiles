using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public static class Util {
    public static bool V3I_Equals(Vector3Int a, Vector3Int b) {
        if (a.x == b.x && a.y == b.y && a.z == b.z) { return true; } else { return false; }
    }
    public static bool V3_Equals(Vector3 a, Vector3 b) {
        if (a.x == b.x && a.y == b.y && a.z == b.z) { return true; } else { return false; }
    }
    public static Vector3Int V3_to_V3I(Vector3 v) {
        return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
    }
    public static Vector3 V3I_to_V3(Vector3Int v) {
        return new Vector3(v.x, v.y, v.z);
    }
    public static Vector3 V3_Round(Vector3 v) {
        return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    }
}