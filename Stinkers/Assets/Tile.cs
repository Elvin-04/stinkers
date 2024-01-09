using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool selected;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void ChangeSelection(bool _selected)
    {
        selected = _selected;
        animator.SetBool("Selected", selected);
    }
}
