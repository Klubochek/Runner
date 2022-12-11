using System.Collections.Generic;
using UnityEngine;

public class PoolCreator : MonoBehaviour
{
    private int poolCapacity = 20;
    [SerializeField] private List<GameObject> tilesPrefabs;
    public List<GameObject> Pool;
    void Start()
    {
        for (int i = 0; i < poolCapacity; i++)
        {
            Pool.Add(Instantiate(tilesPrefabs[Random.Range(0, tilesPrefabs.Count)], Vector3.zero, Quaternion.Euler(new Vector3(0, -90, 0)), transform));
            Pool[i].SetActive(false);
        }
        RoadGenerator.Instance.ResetLevel();
    }

}
