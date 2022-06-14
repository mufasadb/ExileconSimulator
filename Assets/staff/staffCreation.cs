using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staffCreation : MonoBehaviour
{
    public GameObject staffCollection;
    // public GameObject MainCamera;
    [SerializeField] private GameObject[] spawnObjects;
    // public SpawnPoint spawners;
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
        SpawnPoint zombieSpawner = spawnPoints.Find(spawn => spawn.specifiedFor == "Zombie");
        zombieSpawner.used = true;
        GameObject staffPrefab = PrefabHolder.instance.StaffPrefab;
        createStaffMember(staffPrefab, "Zombie", zombieSpawner);
        for (int i = 0; i < 10; i++)
        {
            createStaffMember(staffPrefab);
        }

    }
    private void createStaffMember(GameObject staffPrefab)
    {
        StaffMember staffMember = StaffMember.CreateInstance(1);
        staffList.Add(staffMember);
        staffList.Add(StaffMember.CreateInstance(1));
        SpawnPoint newPositionObj = getSpawnPoint();
        Transform newPositionTrans = newPositionObj.transformPoint.transform;
        Vector3 position = new Vector3(newPositionTrans.position.x, newPositionTrans.position.y, newPositionTrans.position.z);
        var staff = staffPrefab.GetComponent<staffDetails>();
        staff.staffData = staffMember;
        GameObject staffMem = Instantiate(staffPrefab, position, Quaternion.identity, staffCollection.transform);
        staffMem.transform.LookAt(newPositionObj.direction, Vector3.up);
        nerdSpawner.DoNerdGen(position, newPositionObj.direction.localPosition, staff.staffData.staffQueueSize, staffMem.transform);
    }
    private void createStaffMember(GameObject staffPrefab, string chosenStaffMember, SpawnPoint spawnPoint)
    {
        StaffMember staffMember = StaffMember.CreateSpecific(chosenStaffMember);
        staffList.Add(staffMember);
        staffList.Add(StaffMember.CreateInstance(1));
        // SpawnPoint newPositionObj = getSpawnPoint();
        SpawnPoint newPositionObj = spawnPoint;
        Transform newPositionTrans = newPositionObj.transformPoint.transform;
        Vector3 position = new Vector3(newPositionTrans.position.x, newPositionTrans.position.y, newPositionTrans.position.z);
        var staff = staffPrefab.GetComponent<staffDetails>();
        staff.staffData = staffMember;
        GameObject staffMem = Instantiate(staffPrefab, position, Quaternion.identity, staffCollection.transform);
        staffMem.transform.LookAt(newPositionObj.direction, Vector3.up);
        nerdSpawner.DoNerdGen(position, newPositionObj.direction.localPosition, staff.staffData.staffQueueSize, staffMem.transform);
    }
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
