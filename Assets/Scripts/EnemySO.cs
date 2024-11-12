using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/NewEnemy", order = 1)]
public class EnemySO : ScriptableObject
{
    public double hp;
    public int goldDrop;
    public Sprite sprite; 
}
