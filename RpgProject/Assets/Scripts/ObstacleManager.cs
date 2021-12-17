using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleScriptableObject ObstacleData;

    public GameObject obstacle;

    private void Start()
    {
        Debug.Log(ObstacleData.obstacleData[0]);
        for (int i = 0; i < 10; i++)
        { 
            for (int j = 0; j < 10; j++)
            {
                if (ObstacleData.obstacleData[i].y[j] == true)
                {
                    Instantiate(obstacle, new Vector3(i, 0f, j), Quaternion.identity);
                }
            }
        }
    }

}
