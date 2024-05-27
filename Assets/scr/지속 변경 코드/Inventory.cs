using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public GameObject SelectButton;

    public List<string> Recipe = new List<string>();

    public GameObject CookPanel;

    public GameObject FoodStorage;
    public GameObject FoodPrefab;

    public TextMeshProUGUI AlertText;

    public Image CookImg;
    public TextMeshProUGUI CookText;
    public TextMeshProUGUI CookDescriptionText;

    //Store finished food information----------------------------
    Complete_Food cook = new Complete_Food();
    public List<GameObject> CompleteFoods = new List<GameObject>();
    //-----------------------------------------------------------

    public List<GameObject> PlusButtonImg = new List<GameObject>();
    public List<GameObject> InvenItems = new List<GameObject>();
    public List<String> InvenNames = new List<string>();

    public List<GameObject> Generated_Foods = new List<GameObject>();
    public List<Sprite> CookImgs = new List<Sprite>();

    private void Awake()
    {
        Instance = this;

        ResetRecipe();
    }

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (PlusButtonImg[i].GetComponent<Image>().sprite != null)
            {
                Recipe[i] = PlusButtonImg[i].GetComponent<Image>().sprite.name;
            }
        }

        CookText.text = cook.CookName;
        CookDescriptionText.text = cook.CookDescription;

        AlertText.color -= new Color(0, 0, 0, 0.3f) * Time.deltaTime;
    }

    public void SelectButtonChange(GameObject Self)
    {
        SelectButton = Self;
    }

    public void ResetRecipe()
    {
        SelectButton = null;

        for (int i = 0; i < 4; i++)
        {
            Recipe[i] = null;
        }

        foreach (GameObject img in PlusButtonImg)
        {
            img.GetComponent<Image>().sprite = null;
        }
    }

    public void ResetTempCount()
    {
        foreach(GameObject item in InvenItems)
        {
            item.GetComponent<Food>().TempCount = 0;
        }
    }

    public void MakeFood()
    {
        Debug.Log(Recipe.Count + "개");

        if(Recipe.FindAll(n => n == null).Count < 4)
        {
            foreach (GameObject item in InvenItems)
            {
                item.GetComponent<Food>().Count -= item.GetComponent<Food>().TempCount;
            }

            CookPanel.SetActive(true);
            

            if(Recipe.FindAll(n => n == "쌀").Count == 1)
            {
                
                cook.CookName = "주먹밥";
            }
            else if (Recipe.FindAll(n => n == "물").Count == 1)
            {
                cook.CookName = "물";
            }
            else if (Recipe.FindAll(n => n == "꿀").Count == 1 && Recipe.FindAll(n => n == null).Count == 3)
            {
                CookImg.sprite = CookImgs[2];
                cook.CookName = "꿀젤리";
                cook.CookDescription = "맑고 투명한 젤리 속에 꿀의 달콤함이 가득 담겨 있어, 한 입 베어 물면 마치 꽃밭에 온 것 같은 기분을 느낄 수 있어요.";
            }
            else if (Recipe.FindAll(n => n == "꿀").Count == 2 && Recipe.FindAll(n => n == null).Count == 2)
            {
                CookImg.sprite = CookImgs[5];
                cook.CookName = "돼지껍질요리";
                cook.CookDescription = "꿀꿀?! 돼지요리를 발견하셨네요!";
            }
            else if (Recipe.FindAll(n => n == "꿀").Count == 3 && Recipe.FindAll(n => n == null).Count == 1)
            {
                CookImg.sprite = CookImgs[5];
                cook.CookName = "돼지고기요리";
                cook.CookDescription = "꿀꿀꿀?! 돼지요리를 발견하셨네요!";
            }
            else if (Recipe.FindAll(n => n == "꿀").Count == 4 && Recipe.FindAll(n => n == null).Count == 0)
            {
                CookImg.sprite = CookImgs[5];
                cook.CookName = "돼지통구이";
                cook.CookDescription = "꿀꿀꿀꿀?! 돼지요리를 발견하셨네요!";
            }
            else if (Recipe.FindAll(n => n == "딸기").Count == 1 && Recipe.FindAll(n => n == null).Count == 3)
            {
                CookImg.sprite = CookImgs[10];
                cook.CookName = "구운 딸기";
                cook.CookDescription = "딸기 하나가 불길을 만나 달콤한 향기를 풍기며 마음까지 따뜻하게 해주어요.";
            }
            else if (Recipe.FindAll(n => n == "생선").Count == 1 && Recipe.FindAll(n => n == null).Count == 3)
            {
                CookImg.sprite = CookImgs[6];
                cook.CookName = "생선구이";
                cook.CookDescription = "이 작고 간단한 음식은 생선과 불길만 있으면 어디서든 준비할 수 있어요. 마을에서 발견한 신선한 생선을 사용해 만들면, 피로 회복은 물론 잠시의 여유까지 선사합니다.";
            }
            else if (Recipe.FindAll(n => n == "생선").Count == 2 && Recipe.FindAll(n => n == null).Count == 2)
            {
                CookImg.sprite = CookImgs[6];
                cook.CookName = "생선꼬치";
                cook.CookDescription = "바삭바삭하게 구운 생선꼬치입니다!";
            }
            else
            {
                CookImg.sprite = CookImgs[0];
                cook.CookName = "애매한 요리";
                cook.CookDescription = "흐음... 무언가 잘못넣었나?";
            }
            SaveFood(cook);
        }
    }
    void SaveFood(Complete_Food complete_food){
        Debug.Log(complete_food.CookName + " > 보관함에 저장되었습니다.");

        GameObject food = new GameObject();
        food.AddComponent<Complete_Food>();

        //CompleteFood init
        food.name = complete_food.CookName;
        food.GetComponent<Complete_Food>().CookName = complete_food.CookName;
        food.GetComponent<Complete_Food>().CookDescription = complete_food.CookDescription;

        CompleteFoods.Add(food);
    }
}