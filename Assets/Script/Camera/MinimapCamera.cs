using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] private GameObject target;     // 카메라 대상
    //[SerializeField] private float offsetX = 0;  // 카메라 x 좌표
    //[SerializeField] private float offsetY = 0;  // 카메라 y 좌표
    [SerializeField] private float offsetZ = -10;    // 카메라 z 좌표
    Vector3 cameraPosition; // 카메라 위치

    
    private void LateUpdate() 
    {
        //cameraPosition.x = target.transform.position.x + offsetX; 
        //cameraPosition.y = target.transform.position.y + offsetY; 
        cameraPosition.z = target.transform.position.z + offsetZ;   
        transform.position = cameraPosition;    // 카메라 좌표 = 목표 대상 위치
    }
}
