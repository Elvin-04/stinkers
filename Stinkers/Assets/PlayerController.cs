using System.ComponentModel.Design.Serialization;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 mousePosition;
    private Camera cam;

    Tile lastTile;

    [Header("Previews")]
    [SerializeField] private GameObject waterGunPreview;
    [SerializeField] private GameObject showerPreview;
    [SerializeField] private GameObject deodorantGunPreview;
    private float angle = 0.0f;
    [SerializeField] private GameObject range;

    [Header("Prefabs")]
    [SerializeField] private GameObject waterGun;
    [SerializeField] private GameObject showe;
    [SerializeField] private GameObject deodorantGun;

    [SerializeField] private TurretType turretSelected;

    private void Start()
    {
        DisableAllPreviews();

        mousePosition = Vector2.zero;
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        Ray ray = cam.ScreenPointToRay(mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Tile tile = hitInfo.transform.GetComponent<Tile>();
            if(tile != null && tile.isEmpty)
            {
                if (lastTile != null)
                    lastTile.ChangeSelection(false);

                tile.ChangeSelection(true);
                lastTile = tile;

                if (turretSelected != TurretType.NONE)
                {
                    switch (turretSelected)
                    {
                        case TurretType.WATERGUN:
                            SetPreview(waterGunPreview); 
                            break;
                        case TurretType.DEODORANT:
                            SetPreview(deodorantGunPreview);
                            break;
                    }
                }
            }
            else if(tile != null && !tile.isEmpty)
            {
                DisableAllPreviews();
            }
            else if(lastTile!= null && tile != null && !tile.isEmpty)
            {
                lastTile.ChangeSelection(false);
                lastTile = null;
            }
        }
        else
        {
            if (lastTile != null)
            {
                lastTile.ChangeSelection(false);
                lastTile = null;
                DisableAllPreviews();
            }
                
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        mousePosition = ctx.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && lastTile != null && turretSelected != TurretType.NONE && lastTile.isEmpty)
        {
            switch (turretSelected)
            {
                case TurretType.WATERGUN:
                    lastTile.PlaceTurret(waterGun, turretSelected);
                    break;
                case TurretType.DEODORANT:
                    lastTile.PlaceTurret(deodorantGun, turretSelected, angle);
                    break;
            }

            DisableAllPreviews();
            lastTile.ChangeSelection(false);
            lastTile = null;
            turretSelected = TurretType.NONE;
        }
        else if(ctx.performed && lastTile == null)
        {
            turretSelected = TurretType.NONE;
        }
    }

    public void OnScroll(InputAction.CallbackContext ctx)
    {
        float scroll = ctx.ReadValue<float>();
        if(turretSelected == TurretType.DEODORANT)
        {
            if(scroll > 0)
            {
                angle += 10f;
            }
            else if (scroll < 0)
            {
                angle -= 10f;
            }
            deodorantGunPreview.transform.eulerAngles = new Vector3(0, angle, 0);
        }
    }

    public void SelectTurret(int turretIndexType)
    {
        switch (turretIndexType)
        {
            case 0:
                turretSelected = TurretType.NONE;
                break;
            case 1:
                turretSelected = TurretType.WATERGUN;
                break;
            case 2:
                turretSelected = TurretType.SHOWER;
                break;
            case 3:
                turretSelected = TurretType.DEODORANT;
                deodorantGunPreview.transform.eulerAngles = Vector3.zero;
                angle = 0f;
                break;

        }
    }

    private void SetPreview(GameObject turretPreview)
    {
        turretPreview.SetActive(true);
        
        turretPreview.transform.position = lastTile.transform.position + new Vector3(0, 0.65f, 0);
        

        if(turretSelected == TurretType.WATERGUN)
        {
            range.SetActive(true);
            range.transform.position = turretPreview.transform.position + new Vector3(0, -0.3f, 0);
            float rangeValue = 0f;
            rangeValue = waterGun.GetComponent<WaterGun>().range;
            range.transform.localScale = new Vector3(rangeValue * 2f, 0.1f, rangeValue * 2f);
        }
        
        
    }

    private void DisableAllPreviews()
    {
        range.SetActive(false);
        waterGunPreview.SetActive(false);
        //showerPreview.SetActive(false);
        deodorantGunPreview.SetActive(false);
    }
}



public enum TurretType
{
    NONE,
    WATERGUN,
    SHOWER,
    DEODORANT,
}