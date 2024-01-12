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

    // Start is called before the first frame update
    void Start()
    {
        GameObject instanciate = Instantiate(greenStinkerPrefab, greenWay[0].transform.position, Quaternion.identity);
        instanciate.GetComponent<Stinker>().setWayPoints(greenWay);
        GameObject instanciate2 = Instantiate(yellowStinkerPrefab, yellowWay[0].transform.position, Quaternion.identity);
        instanciate2.GetComponent<Stinker>().setWayPoints(yellowWay);
        GameObject instanciate3 = Instantiate(orangeStinkerPrefab, orangeWay[0].transform.position, Quaternion.identity);
        instanciate3.GetComponent<Stinker>().setWayPoints(orangeWay);
        GameObject instanciate4 = Instantiate(redStinkerPrefab, redWay[0].transform.position, Quaternion.identity);
        instanciate4.GetComponent<Stinker>().setWayPoints(redWay);
        GameObject instanciate5 = Instantiate(blackStinkerPrefab, blackWay[0].transform.position, Quaternion.identity);
        instanciate5.GetComponent<Stinker>().setWayPoints(blackWay);
    }
}
