﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    [SerializeField] private int maxHP;
    private int currentHP;
    [SerializeField] private float SecondsToRegenerateStamina;
    [SerializeField] private int maxSP;
    Movement_controller playerMovment;
    private int currentSP;
    private DateTime beginCountSP;

    private bool canBeDamaged=true;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovment = GetComponent<Movement_controller>();
        playerMovment.OnGetHurt += OnGetHurt;
        currentSP = maxSP;
        currentHP = maxHP;
    }
    private bool saver = false;
    private void FixedUpdate()
    {
        if (currentSP < 100)
        {
            TimerIncreasePoints();
            if (!saver)
            {
                saver = true;
                beginCountSP = DateTime.Now;
            }
        }

    }
    private void TimerIncreasePoints()
    {
        float difference = (float)(DateTime.Now - beginCountSP).TotalSeconds;
        if (difference > SecondsToRegenerateStamina)
        {
            ChangeSP(50);
            beginCountSP = DateTime.Now;
        }
        
    }
    public void TakeDamage(int damage, DamageType type = DamageType.Casual,Transform enemy=null)
    {
        if (!canBeDamaged)
            return;
        currentHP -= damage;
        if (currentHP <= 0)
            playerAnimator.SetBool("Death",true);
        switch (type)
        {
            case DamageType.PowerStrike:
                playerMovment.GetHurt(enemy.position);
                break;
        }
        Debug.Log("Damage = " + damage);
        Debug.Log("HP = " + currentHP);
    }
    private void OnGetHurt(bool canBeDamaged)
    {
        this.canBeDamaged = canBeDamaged;
    }
    public void RestoredHP(int value)
    {
        currentHP += value;
        if (currentHP > maxHP)
            currentHP = maxHP;
        Debug.Log("HP = " + currentHP);
    }
    public bool ChangeSP(int value)
    {
        if (value < 0 && currentSP < Mathf.Abs(value))
            return false;
        currentSP += value;
        if (currentSP > maxSP)
            currentSP = maxSP;
        Debug.Log("currentSP = " + currentSP);
        return true;
    }
}
