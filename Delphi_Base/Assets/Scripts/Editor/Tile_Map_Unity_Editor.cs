using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tile_Map_Editor))]
public class Tile_Map_Unity_Editor : Editor
{
    Tile_Map_Editor map_editor;
    Editor map_editor_save;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        DrawSettingsEditor(map_editor.map, map_editor.On_Update, ref map_editor.settings_foldout, ref map_editor_save);
        if (GUILayout.Button("Initialise"))
        {
            map_editor.Initialise();
        }
        if (GUILayout.Button("Save Tile Data"))
        {
            map_editor.Save();
        }
        if (GUILayout.Button("Load Tile Data"))
        {
            map_editor.Load();
        }
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor_save) {
        if (settings != null) {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor_save);
                    editor_save.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdated != null)
                        {
                            onSettingsUpdated();
                        }
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        map_editor = (Tile_Map_Editor)target;
    }
}
