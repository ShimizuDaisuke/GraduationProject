//=======================================================================================
//! @file   EraserDustController.cs
//! @brief  消しカスの動きの処理
//! @author 田中歩夢
//! @date   10月16日
//! @note   ない
//======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//消しカスの動きのクラス
public class EraserDustController : MonoBehaviour
{
    //速度
    [SerializeField]
    private float m_speed = 1.0f;

    //消しカスのクラス
    private EraserDust m_eraserDust = default;

    //ターゲット座標
    private Vector3 m_targetPos;

    // Start is called before the first frame update
    void Start()
    {
        m_eraserDust = GetComponent<EraserDust>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //追いかける
        Chase(m_targetPos);
        
    }

    /// <summary>
    /// 追いかける
    /// </summary>
    /// <param name="targetPos">ターゲットの座標</param>
    public void Chase(Vector3 targetPos)
    {
        if(m_eraserDust.IsEraserMove == EraserDust.EraserMove.CHASE)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, m_speed);
        }
        
    }


    //消しカスの種類の取得・設定
    public Vector3 IsTargetPos { get { return m_targetPos; } set { m_targetPos = value; } }

    public EraserDust IsEraserDust { get { return m_eraserDust; } set { m_eraserDust = value; } }



}
