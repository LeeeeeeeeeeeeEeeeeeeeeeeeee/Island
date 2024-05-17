using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public string Name;
    public int Count;
    public int TempCount;
    public Sprite FoodImage;
    public TextMeshProUGUI CountText;
    public Image ImageObj;

    private void Update()
    {
        ImageObj.sprite = FoodImage;
        CountText.text = (Count - TempCount).ToString();

        TempCount = Inventory.Instance.Recipe.FindAll(n => n == Name).Count;
    }

    public void ChangeFood()
    {
        if (Count - TempCount > 0 && Inventory.Instance.SelectButton != null && Inventory.Instance.SelectButton.GetComponent<Image>().sprite != FoodImage)
        {
            Inventory.Instance.SelectButton.GetComponent<Image>().sprite = FoodImage;

            Inventory.Instance.SelectButton.SetActive(true);
        }
    }
}
