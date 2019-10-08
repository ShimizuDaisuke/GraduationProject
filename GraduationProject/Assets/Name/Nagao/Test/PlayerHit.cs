//======================================================================================= 
//! @file       PlayerHit.cs 
//! @brief      刃物とプレイヤーの当たり判定を行う
//! @author     長尾昌輝 
//! @date       2019/10/08
//======================================================================================= 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    //プレイヤーの大きさ
    [SerializeField]
    private Vector3 size = new Vector3(1.0f,1.0f,1.0f);

    // プレイヤーの監督
    [SerializeField]
    private GameObject PlayerDirector = default;
    // スクリプト：Playerを点滅させる処理
    private PlayerFlashing Script_PlayerFlashing;

    //プレイヤーのサイズの最大値
    [SerializeField]
    private Vector3 maxSize = new Vector3(1.0f, 1.0f, 1.0f);

    //プレイヤーのサイズの最小値
    [SerializeField]
    private Vector3 minSize = new Vector3(0.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        Script_PlayerFlashing = PlayerDirector.GetComponent<PlayerFlashing>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = size;
    }


    //OnTriggerEnter
    void OnTriggerStay(Collider col)
    {

        if ((col.gameObject.tag == "Sword") && (Script_PlayerFlashing.IsPlayerFlashing == false))
        {
            Debug.Log("うんち！");
            size +=  -new Vector3(0.01f, 0.01f, 0.01f);
            Script_PlayerFlashing.IsPlayerFlashing = true;
        }
    }
}
