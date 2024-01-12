using System.Collections.Generic;
using UnityEngine;

public class Stinker : MonoBehaviour
{
    [SerializeField]
    private int level;
    [SerializeField]
    private float stinkPercentage;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int maxValue;

    [SerializeField]
    private List<GameObject> wayPoints;
    [SerializeField]
    private int wayPointIndex = 1;

    public MeshRenderer meshRenderer;

    private List<Color> stinkersColor = new List<Color>() { Color.green, Color.yellow, Color.red, Color.black };
    private Gradient gradient = new Gradient();

    void Start()
    {
        var colors = new GradientColorKey[4];
        colors[0] = new GradientColorKey(stinkersColor[0], 0.05f);
        colors[1] = new GradientColorKey(stinkersColor[1], 0.1f);
        colors[2] = new GradientColorKey(stinkersColor[2], 0.3f);
        colors[3] = new GradientColorKey(stinkersColor[3], 0.5f);

        var alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(0.0f, 0.0f);
        alphas[1] = new GradientAlphaKey(1.0f, 0.05f);

        gradient.SetKeys(colors, alphas);
        meshRenderer.material.SetColor("_Color", gradient.Evaluate(stinkPercentage / 100));
    }

    void Update()
    {
        if(!(wayPointIndex == wayPoints.Count - 1 && Vector3.Distance(transform.position, wayPoints[wayPoints.Count - 1].transform.position) <= 0.1))
        {
            Vector3 distance = wayPoints[wayPointIndex].transform.position;
            transform.position = Vector3.MoveTowards(transform.position, distance, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, distance) <= 0.01 && wayPointIndex < wayPoints.Count - 1)
            {
                wayPointIndex++;
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    UpdateStinkPercentage(1);
        //}
    }

    public bool IsClean()
    {
        return stinkPercentage <= 0;
    }
    public void UpdateStinkPercentage(float reduce)
    {
        stinkPercentage -= reduce;
        meshRenderer.material.SetColor("_Color", gradient.Evaluate(stinkPercentage/100));
    }

    public void SetWayPoints(List<GameObject> wayPointsList)
    {
        wayPoints =wayPointsList;
    }
}
