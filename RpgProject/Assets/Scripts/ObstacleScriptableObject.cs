using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class obstacleinfo
{
    public bool[] y = new bool[10];
}



[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/ObstacleData", order = 1)]
public class ObstacleScriptableObject : ScriptableObject
{
    public obstacleinfo[] obstacleData = new obstacleinfo[10];
}
