using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Game : MonoBehaviour
{
    public GameObject hpTxt;
    public GameObject goldTxt;
    //index 0 for wave, index 1 for round
    public GameObject[] waveTxt = new GameObject[2];
    public GameObject button;


    public double hp = 75;


    public EnemySO[] enemies;
    public EnemySO[] bosses;

    public EnemySO enemy;

    private int wave = 1;
    private int round = 1;

    private int gold;

    public void DmgEnemy(float dmg)
    {
        //adjust hp
        hp -= dmg;
        hpTxt.GetComponent<TMP_Text>().text = "HP: " + hp.ToString();
        CheckHp();
    }

    private void CheckHp()
    {
        if (hp <= 0)
        {
            //enter death
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("Enemy is dead.");
        //disable sprite
        //add particle effects for enemy exploding into gold
        //add gold to player bank
        gold += enemy.goldDrop;
        goldTxt.GetComponent<TMP_Text>().text = "Gold: " + gold.ToString();
        NextRound();
    }

    private void NextEnemy(EnemySO[] enemyList)
    {
        //get next enemy
        //adjust stats accordingly
        int newEnemy = Random.Range(0, enemyList.Length);
        Debug.Log(newEnemy);
        enemy = enemyList[newEnemy];
        hp = enemy.hp;
        hpTxt.GetComponent<TMP_Text>().text = "HP: " + hp.ToString();
        button.GetComponent<UnityEngine.UI.Image>().sprite = enemy.sprite;

    }

    private void NextRound()
    {
        round++;
        if (round >= 10)
        {
            //if round equals 11, boss has been defeated
            if (round == 10) 
            {
                //start boss round
                NextEnemy(bosses);
                waveTxt[1].GetComponent<TMP_Text>().text = "Round: " + round.ToString();
                return;
            }
            else
            {
                wave++;
                round = 1;
            }
        }
        NextEnemy(enemies);
        //adjust UI
        waveTxt[0].GetComponent<TMP_Text>().text = "Wave: " + wave.ToString();
        waveTxt[1].GetComponent<TMP_Text>().text = "Round: " + round.ToString();
    }
}
