//=======================================================================================
//! @file   CreateEraserDust.cs
//! @brief  消しカスを生成する処理
//! @author 田中歩夢
//! @date   10月16日
//! @note   ない
//======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//消しカスを生成するクラス
public class CreateEraserDust : MonoBehaviour
{
    //消しカスオブジェクトのプレハブ
    [SerializeField]
    private GameObject objPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// 生成処理
    /// </summary>
    /// <param name="pos">生成する座標</param>
    public void Create(Vector3 pos)
    { 
        //生成
        Instantiate(objPrefab, pos, Quaternion.identity);
        //動きを止めておく
        objPrefab.GetComponent<EraserDust>().IsEraserMove = EraserDust.EraserMove.STOP;
        
    }
}
