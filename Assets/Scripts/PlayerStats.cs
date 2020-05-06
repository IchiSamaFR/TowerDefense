using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [Header("stats")]
    public int  money;
    public int  startMoney;
    public int  maxHealth;
    int         health;
    public bool isDead;

    public int income;

    [Header("UI")]
    public Text txtMoney;
    public Text txtIncome;
    public Text txtHealth;

    void Start()
    {
        health = maxHealth;
        txtHealth.text = health + " hp";

        money = startMoney;
        txtMoney.text = money.ToString();
    }
    
    public void Paid(int amount)
    {
        money -= amount;
        txtMoney.text = money.ToString();
    }
    public void GetIncome()
    {
        money += income;
        txtMoney.text = money.ToString();
    }
    public void GetMoney(int amount)
    {
        money += amount;
        txtMoney.text = money.ToString();
    }

    void Update()
    {
        if(income > 0)
        {
            txtIncome.text = "+" + income;
        } else
        {
            txtIncome.text = "";
        }
    }

    public void GetDmg(int amount)
    {
        if(health > 0)
        {
            health -= amount;
            txtHealth.text = health + " hp";
        }
        if(health <= 0)
        {
            IsDead();
        }
    }

    void IsDead()
    {
        money = 0;
        isDead = true;
        this.GetComponent<Pause>().PlayerDead();
    }
}
