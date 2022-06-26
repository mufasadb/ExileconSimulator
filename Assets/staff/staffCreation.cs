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
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var objs in spawnObjects)
        {
            spawnPoints.Add(new SpawnPoint(objs));
        }
        DoStaffGen();
    }
    private void DoStaffGen()
    {
        GameObject staffPrefab = PrefabHolder.instance.StaffPrefab;

        SpecialStaffGen(staffPrefab);
        // for (int i = 0; i < 50; i++)
        // {
        //     createStaffMember(staffPrefab);
        // }
        foreach (var sm in StaffDataSystem.instance.staffDataSet.set)
        {
            createStaffMember(staffPrefab, sm.name);
        };
    }
    void SpecialStaffGen(GameObject staffPrefab)
    {
        //there is a specific spawner close left which is always there whatever name is in the 
        // "create staff member" section will specifically spawn that mob in the close left position
        SpawnPoint zombieSpawner = spawnPoints.Find(spawn => spawn.specifiedFor == "Zombie");
        zombieSpawner.used = true;
        createStaffMember(staffPrefab, "Zombie", zombieSpawner);

        zombieSpawner = spawnPoints.Find(spawn => spawn.specifiedFor == "Shaper");
        zombieSpawner.used = true;
        createStaffMember(staffPrefab, "The Shaper", zombieSpawner);
        zombieSpawner = spawnPoints.Find(spawn => spawn.specifiedFor == "Minotaur");
        zombieSpawner.used = true;
        createStaffMember(staffPrefab, "Guardian of The Minotaur", zombieSpawner);
        zombieSpawner = spawnPoints.Find(spawn => spawn.specifiedFor == "Phoenix");
        zombieSpawner.used = true;
        createStaffMember(staffPrefab, "Guardian of The Pheonix", zombieSpawner);
        zombieSpawner = spawnPoints.Find(spawn => spawn.specifiedFor == "Chimera");
        zombieSpawner.used = true;
        createStaffMember(staffPrefab, "Guardian of The Chimera", zombieSpawner);
        zombieSpawner = spawnPoints.Find(spawn => spawn.specifiedFor == "Hydra");
        zombieSpawner.used = true;
        createStaffMember(staffPrefab, "Guardian of The Hydra", zombieSpawner);

    }
    private void createStaffMember(GameObject staffPrefab, string staffName)
    {
        StaffMember staffMember = StaffMember.CreateSpecific(staffName);
        staffList.Add(staffMember);
        // staffList.Add(StaffMember.CreateInstance(1));
        SpawnPoint newPositionObj = getSpawnPoint();
        Transform newPositionTrans = newPositionObj.transformPoint.transform;
        Vector3 position = new Vector3(newPositionTrans.position.x, newPositionTrans.position.y, newPositionTrans.position.z);
        var staff = staffPrefab.GetComponent<staffDetails>();
        staff.staffData = staffMember;
        GameObject staffMem = Instantiate(staffPrefab, position, Quaternion.identity, staffCollection.transform);
        staffMem.name = staff.staffData.name;
        staffMem.transform.LookAt(newPositionObj.direction, Vector3.up);
        // nerdSpawner.DoNerdGen(position, newPositionObj.direction.localPosition, staff.staffData.staffQueueSize, staffMem.transform);
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
        staffMem.name = staff.staffData.name;
        staffMem.transform.LookAt(newPositionObj.direction, Vector3.up);
        // nerdSpawner.DoNerdGen(position, newPositionObj.direction.localPosition, staff.staffData.staffQueueSize, staffMem.transform);
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
