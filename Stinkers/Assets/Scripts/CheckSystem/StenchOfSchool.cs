using System.Collections.Generic;
using UnityEngine;

public class StenchOfSchool : MonoBehaviour
{
    private float stenchOfSchool;

    public List<ParticleSystem> stench;

    private List<Color> stinkersColor = new List<Color>() { Color.green, Color.yellow, Color.red, Color.magenta };
    private Gradient gradient = new Gradient();

    void Start()
    {
        stenchOfSchool = 0;

        var colors = new GradientColorKey[4];
        colors[0] = new GradientColorKey(stinkersColor[0], 0.1f);
        colors[1] = new GradientColorKey(stinkersColor[1], 0.2f);
        colors[2] = new GradientColorKey(stinkersColor[2], 0.6f);
        colors[3] = new GradientColorKey(stinkersColor[3], 1.0f);

        var alphas = new GradientAlphaKey[3];
        alphas[0] = new GradientAlphaKey(0.0f, 0.0f);
        alphas[1] = new GradientAlphaKey(0.5f, 0.1f);
        alphas[2] = new GradientAlphaKey(0.5f, 1.0f);

        gradient.SetKeys(colors, alphas);
        foreach (var col in stench) 
        {
            col.startColor = gradient.Evaluate(stenchOfSchool / 100);
        }
    }

    public void UpdateStenchOfSchool(float add)
    {
        if(stenchOfSchool + add > 100)
        {
            stenchOfSchool = 100;
        }
        else
        {
            stenchOfSchool += add;
        }
        
        foreach (var col in stench)
        {
            col.startColor = gradient.Evaluate(stenchOfSchool / 100);
        }
    }

    public float GetStenchOfSchool() {  return stenchOfSchool; }
}