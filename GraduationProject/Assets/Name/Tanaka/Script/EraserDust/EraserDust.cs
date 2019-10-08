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
    public enum EraserDustKIND
    {
        ERR,            //エラー
        NORMAL,         //ノーマル
        ONEUPGAUGE,     //１UP
    };
    //消しカスの種類
    [SerializeField]
    private EraserDustKIND m_eraserDustKIND = EraserDustKIND.ERR;

    // ポイント
    // 種類:ノーマル→スコア数   / 種類:1UP→1upゲージの量 
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

    // ポイントの取得・設定
    public int Point { get { return m_point; } set { m_point = value; } }
}
