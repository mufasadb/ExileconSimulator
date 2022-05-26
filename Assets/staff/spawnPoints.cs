using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint{
    public Transform transformPoint;
    public bool used;
    public SpawnPoint(Transform trans){
        transformPoint = trans;
        used = false;
    }
}