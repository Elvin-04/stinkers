using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool selected;
    private Animator animator;

    public Transform visual;

    public bool isEmpty;

    public TurretType connectedTurretType;
    private GameObject connectedTurret;

    private void Start()
    {
        isEmpty = true;
        animator = GetComponent<Animator>();
    }

    public GameObject GetConnectedTurret() { return connectedTurret; }

    public void ChangeSelection(bool _selected)
    {
        selected = _selected;
        animator.SetBool("Selected", selected);
    }

    public void PlaceTurret(GameObject turret, TurretType type, float angle = 0f)
    {
        if(WashCoinsManager.instance.GetWashCoins() >= turret.GetComponent<Turret>().buyPrice)
        {
            isEmpty = false;
            connectedTurret = Instantiate(turret, transform.position + new Vector3(0, 0.65f, 0), Quaternion.identity);
            connectedTurret.transform.eulerAngles = new Vector3(0, angle, 0);
            connectedTurretType = type;
        }
    }

    public void DeleteTurret()
    {
        WashCoinsManager.instance.AddWashCoins((int)connectedTurret.GetComponent<Turret>().sellPrice);
        Destroy(connectedTurret);
        isEmpty = true;
        connectedTurretType = TurretType.NONE;

    }

    public GameObject GetTurretGo()
    {
        return connectedTurret;
    }
}
