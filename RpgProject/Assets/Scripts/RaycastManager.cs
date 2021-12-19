using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastManager : MonoBehaviour
{
    public TMP_Text coordinatesText;
    public GameObject indicator;
    public ObstacleScriptableObject obstaclinfo;
    Pathfinding pathfinder;

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
            //Debug.Log(tileinfo.gridPosX + "," + tileinfo.gridPosY);
            coordinatesText.text = (tileinfo.gridPosX) + "," + (tileinfo.gridPosY);
            if(!obstaclinfo.obstacleData[tileinfo.gridPosX].y[tileinfo.gridPosY])
            {
                SetTileIndicators(new Vector2(tileinfo.gridPosX,tileinfo.gridPosY));
            }
            else
            {
                indicator.SetActive(false);
                pathfinder.ClearPath();
            }
        }
        else
        {
            indicator.SetActive(false);
            pathfinder.ClearPath();
        }

    }

    void SetTileIndicators(Vector2 tilepos)
    {
        indicator.SetActive(true);
        indicator.transform.position = new Vector3(tilepos.x + 0.5f, 1.02f, tilepos.y + 0.5f);
        if (pathfinder.TargetPos == new Vector2(tilepos.x, tilepos.y))
        {
            pathfinder.FindPath();
            pathfinder.Highlightpath();
        }
        pathfinder.TargetPos = new Vector2(tilepos.x, tilepos.y);
    }    
}
