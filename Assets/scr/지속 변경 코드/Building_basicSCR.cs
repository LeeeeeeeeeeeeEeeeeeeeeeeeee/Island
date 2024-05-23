using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building_basicSCR : MonoBehaviour
{
    public ArchitectureSystem _System = ArchitectureSystem.build_system;
    public bool isnotCol;
    public bool RearrangeNow = false;

    public SpriteRenderer mySprite;
    public PolygonCollider2D myCollider;

    public void WhoAmI(string name)
    {
        switch (name)
        {
            case "빨간 설탕 유리 꽃":
                gameObject.AddComponent<Food_Generator>();
                break;

            case "노란 설탕 유리 꽃":
                gameObject.AddComponent<Food_Generator>();
                break;

            case "파란 설탕 유리 꽃":
                gameObject.AddComponent<Food_Generator>();
                break;

            case "비스킷 의자":
                break;

            case "아이스크림 테이블":
                break;

            case "마카롱 쿠션":
                break;

            case "2단 마카롱 쿠션":
                break;

            case "딸기우유 연못":
                gameObject.AddComponent<Food_Generator>();
                break;

            case "솜사탕 구름 1":
                break;

            case "솜사탕 구름 2":
                break;

            case "캔디 가로등":
                break;

            case "롤리팝 캔디 나무":
                break;

            case "마카롱 나무":
                break;

            case "초코 분수":
                break;

            case "녹차 푸딩 산":
                break;

            case "초코 푸딩 산":
                break;

            case "거대한 롤리팝 마시멜로우 언덕":
                break;

            default:
                break;
        }

        gameObject.GetComponent<Food_Generator>().init();
    }
    
}
