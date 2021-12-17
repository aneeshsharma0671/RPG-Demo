using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
public class ObstacleGenerator : EditorWindow
{

    bool[,] obstacle = new bool[10,10];
    private ObstacleScriptableObject obstacleinfo;

    [MenuItem("Window/Obstacle Generator")]

    public static void ShowWindow()
    {
        GetWindow<ObstacleGenerator>("Obstacle Generator");
    }

    private void OnEnable()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                obstacle[i, j] = false;
            }
        }
    }

    private void OnGUI()
    {
        // window Code

        obstacleinfo = (ObstacleScriptableObject)EditorGUILayout.ObjectField(obstacleinfo, typeof(ObstacleScriptableObject), true);
        for (int i = 0; i < 10; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < 10; j++)
            {
                obstacle[i,j] = GUILayout.Toggle(obstacle[i,j],"");
            }
            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Generate Obstacle data"))
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++) 
                {
                    obstacleinfo.obstacleData[i].y[j] = obstacle[i,j];
                }
            }
            
        }
        if (GUILayout.Button("Debug"))
        {

            Debug.Log(obstacleinfo.obstacleData[0]);
        }

    }

    private void OnInspectorUpdate()
    {

    }


}
#endif