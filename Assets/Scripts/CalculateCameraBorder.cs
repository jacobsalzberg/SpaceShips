using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CalculateCameraBorder : MonoBehaviour
{
    public float CameraXMin;
    public float CameraXMax;
    public float CameraYMax;
    public float CameraYMin;
    public float padding = 0.5f;
    void Start()
    {
        
        //
        CalculateCameraBorders(padding);
    }
    void CalculateCameraBorders(float padding)
    {
        //
        // calculate at distance to cammera from this object
        // 
        float zDistansCammeraFromThisObject = transform.position.z - Camera.main.transform.position.z;
        // set point at left bottom corner
        Vector3 LeftMostPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDistansCammeraFromThisObject));
        // set point at right top corner
        Vector3 RightMostPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, zDistansCammeraFromThisObject));
        //
        // calculate botom left right top
        //
        CameraXMin = LeftMostPosition.x + padding;
        CameraYMin = LeftMostPosition.y + padding;
        CameraXMax = RightMostPosition.x - padding;
        CameraYMax = RightMostPosition.y - padding;
    }
}