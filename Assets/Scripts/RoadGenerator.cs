using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> activeTiles;
    [SerializeField] private List<GameObject> tiles;
    [SerializeField] private PoolCreator poolCreator;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 150);
    public bool IsStarted { get; private set; }

    private int maxRoadTiles = 10;
    public int speed = 0;
    private int maxSpeed = 40;
    public static RoadGenerator Instance;

    private void Awake()
    {
        Instance = this;
        SwipeController.Instance.ClickEvent += StartLevel;
    }

    public void ResetLevel()
    {
        Player.Instance.RestartGame();
        IsStarted = false;
        speed = 0;
        for (int i = 0; i < maxRoadTiles; i++)
        {
            CreateRoad();
        }
    }
    public void StartLevel()
    {
        if (!IsStarted)
        {
            speed = maxSpeed;
            IsStarted = true;
            Player.Instance.PlayerRun();
        }
    }

    public void CreateRoad()
    {
        Vector3 pos = Vector3.zero;
        if (activeTiles.Count > 0)
        {

            pos = activeTiles[activeTiles.Count - 1].transform.position + offset;
        }
        int r = Random.Range(0, poolCreator.Pool.Count);
        GameObject disabledTile = poolCreator.Pool[r];
        activeTiles.Add(disabledTile);
        poolCreator.Pool.RemoveAt(r);
        activeTiles[activeTiles.Count - 1].SetActive(true);
        activeTiles[activeTiles.Count - 1].transform.position = pos;
        if (activeTiles.Count == 1)
        {
            activeTiles[0].GetComponent<Tile>().DeactivateObstacle();
        }

    }
    private void Update()
    {
        foreach (GameObject tile in activeTiles)
        {
            tile.transform.position += Vector3.back * Time.deltaTime * speed;
        }
    }
    public void ReinstallPrefab()
    {
        activeTiles[0].gameObject.SetActive(false);
        poolCreator.Pool.Add(activeTiles[0]);
        activeTiles.RemoveAt(0);

        CreateRoad();
    }
}
