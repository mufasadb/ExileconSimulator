using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerdSpawner : MonoBehaviour
{
    public int nerdCount = 100;
    [SerializeField] Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawningNerds());
    }

    private IEnumerator SpawningNerds()
    {
        for (int i = 0; i < nerdCount; i++)
        {
            Vector3 position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            // Vector3 position = spawnPoints[0].position;
            position += new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
            GameObject nerdPrefab = PrefabHolder.instance.NerdPrefab;
            GameObject newNerd = Instantiate(nerdPrefab, position, Quaternion.identity, transform);
            newNerd.name = "Nerd " + i;
            yield return new WaitForSeconds(3f);
        }
    }

}
