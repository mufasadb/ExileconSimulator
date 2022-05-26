using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staffCreation : MonoBehaviour
{
    public GameObject staffCollection;
    public GameObject MainCamera;
    [SerializeField] private Transform[] spawnTransforms;
    private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    public List<StaffMember> staffList = new List<StaffMember>();
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var trans in spawnTransforms)
        {
            spawnPoints.Add(new SpawnPoint(trans));
        }
        DoStaffGen();
    }
    private void DoStaffGen()
    {
        GameObject staffPrefab = PrefabHolder.instance.StaffPrefab;
        Canvas canvas = staffPrefab.GetComponentInChildren<Canvas>();
        canvas.worldCamera = MainCamera.GetComponent<Camera>();
        FaceCamera faceCamera = staffPrefab.GetComponentInChildren<FaceCamera>();
        faceCamera.cameraPos = MainCamera.transform;
        for (int i = 0; i < 5; i++)
        {
            staffList.Add(StaffMember.CreateInstance(1));
            Transform newPositionTrans = getSpawnPoint();
            Vector3 position = new Vector3(newPositionTrans.position.x, newPositionTrans.position.y, newPositionTrans.position.z);
            var staff = staffPrefab.GetComponent<staffDetails>();
            // var card = cardPrefab.GetComponent<CardDisplay>();
            // card.card = hand[i];
            staff.staffData = staffList[i];

            Instantiate(staffPrefab, position, Quaternion.identity, staffCollection.transform);
        }

    }
    private void positionStaffMember() { }
    private Transform getSpawnPoint()
    {
        List<SpawnPoint> availableSpawnPoints = spawnPoints.FindAll(spawn => spawn.used == false);
        int choice = Random.Range(0, availableSpawnPoints.Count);
        availableSpawnPoints[choice].used = true;
        return availableSpawnPoints[choice].transformPoint;
    }
    // Update is called once per frame
}
