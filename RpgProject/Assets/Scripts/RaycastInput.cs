using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastInput : MonoBehaviour
{
    public TMP_Text coordinatesText;
    public GameObject mouseIndicator;
    public GameObject pathindicator;
    public Transform pathindicatorParent;
    public ObstacleScriptableObject obstaclinfo;
    Pathfinding pathfinder;
    TileInfo lastTile;
    public List<Vector2> path;
    private void Awake()
    {
        pathfinder = GetComponent<Pathfinding>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            TileInfo tileinfo = hit.collider.gameObject.GetComponentInParent<TileInfo>();
            if (hit.collider.gameObject.GetComponentInParent<TileInfo>() != lastTile)
            {  
                lastTile = hit.collider.gameObject.GetComponentInParent<TileInfo>();
                coordinatesText.text = (tileinfo.gridPosX) + "," + (tileinfo.gridPosY);
                if (!obstaclinfo.obstacleData[tileinfo.gridPosX].y[tileinfo.gridPosY])
                {
                    path = pathfinder.GetPath(new Vector2(transform.position.x,transform.position.y), new Vector2(tileinfo.gridPosX,tileinfo.gridPosY));
                    Highlightpath(path);
                }
                else
                {
                    ClearPath();
                }
            }

            if(Input.GetMouseButtonDown(0) && hit.collider.gameObject.GetComponentInParent<TileInfo>())
            {
                Debug.Log("GetPath on click");
                if (!obstaclinfo.obstacleData[tileinfo.gridPosX].y[tileinfo.gridPosY])
                {
                    List<Vector2> path = pathfinder.GetPath(new Vector2(transform.position.x, transform.position.y), new Vector2(tileinfo.gridPosX, tileinfo.gridPosY));
                    GetComponent<PlayerMovement>().SetPath(path);
                }
            }

        }
        else
        {
            ClearPath();
        }

    }

    public void Highlightpath(List<Vector2> path)
    {
        ClearPath();
        for (int i = 1; i < path.Count - 1; i++)
        {
            Instantiate(pathindicator, new Vector3(path[i].x, 0f, path[i].y), Quaternion.identity, pathindicatorParent);
        }
        Instantiate(mouseIndicator, new Vector3(path[0].x, 0f, path[0].y), Quaternion.identity, pathindicatorParent);
    }

    public void ClearPath()
    {
        foreach (Transform child in pathindicatorParent)
        {
            Destroy(child.gameObject);
        }
    }
}
