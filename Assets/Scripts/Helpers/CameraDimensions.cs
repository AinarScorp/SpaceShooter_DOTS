using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraDimensions : MonoBehaviour
{
    [SerializeField] bool drawGizmos;
    [SerializeField] Color gizmosColor = Color.green;
    [SerializeField] Transform player;
    [SerializeField] float2 mapDimensions = new float2(10, 10);
    void Awake()
    {
        player = this.transform;
    }

    void OnDrawGizmos()
    {
        if (!drawGizmos || !player) return;
        Gizmos.color = gizmosColor;
        //Calc rightTopCorner
        Vector3 rightTopCorner = player.transform.position;
        rightTopCorner.x += mapDimensions.x;
        rightTopCorner.z += mapDimensions.y;
        
        //Calc leftTopCorner
        Vector3 leftTopCorner = rightTopCorner;
        leftTopCorner.x -= mapDimensions.x * 2;
        
        //Calc rightBottomCorner
        Vector3 rightBottomCorner = rightTopCorner;
        rightBottomCorner.z -= mapDimensions.y * 2;
        
        //Calc leftBottomCorner
        Vector3 leftBottomCorner = leftTopCorner;
        leftBottomCorner.z -= mapDimensions.y * 2;
        
        
        Gizmos.DrawLine(rightTopCorner, leftTopCorner);
        Gizmos.DrawLine(rightTopCorner, rightBottomCorner);
        Gizmos.DrawLine(leftTopCorner, leftBottomCorner);
        Gizmos.DrawLine(rightBottomCorner, leftBottomCorner);

    }
}
