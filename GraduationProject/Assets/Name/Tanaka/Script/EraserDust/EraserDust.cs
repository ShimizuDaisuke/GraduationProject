//=======================================================================================
//! @file   EraserDust.cs
//! @brief  消しカスの処理
//! @author 田中歩夢
//! @date   10月07日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//消しカスのクラス
public class EraserDust : MonoBehaviour
{
    //消しカスの種類
    public enum EraserDustKIND
    {
        ERR,            //エラー
        NORMAL,         //ノーマル
        ONEUPGAUGE,     //１UP
    };
    [SerializeField]
    private EraserDustKIND m_eraserDustKIND = EraserDustKIND.ERR;


    //消しカスの動き
    public enum EraserMove
    {
        STOP,           //止まる
        CHASE,          //追いかける
    }
    [SerializeField]
    private EraserMove m_eraserMove = EraserMove.STOP;


    //ポイント
    [SerializeField]
    private int m_point;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //消しカスの種類の取得・設定
    public EraserDustKIND IsEraserDustKind { get { return m_eraserDustKIND; } set { m_eraserDustKIND = value; } }

    //消しカスの種類の取得・設定
    public EraserMove IsEraserMove { get { return m_eraserMove; } set { m_eraserMove = value; } }

    // ポイントの取得・設定
    public int PointRandom { get { return m_point; } set { m_point = value; } }
}
