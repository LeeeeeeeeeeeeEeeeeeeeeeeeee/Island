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

        if (Count == 0)
        {
            Inventory.Instance.InvenItems.Remove(gameObject);
            Inventory.Instance.InvenNames.Remove(gameObject.name);
            Destroy(gameObject);
        }
    }

    public void ChangeFood()
    {
        if (Count - TempCount > 0 && Inventory.Instance.SelectButton != null && Inventory.Instance.SelectButton.GetComponent<Image>().sprite != FoodImage)
        {
            Inventory.Instance.SelectButton.GetComponent<Image>().sprite = FoodImage;

            Inventory.Instance.SelectButton.SetActive(true);
        }
        else if(Count - TempCount > 0)
        {
            if (Inventory.Instance.PlusButtonImg[0].activeSelf == false)
            {
                Inventory.Instance.PlusButtonImg[0].GetComponent<Image>().sprite = FoodImage;

                Inventory.Instance.PlusButtonImg[0].SetActive(true);
            }
            else if (Inventory.Instance.PlusButtonImg[1].activeSelf == false)
            {
                Inventory.Instance.PlusButtonImg[1].GetComponent<Image>().sprite = FoodImage;

                Inventory.Instance.PlusButtonImg[1].SetActive(true);
            }
            else if (Inventory.Instance.PlusButtonImg[2].activeSelf == false)
            {
                Inventory.Instance.PlusButtonImg[2].GetComponent<Image>().sprite = FoodImage;

                Inventory.Instance.PlusButtonImg[2].SetActive(true);
            }
            else if (Inventory.Instance.PlusButtonImg[3].activeSelf == false)
            {
                Inventory.Instance.PlusButtonImg[3].GetComponent<Image>().sprite = FoodImage;

                Inventory.Instance.PlusButtonImg[3].SetActive(true);
            }
        }
    }
}
