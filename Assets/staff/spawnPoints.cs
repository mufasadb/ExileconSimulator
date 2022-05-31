using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint{
    public GameObject transformPoint;
    public Transform direction;
    public bool used;
    public SpawnPoint(GameObject obj){
        transformPoint = obj;
        used = false;
        // Debug.Log(transformPoint.GetComponent<SpawnDirection>().spawnDirection);
        direction = transformPoint.GetComponent<SpawnDirection>().spawnDirection;
    }
}