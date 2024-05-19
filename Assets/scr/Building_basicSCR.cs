using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building_basicSCR : MonoBehaviour
{
    public ArchitectureSystem _System = ArchitectureSystem.build_system;
    public bool isnotCol;
    public bool RearrangeNow = false;

    public SpriteRenderer mySprite;
    public BoxCollider2D myCollider;

    public void WhoAmI(string name)
    {
        switch (name)
        {
            case "빨간 설탕 유리 꽃":
                gameObject.AddComponent<Food_Generator>();
                break;

            case "Cafe":
                gameObject.AddComponent<Cafe>();
                break;

            case "Grocery":
                gameObject.AddComponent<Grocery>();
                break;
            case "딸기우유 연못":
                gameObject.AddComponent<Food_Generator>();
                break;

            default:
                break;
        }

        gameObject.GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().size;
        gameObject.GetComponent<Food_Generator>().init();
    }
    
}
