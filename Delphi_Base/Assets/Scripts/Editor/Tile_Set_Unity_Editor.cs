using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tile_Set_Editor))]
public class Tile_Set_Unity_Editor : Editor
{
    Tile_Set_Editor editor;
    Editor editor_save;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawSettingsEditor(editor.tileset, editor.On_Settings_Update, ref editor.settings_foldout, ref editor_save);
        if (GUILayout.Button("New Tile Set"))
        {
            editor.tileset = new Tile_Set();
        }
        if (GUILayout.Button("Save Tile Set") && editor.filename != null) {
            editor.tileset.Save(editor.filename);
        }
        if (GUILayout.Button("Load Tile Set") && editor.save != null) {
            editor.tileset = new Tile_Set(editor.save);
            editor.On_Settings_Update();
        }
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor_save)
    {
        if (settings != null)
        {
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
        editor = (Tile_Set_Editor)target;
    }
   
}
