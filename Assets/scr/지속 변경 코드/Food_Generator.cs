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

        if(current_food_count > 0)
        {
            if (!gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().isPlaying)
            {
                gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            }
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
            GameObject Box;
            Food TempFoodData;
            if(food_prefabs.Length > 1){
                int sum = Random.Range(1,101);
                for(int i = 0; i < food_prefabs.Length; i++){
                    if(sum <= food_generate_Probability[i]){
                        obj = food_prefabs[i];
                        
                        int num = Inventory.Instance.InvenNames.IndexOf(obj.gameObject.name);

                        Box = Inventory.Instance.InvenItems[num];
                        TempFoodData = Box.GetComponent<Food>();
                        
                        TempFoodData.Count += 1;
                        
                        Inventory.Instance.AlertText.text = obj.gameObject.name + " 획득";
                        Inventory.Instance.AlertText.color = Color.white;
                        Debug.Log(obj.gameObject.name + "를 획득 하였습니다.");
                        break;
                    }
                    else if(sum > food_generate_Probability[i] && sum <= food_generate_Probability[i+1]){
                        obj = food_prefabs[i+1];

                        int num = Inventory.Instance.InvenNames.IndexOf(obj.gameObject.name);

                        Box = Inventory.Instance.InvenItems[num];
                        TempFoodData = Box.GetComponent<Food>();

                        TempFoodData.Count += 1;

                        Inventory.Instance.AlertText.text = obj.gameObject.name + " 획득";
                        Inventory.Instance.AlertText.color = Color.white;
                        Debug.Log(obj.gameObject.name + "를 획득 하였습니다.");
                        break;
                    }
                }
            }
            else{
                obj = food_prefabs[0];

                int num = Inventory.Instance.InvenNames.IndexOf(obj.gameObject.name);

                Box = Inventory.Instance.InvenItems[num];
                TempFoodData = Box.GetComponent<Food>();

                TempFoodData.Count += 1;

                Inventory.Instance.AlertText.text = obj.gameObject.name + " 획득";
                Inventory.Instance.AlertText.color = Color.white;
                Debug.Log(obj.gameObject.name + "를 획득 하였습니다.");
            }

            if (gameObject.name == "딸기우유 연못")
            {
                SoundManager.instance.PlaySound("Milk");
            }
            else if (gameObject.name == "빨간 설탕 유리 꽃" || gameObject.name == "노란 설탕 유리 꽃" || gameObject.name == "파란 설탕 유리 꽃")
            {
                SoundManager.instance.PlaySound("Sugar");
            }


        }
        else
        {
            Inventory.Instance.AlertText.text = "수확할 수 있는 재료가 없습니다!";
            Inventory.Instance.AlertText.color = Color.white;
            Debug.Log("수확할 수 있는 재료가 없습니다!");
        }
    }

}