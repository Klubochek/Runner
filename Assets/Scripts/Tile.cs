using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject[] firstObstacls;
    [SerializeField] private GameObject[] secondObstacls;
    [SerializeField] private GameObject[] thirdObstacls;
    [SerializeField] private GameObject[] coinsLine;
    [SerializeField] private GameObject[] allTileObject;


    private void OnEnable()
    {

        SetupFirstObstacls();
        SetupSecondObstacls();
        SetupThirdObstacls();
        SetupCoinsLine();
    }

    private void SetupCoinsLine()
    {
        for (int i = 0; i < coinsLine.Length; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                coinsLine[i].SetActive(true);
            }
        }
    }

    private void SetupThirdObstacls()
    {
        int activeObstacle = 0;
        for (int i = 0; i < thirdObstacls.Length; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                thirdObstacls[i].SetActive(true);
                activeObstacle++;
            }
        }
        if (activeObstacle == 3)
        {
            thirdObstacls[Random.Range(0, 3)].SetActive(false);
        }
    }

    private void SetupSecondObstacls()
    {
        int activeObstacle = 0;
        for (int i = 0; i < secondObstacls.Length; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                secondObstacls[i].SetActive(true);
                activeObstacle++;
            }
        }
        if (activeObstacle == 6)
        {
            secondObstacls[Random.Range(0, 6)].SetActive(false);
        }
    }

    private void SetupFirstObstacls()
    {
        int r = Random.Range(0, 2);
        firstObstacls[r].SetActive(true);
    }

    private void OnDisable()
    {
        foreach (GameObject go in allTileObject)
        {
            go.SetActive(false);
        }
    }
    public void DeactivateObstacle()
    {
        foreach(GameObject go in allTileObject)
        {
            go.SetActive(false);
        }
    }
}
