using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staffCreation : MonoBehaviour
{
    public GameObject staffCollection;
    public GameObject MainCamera;
    [SerializeField] private GameObject[] spawnObjects;
    private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    public List<StaffMember> staffList = new List<StaffMember>();
    private NerdSpawner nerdSpawner;
    // Start is called before the first frame update
    void Awake()
    {
        nerdSpawner = GetComponent<NerdSpawner>();
        foreach (var objs in spawnObjects)
        {
            spawnPoints.Add(new SpawnPoint(objs));
        }
        DoStaffGen();
    }
    private void DoStaffGen()
    {
        GameObject staffPrefab = PrefabHolder.instance.StaffPrefab;
        // Canvas canvas = staffPrefab.GetComponentInChildren<Canvas>();
        // canvas.worldCamera = MainCamera.GetComponent<Camera>();
        // FaceCamera faceCamera = staffPrefab.GetComponentInChildren<FaceCamera>();
        // faceCamera.cameraPos = MainCamera.transform;
        for (int i = 0; i < 5; i++)
        {
            staffList.Add(StaffMember.CreateInstance(1));
            SpawnPoint newPositionObj = getSpawnPoint();
            Transform newPositionTrans = newPositionObj.transformPoint.transform;
            Vector3 position = new Vector3(newPositionTrans.position.x, newPositionTrans.position.y, newPositionTrans.position.z);
            var staff = staffPrefab.GetComponent<staffDetails>();
            // var card = cardPrefab.GetComponent<CardDisplay>();
            // card.card = hand[i];
            staff.staffData = staffList[i];

            GameObject staffMem = Instantiate(staffPrefab, position, Quaternion.identity, staffCollection.transform);
            staffMem.transform.LookAt(newPositionObj.direction, Vector3.up);
            // Debug.Log("position" + position);
            // Debug.Log("newPositionObj.direction.localPosition" + newPositionObj.direction.localPosition);
            // Debug.Log("staffMem.transform " + staffMem.transform);
            // Debug.Log("qsize" + staff.staffData.staffQueueSize);
            nerdSpawner.DoNerdGen(position, newPositionObj.direction.localPosition, staff.staffData.staffQueueSize, staffMem.transform);

        }

    }
    private void positionStaffMember() { }
    private SpawnPoint getSpawnPoint()
    {
        if (spawnPoints.Count < 1) { Debug.LogError("There are no spawnpoints assigned"); }
        List<SpawnPoint> availableSpawnPoints = spawnPoints.FindAll(spawn => spawn.used == false);
        int choice = Random.Range(0, availableSpawnPoints.Count);
        availableSpawnPoints[choice].used = true;
        return availableSpawnPoints[choice];
    }
    // Update is called once per frame
}
