//=======================================================================================
//! @file   QRChangeObjct.cs
//! @brief  シーソーを定規に変更の処理
//! @author 田中歩夢
//! @date   01月15日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シーソーを定規に変更のクラス
public class QRChangeObjct : MonoBehaviour
{
    //シーソーをまとめたオブジェクト
    [SerializeField]
    private GameObject m_nomalObj = null;

    //定規をまとめたオブジェクト
    [SerializeField]
    private GameObject m_changeObj = null;

    //定規をまとめたオブジェクト
    [SerializeField]
    private GameObject m_changeObj_NoMove = null;


    public void ChangeRuler()
    {
        Destroy(m_nomalObj);
        Instantiate(m_changeObj, Vector3.zero, Quaternion.identity);

        if(m_changeObj_NoMove != null)
        {
            Instantiate(m_changeObj_NoMove, Vector3.zero, Quaternion.identity);
        }

    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.transform.tag == "Player")
        {
            ChangeRuler();
        }
    }

}
