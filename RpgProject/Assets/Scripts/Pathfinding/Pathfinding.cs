using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Vector2 StartPos;
    public Vector2 TargetPos;
    public ObstacleScriptableObject obstacleinfo;
    public List<Vector2> Path;
    public GameObject pathindicator;
    public Transform pathParent;
    Vector2[] dir = {new Vector2(0,1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0)};

    public void FindPath()
    {
        if(Path.Count > 2 && Path[Path.Count-1] == TargetPos)
        {
            return;
        }
        Path.Clear();
        Vector2 currentnode = StartPos;
        Path.Add(StartPos);
        Path.Add(StartPos);
        for (int i = 0; i < 100; i++)
        { 
            if (currentnode == TargetPos)
            {
                Debug.Log("Path Found");
                return;
            }
      
            currentnode = CheckNeighbor(currentnode);
        }

    }

    public void Highlightpath()
    {
        ClearPath();
        for (int i = 0; i < Path.Count-1; i++)
        {
            Instantiate(pathindicator, new Vector3(Path[i].x, 0f, Path[i].y), Quaternion.identity, pathParent);
        }

    }

    public void ClearPath()
    {
        foreach (Transform child in pathParent)
        {
            Destroy(child.gameObject);
        }
    }

    Vector2 CheckNeighbor(Vector2 currentnode)
    {
        float mindis = 200f;
        Vector2 nextnode = currentnode;

        for (int i = 0; i < 4; i++)
        {
            Vector2 nodeToCheck = new Vector2(currentnode.x + dir[i].x, currentnode.y + dir[i].y);
            if (Vector2.Distance(nodeToCheck, TargetPos) <= mindis && nodeToCheck.x >= 0 && nodeToCheck.y >= 0 && nodeToCheck.x < 10 && nodeToCheck.y < 10 && nodeToCheck != Path[Path.Count-2])
            {
                if (!checkObstacle(nodeToCheck))
                {
                    mindis = Vector2.Distance(nodeToCheck, TargetPos);
                    nextnode = nodeToCheck;
                }
            }
        }

        Path.Add(nextnode);
        return nextnode;
    }

    bool checkObstacle(Vector2 checknode)
    {
        if(obstacleinfo.obstacleData[(int)checknode.x].y[(int)checknode.y])
            return true;
        else
        {
            return false;
        }
    }
}
