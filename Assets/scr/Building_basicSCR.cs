using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building_basicSCR : MonoBehaviour
{
    public BuildingSystem _System = BuildingSystem.build_system;
    public bool isnotCol;
    public bool RearrangeNow = false;

    //public SpriteRenderer mySprite;
    //public BoxCollider2D myCollider;

    public void WhoAmI(string name)
    {
        switch (name)
        {
            case "House":
                gameObject.AddComponent<House>();
                break;

            case "Cafe":
                break;

            case "Grocery":
                break;

            default:
                break;
        }

    }
    
}
