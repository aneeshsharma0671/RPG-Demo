using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public int gridPosX;
    public int gridPosY;

    private void Awake()
    {
        gridPosX = (int)gameObject.transform.localPosition.x;
        gridPosY = (int)gameObject.transform.localPosition.z;
    }
}
