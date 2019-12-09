using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RenamerTool : EditorWindow {

    string nameForItems;
    bool giveIndex;
    int index;

    [MenuItem( "Tools/Renamer", false, 1 )]

    public static void ShowWindow() {
        RenamerTool window = (RenamerTool)GetWindow( typeof( RenamerTool ) );
    }

    void OnGUI() {
        GUILayout.Label( "Enter the new Name for the Items. " , EditorStyles.boldLabel );
        nameForItems = EditorGUILayout.TextField( "Name" , nameForItems );
        giveIndex = EditorGUILayout.Toggle( "Give Index" , giveIndex );

        if(GUILayout.Button( "Rename" )) {
            Rename();
        }
    }

    private void Rename() {
        foreach(GameObject obj in Selection.gameObjects) {
            if(!giveIndex)
                obj.name = nameForItems;
            else {
                obj.name = nameForItems + " (" + index + ")";
                obj.transform.SetSiblingIndex( index );
                index++;
            }
        }
        index = 0;
    }
}


//CR: Thijmen Hermans 2GD2
