using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StinkersSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject greenStinkerPrefab;
    [SerializeField]
    private GameObject yellowStinkerPrefab;
    [SerializeField]
    private GameObject orangeStinkerPrefab;
    [SerializeField]
    private GameObject redStinkerPrefab;
    [SerializeField]
    private GameObject blackStinkerPrefab;
    private List<GameObject> stinkersPrefabs ;

    [SerializeField]
    private List<GameObject> greenWay;
    [SerializeField]
    private List<GameObject> yellowWay;
    [SerializeField]
    private List<GameObject> orangeWay;
    [SerializeField]
    private List<GameObject> redWay;
    [SerializeField]
    private List<GameObject> blackWay;
    private List<List<GameObject>> waysList;


    private int levelNumber;
    private int waveNumber;
    private float waveTimer;

    private bool isNextWave;
    private bool isSkipWave;

    [SerializeField]
    private List <Level> levels;

    [System.Serializable]
    public struct Level
    {
        public List<Wave> waves;
    }

    [System.Serializable]
    public struct Wave
    {
        public List<Way> ways;
        public float startTimer;
        public int endSpawnWay;
    }

    [System.Serializable]
    public struct Way
    {
        public List<_Stinker> stinkers;
        public int stinkerNumber;
    }

    [System.Serializable]
    public struct _Stinker
    {
        public int stinker;
        public float stinkerSpawnDelay;
    }

    void Start()
    {
        isNextWave = false;
        isSkipWave = false;
        stinkersPrefabs = new List<GameObject>() { greenStinkerPrefab, yellowStinkerPrefab, orangeStinkerPrefab, redStinkerPrefab, blackStinkerPrefab };
        waysList = new List<List<GameObject>>() { greenWay, yellowWay, orangeWay, redWay, blackWay };
        StartSpawnWave();
    }

    private void Update()
    {
        print(waveNumber);
        if(Time.time - waveTimer >= levels[levelNumber].waves[waveNumber].startTimer && isNextWave || isSkipWave)
        {
            print("c bon");
            isNextWave = false;
            isSkipWave = false;
            StartSpawnWave();
        }
    }

    public IEnumerator SpawnStinker(int way)
    {
        yield return new WaitForSeconds(levels[levelNumber].waves[waveNumber].ways[way].stinkers[levels[levelNumber].waves[waveNumber].ways[way].stinkerNumber].stinkerSpawnDelay);
        int stinkerType = levels[levelNumber].waves[waveNumber].ways[way].stinkers[levels[levelNumber].waves[waveNumber].ways[way].stinkerNumber].stinker-1;
        GameObject instantiate = Instantiate(stinkersPrefabs[stinkerType], waysList[way] [0].transform.position, Quaternion.identity);
        instantiate.GetComponent<Stinker>().SetWayPoints(waysList[way]);
        if (!(levels[levelNumber].waves.Count-1 == waveNumber && levels[levelNumber].waves[levels[levelNumber].waves.Count-1].ways[way].stinkers.Count-1 == levels[levelNumber].waves[waveNumber].ways[way].stinkerNumber))
        {
            Debug.Log("1er =" + (levels[levelNumber].waves[waveNumber].ways[way].stinkers.Count - 1));
            Debug.Log("2eme =" + levels[levelNumber].waves[waveNumber].ways[way].stinkerNumber);
            if (levels[levelNumber].waves[waveNumber].ways[way].stinkers.Count - 1 == levels[levelNumber].waves[waveNumber].ways[way].stinkerNumber)
            {
                print("aboubou");
                Wave ab = levels[levelNumber].waves[waveNumber];
                ab.endSpawnWay = levels[levelNumber].waves[waveNumber].endSpawnWay + 1;
                levels[levelNumber].waves[waveNumber] = ab;
                Way a = levels[levelNumber].waves[waveNumber].ways[way];
                a.stinkerNumber = 0;
                levels[levelNumber].waves[waveNumber].ways[way] = a;
                if (levels[levelNumber].waves[waveNumber].endSpawnWay == levels[levelNumber].waves[waveNumber].ways.Count)
                {
                    print("nice");
                    waveTimer = Time.time;
                    Wave abc = levels[levelNumber].waves[waveNumber];
                    abc.endSpawnWay = 0;
                    levels[levelNumber].waves[waveNumber] = abc;
                    isNextWave = true;
                    waveNumber++;
                }
            }
            else
            {
                Way a = levels[levelNumber].waves[waveNumber].ways[way];
                a.stinkerNumber = levels[levelNumber].waves[waveNumber].ways[way].stinkerNumber + 1;
                levels[levelNumber].waves[waveNumber].ways[way] = a;
                StartSpawnStinker(way);
            }
        }
    }

    public void StartSpawnStinker(int way)
    {
            StartCoroutine(SpawnStinker(way));
    }

    public void StartSpawnWave()
    {
        for (int way = 0; way < levels[levelNumber].waves[waveNumber].ways.Count; way++)
        {
            StartSpawnStinker(way);
        }
    }

    public void skipWave()
    {
        isSkipWave = true;
        //addWC((int)(levels[levelNumber].waves[waveNumber].startTimer - (Time.time - waveTimer))+1);
    }
}