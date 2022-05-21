using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public static class Util {
    public static bool V3I_Equals(Vector3Int a, Vector3Int b) {
        if (a.x == b.x && a.y == b.y && a.z == b.z) { return true; } else { return false; }
    }
}