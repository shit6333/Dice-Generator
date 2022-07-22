using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Dice : MonoBehaviour
{
    public GameObject dices;            // dices ���X����(��)
    public GameObject dicePrefab;
    public List<GameObject> diceList = new List<GameObject>();

    public Sprite[] diceSprites;        // Dice ���X�Ϥ�

    public GameObject initSpawnPoint;   // ��l�ͦ���l�I
    public float distance_x = 2.5f;        // ��l���Zx
    public float distance_y = 2.2f;        // ��l���Zy

    private void Awake()
    {
        // ��l�Ƴ��W��l
        for ( int i=0; i < dices.transform.childCount; i++)
        {
            diceList.Add(dices.transform.GetChild(i).gameObject);
        }
        
        // ��l�ƨC�ӻ�l�I��
        for(int i=0; i<diceList.Count; i++)
        {
            GameManager.diceNumbers.Add(0);
        }
    }

    // ====================================================================================

    // �R����l
    public void RemoveDice()
    {
        // �S����l
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


    // �ͦ��s��l
    public void GenerateDice()
    {
        GameObject newDice;
        // ���l�Ƥj�� 16 ����ͦ�
        if (diceList.Count >= 10)
        {
            Debug.Log("FULL of Dice");
            return;
        }

        // �ͦ���l
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

        // �N�s��l�]�� dices �l����
        newDice.transform.parent = dices.transform;
        // �N�s��l�[�J list
        diceList.Add(newDice);
        // �[�J�s��l�I�Ƹ�T
        GameManager.diceNumbers.Add(0);
    }


    // Roll ��l
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


    // �� Dice ���X�Ϥ�
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
