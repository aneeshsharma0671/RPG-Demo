using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastManager : MonoBehaviour
{
    public TMP_Text coordinatesText;
    public GameObject indicator;
    

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                TileInfo tileinfo = hit.collider.gameObject.GetComponentInParent<TileInfo>();
                //Debug.Log(tileinfo.gridPosX + "," + tileinfo.gridPosY);
                coordinatesText.text = (tileinfo.gridPosX+1) + "," + (tileinfo.gridPosY+1);
                indicator.SetActive(true);
                indicator.transform.position = new Vector3(tileinfo.gridPosX + 0.5f, 1.02f ,tileinfo.gridPosY + 0.5f);
            }
            else
            {
                indicator.SetActive(false);
            }

    }
}
