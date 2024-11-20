using System;
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
    public GameObject[] hpSliders = new GameObject[2];
    public GameObject button;

    public float deathTime;
    public int hp = 8;


    public EnemySO[] enemies;
    public EnemySO[] bosses;

    public EnemySO enemy;

    private int wave = 1;
    private int round = 1;

    private int clickDmg = 1;
    private int gold;

    public void DmgEnemy()
    {
        //adjust hp
        hp -= clickDmg;
        hpTxt.GetComponent<TMP_Text>().text = "HP: " + hp.ToString();
        foreach(GameObject slider in hpSliders)
        {
            slider.GetComponent<UnityEngine.UI.Slider>().value = hp;
        }
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
        button.SetActive(false);
        //add particle effects for enemy exploding into gold
        //add gold to player bank
        AdjustGold(enemy.goldDrop);
        NextRound();
    }

    private void NextEnemy(EnemySO[] enemyList)
    {
        //get next enemy
        //adjust stats accordingly
        int newEnemy = UnityEngine.Random.Range(0, enemyList.Length);
        Debug.Log(newEnemy);
        enemy = enemyList[newEnemy];
        hp = enemy.hp;
        hpTxt.GetComponent<TMP_Text>().text = "HP: " + hp.ToString();
        foreach (GameObject slider in hpSliders)
        {
            UnityEngine.UI.Slider slide = slider.GetComponent<UnityEngine.UI.Slider>();
            slide.maxValue = hp;
            slide.value = hp;
        }
        button.GetComponent<UnityEngine.UI.Image>().sprite = enemy.sprite;

        StartCoroutine(DeathCd(deathTime));
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


    public void IncreaseClickDmg()
    {
        if(gold >= 10)
        {
            clickDmg++;
            AdjustGold(-10);
        }

    }

    private void AdjustGold(int amount)
    {
        gold += amount;
        goldTxt.GetComponent<TMP_Text>().text = gold.ToString() + "G";
    }

    private IEnumerator DeathCd(float time)
    {
        for(float remainingTime = time; remainingTime < 0; remainingTime -= Time.deltaTime)
        {
            continue;            
        }
        button.SetActive(true);
        return null;

    }
}
