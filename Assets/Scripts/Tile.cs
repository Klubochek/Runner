using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    private const int SecondLine = 2;
    [SerializeField] private GameObject[] firstObstacls;
    [SerializeField] private GameObject[] secondObstacls;
    [SerializeField] private GameObject[] thirdObstacls;
    [SerializeField] private GameObject[] coinsLine;
    [SerializeField] private GameObject[] allTileObject;


    private void OnEnable()
    {
        SetupFirstObstacls();
        SetupObstacls(secondObstacls);
        SetupObstacls(thirdObstacls);
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

    private void SetupObstacls(GameObject[] obstals)
    {
        int activeObstacle = 0;
        for (int i = 0; i < obstals.Length; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                obstals[i].SetActive(true);
                activeObstacle++;
            }
        }
        if (activeObstacle == obstals.Length)
        {
            obstals[Random.Range(0, obstals.Length)].SetActive(false);
        }
    }

    private void SetupFirstObstacls()
    {
        int obstacl = Random.Range(0, 3);
        if (obstacl != SecondLine)
        {
            firstObstacls[obstacl].SetActive(true);
        }
    }

    private void OnDisable()
    {
        DeactivateObstacle();
    }
    public void DeactivateObstacle()
    {
        foreach (GameObject obstacl in allTileObject)
        {
            obstacl.SetActive(false);
        }
    }
}
