using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform endLevel;

    private void Awake()
    {
        instance = this;
    }
}
