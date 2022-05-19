using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tile_Template_Editor))]
public class Tile_Editor : Editor
{
    Tile_Template_Editor tile_editor;
    Editor tile_editor_save;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawSettingsEditor(tile_editor.tile, tile_editor.On_Settings_Update, ref tile_editor.tile_settings_foldout, ref tile_editor_save);
        if (GUILayout.Button("New Tile Template"))
        {
            tile_editor.tile = new Tile_Template();
        }
        if (GUILayout.Button("Save Tile Template") && tile_editor.filename != null) {
            tile_editor.Save_Tile();
        }
        if (GUILayout.Button("Update Access") && tile_editor.filename != null) {
            tile_editor.Update_Access();
        }
        if (GUILayout.Button("Generate Rotations") && tile_editor.filename != null) {
            tile_editor.Gen_Rotations();
        }
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if (settings != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using (var check = new EditorGUI.ChangeCheckScope())
            {        
                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

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
        tile_editor = (Tile_Template_Editor)target;
    }
}
