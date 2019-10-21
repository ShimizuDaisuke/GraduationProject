//======================================================================================= 
//! @file       EnemyDamage.cs 
//! @brief      何を行うファイルなのか 
//! @author     長尾昌輝 
//! @date       2019/10/08 
//! @note       メモ  ※書かなくてもいい 
//======================================================================================= 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //ダメージ
    private int damage;

    //最大ダメージ
    [SerializeField]
    private int maxDamage = 0;

    //最小ダメージ
    [SerializeField]
    private int minDamage = 0;


    //Update
    void Start()
    {
        //ダメージ数を決める
        damage = Random.Range(minDamage, maxDamage + 1);

    }


    public int IsDamage { get { return damage; } set { damage = value; } }
}
