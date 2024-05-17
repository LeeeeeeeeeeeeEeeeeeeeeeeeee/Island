using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Building_basicSCR
{
    private void Start()
    {
        isnotCol = true;
        TryGetComponent(out mySprite);
        TryGetComponent(out myCollider);
        WhoAmI(this.name);
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
        StartCoroutine(ArchitectureSystem.build_system.FollowMouse(this.gameObject,2));
    }
    
}

