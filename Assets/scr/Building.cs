using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool isnotCol;
    public bool RearrangeNow = false;

    private SpriteRenderer mySprite;

    private void Start()
    {
        isnotCol = true;
        TryGetComponent(out mySprite);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isnotCol = true;
        mySprite.color = Color.white;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isnotCol = false;
        mySprite.color = Color.magenta;
    }

    public void BuildingMove()
    {
        StartCoroutine(BuildingSystem.build_system.FollowMouse(this.gameObject,2));
    }

}

