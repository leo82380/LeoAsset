using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    [SerializeField] private int _level = 1;
    [SerializeField] private int defaultCost;
    [SerializeField] private int cost1;
    [SerializeField] private int cost2;
    [SerializeField] private int cost3;
    [SerializeField] private int commonDifference;
    
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text costText1;
    [SerializeField] private TMP_Text costText2;
    [SerializeField] private TMP_Text costText3;


    private void Start()
    {
        cost1 = defaultCost;
        cost2 = CalculateCost(_level, 5, commonDifference) - CalculateCost(_level, 1, commonDifference);
        cost3 = CalculateCost(_level, 10, commonDifference) - CalculateCost(_level, 1, commonDifference);
        UpdateUI();
    }

    public void OneLevelUp()
    {
        _level++;
        cost1 += commonDifference;
        cost2 = CalculateCost(_level, 1, commonDifference) - CalculateCost(_level, 1, commonDifference);
        cost3 = CalculateCost(_level, 1, commonDifference) - CalculateCost(_level, 1, commonDifference);
        UpdateUI();
    }
    
    public void FiveLevelUp()
    {
        _level += 5;
        cost1 = CalculateCost(_level, 5, commonDifference) - CalculateCost(_level, 1, commonDifference);
        cost2 = CalculateCost(_level, 5, commonDifference) - CalculateCost(_level, 1, commonDifference);
        cost3 = CalculateCost(_level, 5, commonDifference) - CalculateCost(_level, 1, commonDifference);
        UpdateUI();
    }
    
    public void TenLevelUp()
    {
        _level += 10;
        cost1 = CalculateCost(_level, 10, commonDifference) - CalculateCost(_level, 1, commonDifference);
        cost2 = CalculateCost(_level, 10, commonDifference) - CalculateCost(_level, 5, commonDifference);
        cost3 = CalculateCost(_level, 10, commonDifference) - CalculateCost(_level, 1, commonDifference);
        UpdateUI();
    }
    
    public void UpdateUI()
    {
        levelText.text = $"Level: {_level}";
        costText1.text = $"Cost: {cost1}";
        costText2.text = $"Cost: {cost2}";
        costText3.text = $"Cost: {cost3}";
    }

    private int CalculateCost(int level, int increaseLevel, int commonDifference)
    {
        return (_level * (defaultCost + (defaultCost + (increaseLevel - 1) * commonDifference))) / 2;
    }

}
