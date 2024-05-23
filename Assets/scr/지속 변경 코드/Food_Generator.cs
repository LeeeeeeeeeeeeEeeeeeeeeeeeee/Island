using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Food_Generator : MonoBehaviour
{
    public bool day_skip;
    public int max_food_count = 3;
    public int current_food_count = 0;
    
    public int[] food_generate_Probability;
    public GameObject[] food_prefabs;
    
    public void init() {
        day_skip = true;
        Debug.Log(gameObject.name + " 생성");

        switch(gameObject.name){
            case "빨간 설탕 유리 꽃":
                max_food_count = 3;

                food_generate_Probability = new int[2];
                food_generate_Probability[0] = 70;
                food_generate_Probability[1] = 100;

                food_prefabs = new GameObject[2];
                food_prefabs[0] = Inventory.Instance.Generated_Foods[0];
                food_prefabs[1] = Inventory.Instance.Generated_Foods[1];
                break;

            case "노란 설탕 유리 꽃":
                max_food_count = 3;

                food_generate_Probability = new int[2];
                food_generate_Probability[0] = 70;
                food_generate_Probability[1] = 100;

                food_prefabs = new GameObject[2];
                food_prefabs[0] = Inventory.Instance.Generated_Foods[0];
                food_prefabs[1] = Inventory.Instance.Generated_Foods[1];
                break;

            case "파란 설탕 유리 꽃":
                max_food_count = 3;

                food_generate_Probability = new int[2];
                food_generate_Probability[0] = 70;
                food_generate_Probability[1] = 100;

                food_prefabs = new GameObject[2];
                food_prefabs[0] = Inventory.Instance.Generated_Foods[0];
                food_prefabs[1] = Inventory.Instance.Generated_Foods[1];
                break;

            case "딸기우유 연못":
                max_food_count = 3;

                food_generate_Probability = new int[3];
                food_generate_Probability[0] = 40;
                food_generate_Probability[1] = 60;
                food_generate_Probability[2] = 100;

                food_prefabs = new GameObject[3];
                food_prefabs[0] = Inventory.Instance.Generated_Foods[2];
                food_prefabs[1] = Inventory.Instance.Generated_Foods[3];
                food_prefabs[2] = Inventory.Instance.Generated_Foods[4];
                break;

            default:
                break;
        }
    }

    void Update() {
        if(day_skip){  //디버깅용 코드
            day_skip = false;
            Generate_Food();
        }
    }

    void Generate_Food()
    {
        current_food_count = max_food_count;
        Debug.Log(gameObject.name + " : 재료가 생산되었습니다!");
    }

    public void Get_Food()
    {
        if(current_food_count > 0){
            current_food_count--;
            
            GameObject obj;
            GameObject Box = Instantiate(Inventory.Instance.FoodPrefab, Inventory.Instance.FoodStorage.transform.position, Quaternion.identity);
            Food TempFoodData;
            if(food_prefabs.Length > 1){
                int sum = Random.Range(1,101);
                for(int i = 0; i < food_prefabs.Length; i++){
                    if(sum <= food_generate_Probability[i]){
                        obj = food_prefabs[i];

                        Box.gameObject.name = obj.gameObject.name;
                        
                        if(Inventory.Instance.InvenNames.Contains(Box.gameObject.name))
                        {
                            int num = Inventory.Instance.InvenNames.IndexOf(Box.gameObject.name);
                            
                            Destroy(Box);
                            Box = Inventory.Instance.InvenItems[num];
                            TempFoodData = Box.GetComponent<Food>();
                        
                            TempFoodData.Name = obj.GetComponent<Food>().Name;
                            TempFoodData.Count += 1;
                            TempFoodData.ImageObj.GetComponent<Image>().sprite = obj.GetComponent<Food>().FoodImage;
                            TempFoodData.FoodImage = obj.GetComponent<Food>().FoodImage;
                        }
                        else
                        {
                            Inventory.Instance.InvenItems.Add(Box);
                            Inventory.Instance.InvenNames.Add(Box.gameObject.name);
                            Box.transform.SetParent(Inventory.Instance.FoodStorage.transform);
                            TempFoodData = Box.GetComponent<Food>();
                        
                            TempFoodData.Name = obj.GetComponent<Food>().Name;
                            TempFoodData.Count += 1;
                            TempFoodData.ImageObj.GetComponent<Image>().sprite = obj.GetComponent<Food>().FoodImage;
                            TempFoodData.FoodImage = obj.GetComponent<Food>().FoodImage;
                        }
                        Box.GetComponent<RectTransform>().localScale = new Vector3(0.9f,0.8f,1);
                        Debug.Log(obj.gameObject.name + "를 획득 하였습니다.");
                        break;
                    }
                    else if(sum > food_generate_Probability[i] && sum <= food_generate_Probability[i+1]){
                        obj = food_prefabs[i+1];
                        
                        Box.gameObject.name = obj.gameObject.name;
                        
                        if(Inventory.Instance.InvenNames.Contains(Box.gameObject.name))
                        {
                            int num = Inventory.Instance.InvenNames.IndexOf(Box.gameObject.name);
                            
                            Destroy(Box);
                            Box = Inventory.Instance.InvenItems[num];
                            TempFoodData = Box.GetComponent<Food>();
                        
                            TempFoodData.Name = obj.GetComponent<Food>().Name;
                            TempFoodData.Count += 1;
                            TempFoodData.ImageObj.GetComponent<Image>().sprite = obj.GetComponent<Food>().FoodImage;
                            TempFoodData.FoodImage = obj.GetComponent<Food>().FoodImage;
                        }
                        else
                        {
                            Inventory.Instance.InvenItems.Add(Box);
                            Inventory.Instance.InvenNames.Add(Box.gameObject.name);
                            Box.transform.SetParent(Inventory.Instance.FoodStorage.transform);
                            TempFoodData = Box.GetComponent<Food>();
                        
                            TempFoodData.Name = obj.GetComponent<Food>().Name;
                            TempFoodData.Count += 1;
                            TempFoodData.ImageObj.GetComponent<Image>().sprite = obj.GetComponent<Food>().FoodImage;
                            TempFoodData.FoodImage = obj.GetComponent<Food>().FoodImage;
                        }
                        Box.GetComponent<RectTransform>().localScale = new Vector3(0.9f,0.8f,1);
                        Debug.Log(obj.gameObject.name + "를 획득 하였습니다.");
                        break;
                    }
                }
            }
            else{
                obj = food_prefabs[0];
                
                Box.gameObject.name = obj.gameObject.name;
                
                if(Inventory.Instance.InvenNames.Contains(Box.gameObject.name))
                {
                    int num = Inventory.Instance.InvenNames.IndexOf(Box.gameObject.name);
                    
                    Destroy(Box);
                    Box = Inventory.Instance.InvenItems[num];
                    TempFoodData = Box.GetComponent<Food>();
                
                    TempFoodData.Name = obj.GetComponent<Food>().Name;
                    TempFoodData.Count += 1;
                    TempFoodData.ImageObj.GetComponent<Image>().sprite = obj.GetComponent<Food>().FoodImage;
                    TempFoodData.FoodImage = obj.GetComponent<Food>().FoodImage;
                }
                else
                {
                    Inventory.Instance.InvenItems.Add(Box);
                    Inventory.Instance.InvenNames.Add(Box.gameObject.name);
                    Box.transform.SetParent(Inventory.Instance.FoodStorage.transform);
                    TempFoodData = Box.GetComponent<Food>();
                
                    TempFoodData.Name = obj.GetComponent<Food>().Name;
                    TempFoodData.Count += 1;
                    TempFoodData.ImageObj.GetComponent<Image>().sprite = obj.GetComponent<Food>().FoodImage;
                    TempFoodData.FoodImage = obj.GetComponent<Food>().FoodImage;
                }
                Box.GetComponent<RectTransform>().localScale = new Vector3(0.9f,0.8f,1);
                Debug.Log(obj.gameObject.name + "를 획득 하였습니다.");
            }

            
        }
        else{
            Debug.Log("수확할 수 있는 재료가 없습니다!");
        }
    }

}