using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building_basicSCR : MonoBehaviour
{
    public bool isnotCol;
    public bool RearrangeNow = false;

    public SpriteRenderer mySprite;
    public BoxCollider2D myCollider;

    public void WhoAmI(string name)
    {
        Debug.Log(name);

        switch (name)
        {
            case "빨간 설탕 유리 꽃":
                gameObject.AddComponent<Food_Generator>();
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.09f, -0.08f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.11f, 0.287f);
                break;

            case "노란 설탕 유리 꽃":
                gameObject.AddComponent<Food_Generator>();
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.09f, -0.08f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.11f, 0.287f);
                break;

            case "파란 설탕 유리 꽃":
                gameObject.AddComponent<Food_Generator>();
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.09f, -0.08f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.11f, 0.287f);
                break;

            case "비스킷 의자":
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.03f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.57f, 0.42f);
                break;

            case "아이스크림 테이블":
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.02f, -0.17f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.57f, 0.44f);
                break;

            case "마카롱 쿠션":
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.07f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.87f, 0.6f);
                break;

            case "2단 마카롱 쿠션":
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.06f, -0.02f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.87f, 0.6f);
                break;

            case "딸기우유 연못":
                gameObject.AddComponent<Food_Generator>();
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.16f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.26f, 0.89f);
                break;

            case "솜사탕 구름 1":
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.01f, -0.05f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(3.58f, 0.34f);
                break;

            case "솜사탕 구름 2":
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.02f, 0);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.89f, 0.19f);
                break;

            case "캔디 가로등":
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.28f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.38f, 0.65f);
                break;

            case "롤리팝 캔디 나무":
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.62f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.16f, 1.31f);
                break;

            case "마카롱 나무":
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.03f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.554f, 0.444f);
                break;

            case "초코 분수":
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.01f, 0.572f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.97f, 1.62f);
                break;

            case "녹차 푸딩 산":
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.133f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.6f, 1.26f);
                break;

            case "초코 푸딩 산":
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.03f, 0.01f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(2.18f, 1.17f);
                break;

            case "거대한 롤리팝 마시멜로우 언덕":
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.01f, -1);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(3.95f, 0.37f);
                break;

            default:
                break;
        }

        if (gameObject.TryGetComponent(out Food_Generator generator))
        {
            generator.init();
        }
    }
    
}
