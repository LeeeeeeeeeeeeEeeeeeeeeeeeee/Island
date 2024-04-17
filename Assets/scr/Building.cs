using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool isnotCol;
    public bool RearrangeNow = false;

    private SpriteRenderer mySprite;
    private BoxCollider2D myCollider;
    private Coroutine coco = null;

    private void Start()
    {
        isnotCol = true;
        TryGetComponent(out mySprite);
        TryGetComponent(out myCollider);
        BuildingSystem.build_system.FollowStart += TurnStart;

        switch (this.name)
        {
            case "House":
                break;

            case "Cafe":
                gameObject.transform.localScale = new Vector2(0.5f, 0.5f);
                myCollider.size = new Vector2(3f, 3f);
                break;
            case "Grocery":
                gameObject.transform.localScale = new Vector2(0.5f, 0.5f);         
                myCollider.size = new Vector2(3f, 3f);
                break;
            default:
                break;
        }
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

    public void TurnStart()
    {
        if (coco == null)
        {
            coco = StartCoroutine(TurnMouseFunc());
        }
        else if (coco != null)
        {
            StopCoroutine(coco);
            coco = null;
        }
    }

    private IEnumerator TurnMouseFunc()
    {
        while (true)
        {

            Debug.Log("ds");
            if (Input.GetKey(KeyCode.Space) || Input.touchCount > 1) 
            { 
                if (mySprite.flipX == true)
                {
                    mySprite.flipX = false;
                }else if(mySprite.flipX == false)
                {
                    mySprite.flipX = true;
                }    
            }
            yield return null;  
        }
    }
}

