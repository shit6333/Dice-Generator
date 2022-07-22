using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Dice : MonoBehaviour
{
    public GameObject dices;            // dices 集合物件(父)
    public GameObject dicePrefab;
    public List<GameObject> diceList = new List<GameObject>();

    public Sprite[] diceSprites;        // Dice 號碼圖片

    public GameObject initSpawnPoint;   // 初始生成骰子點
    public float distance_x = 2.5f;        // 骰子間距x
    public float distance_y = 2.2f;        // 骰子間距y

    private void Awake()
    {
        // 初始化場上骰子
        for ( int i=0; i < dices.transform.childCount; i++)
        {
            diceList.Add(dices.transform.GetChild(i).gameObject);
        }
        
        // 初始化每個骰子點數
        for(int i=0; i<diceList.Count; i++)
        {
            GameManager.diceNumbers.Add(0);
        }
    }

    // ====================================================================================

    // 刪除骰子
    public void RemoveDice()
    {
        // 沒有骰子
        if(diceList.Count <= 0)
        {
            Debug.Log("There is no dice");
            return;
        }

        GameObject removeDice = diceList[diceList.Count-1];
        Destroy(removeDice);
        diceList.RemoveAt(diceList.Count-1);
        GameManager.diceNumbers.Remove(GameManager.diceNumbers.Count-1);
    }


    // 生成新骰子
    public void GenerateDice()
    {
        GameObject newDice;
        // 當骰子數大於 16 停止生成
        if (diceList.Count >= 10)
        {
            Debug.Log("FULL of Dice");
            return;
        }

        // 生成骰子
        if (diceList.Count == 0)
        {
            newDice = Instantiate(dicePrefab, initSpawnPoint.transform.position, Quaternion.identity);
        }
        else if(diceList[diceList.Count - 1].transform.position.x >= 4.6)
        {
            Vector3 spawnPoint = new Vector3(initSpawnPoint.transform.position.x,
                                             initSpawnPoint.transform.position.y - distance_y,
                                             initSpawnPoint.transform.position.z);
            newDice = Instantiate(dicePrefab, spawnPoint, Quaternion.identity);
        }
        else
        {
            Vector3 spawnPoint = new Vector3(diceList[diceList.Count-1].transform.position.x + distance_x,
                                             diceList[diceList.Count - 1].transform.position.y,
                                             diceList[diceList.Count - 1].transform.position.z);
            newDice = Instantiate(dicePrefab, spawnPoint, Quaternion.identity);
        }

        // 將新骰子設為 dices 子物件
        newDice.transform.parent = dices.transform;
        // 將新骰子加入 list
        diceList.Add(newDice);
        // 加入新骰子點數資訊
        GameManager.diceNumbers.Add(0);
    }


    // Roll 骰子
    public void Roll()
    {
        for (int i = 0; i < diceList.Count; i++)
        {
            GameManager.diceNumbers[i] = Random.Range(GameManager.minDiceNumber, GameManager.maxDiceNumber + 1);
            Debug.Log("Dice" + i + ": " + GameManager.diceNumbers[i]);
            ChangeDiceImage(diceList[i], GameManager.diceNumbers[i]);
        }

        Debug.Log("======================");
    }


    // 更換 Dice 號碼圖片
    void ChangeDiceImage(GameObject dice, int diceNumber)
    {
        SpriteRenderer sp = dice.GetComponent<SpriteRenderer>();
        sp.sprite = diceSprites[diceNumber - 1];
    }


    //void ChangeText(GameObject diceText ,int num )
    //{
    //    TextMeshProUGUI numberText = diceText.GetComponent<TextMeshProUGUI>();
    //    numberText.text = num.ToString();
    //}
}
