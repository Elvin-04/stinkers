using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 mousePosition;
    private Camera cam;

    Tile lastTile;

    private void Start()
    {
        mousePosition = Vector2.zero;
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        Ray ray = cam.ScreenPointToRay(mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Tile tile = hitInfo.transform.GetComponent<Tile>();
            if(tile != null)
            {
                if (lastTile != null)
                    lastTile.ChangeSelection(false);

                tile.ChangeSelection(true);
                lastTile = tile;
            }
        }
        else
        {
            if (lastTile != null)
                lastTile.ChangeSelection(false);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        mousePosition = ctx.ReadValue<Vector2>();
    }
}
