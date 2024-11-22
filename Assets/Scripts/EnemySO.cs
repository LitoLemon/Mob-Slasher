using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/NewEnemy", order = 1)]
public class EnemySO : ScriptableObject
{
    public int hp;
    public int goldDrop;
    public Sprite sprite;
    public RuntimeAnimatorController animController;
}
