using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public Image CookImg;
    public TextMeshProUGUI CookText;
    public string Cook;
    public List<GameObject> CookUI = new List<GameObject>();
    public List<GameObject> InvenUI = new List<GameObject>();
    public List<String> InvenNames = new List<string>();
    public List<GameObject> inventory = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

        ResetRecipe();
    }

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (inventory[i].GetComponent<Image>().sprite != null)
            {
                Recipe[i] = inventory[i].GetComponent<Image>().sprite.name;
            }
        }

        CookText.text = Cook;
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

        foreach (GameObject img in CookUI)
        {
            img.GetComponent<Image>().sprite = null;
        }
    }

    public void ResetTempCount()
    {
        foreach(GameObject item in InvenUI)
        {
            item.GetComponent<Food>().TempCount = 0;
        }
    }

    public void MakeFood()
    {
        if(Recipe.FindAll(n => n == null).Count < 4)
        {
            foreach (GameObject item in InvenUI)
            {
                item.GetComponent<Food>().Count -= item.GetComponent<Food>().TempCount;
            }

            CookPanel.SetActive(true);

            if(Recipe.FindAll(n => n == "쌀").Count == 1)
            {
                Cook = "주먹밥";
            }
            else if (Recipe.FindAll(n => n == "물").Count == 1)
            {
                Cook = "우어터";
            }
            else
            {
                Cook = "음쓰";
            }
        }
    }
}
