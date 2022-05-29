using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] public Transform cameraPos;
    // Start is called before the first frame update
    void Start()
    {
        // Transform cameraPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraPos);
    }
}
