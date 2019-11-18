//=======================================================================================
//! @file   PencilCaseCoverClose.cs
//! @brief  筆箱のカバーを閉じる処理
//! @author 田中歩夢
//! @date   11月13日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//筆箱のカバーを閉じるクラス
public class PencilCaseCoverClose : MonoBehaviour
{
    //当たったらゴール座標に移動のクラス
    [SerializeField]
    private GoolPosMovePlayer m_goolPosMovePlayer = default;

    //回転量
    [SerializeField]
    private float m_rotationAmount = 1.0f;

    //カバーが閉まる角度
    private const float COVER_MAXANGLE = 205.0f;

    //リザルトメインのスクリプト
    [SerializeField]
    private ClearManagement m_clearManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //消しゴムがゴールに入った
        if(m_goolPosMovePlayer.GoolINFlag)
        {
            //カバーが閉まるまで回転
            if(transform.localEulerAngles.z <= COVER_MAXANGLE)
            {
                transform.Rotate(new Vector3(0, 0, m_rotationAmount) * Time.deltaTime);
            }
            else
            {
                m_clearManager.GetComponent<ClearManagement>().IsPlayerClear = true;
                //リザルトシーンへ
                SceneManager.LoadScene("Result");
            }
           
        }

    }
}
