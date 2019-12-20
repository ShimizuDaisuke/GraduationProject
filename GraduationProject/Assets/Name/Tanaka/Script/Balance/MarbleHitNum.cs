//=======================================================================================
//! @file   MarbleHitNum.cs
//! @brief  天秤にビー玉が当たった数を取得の処理
//! @author 田中歩夢
//! @date   12月16日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//天秤にビー玉が当たった数を取得のクラス
public class MarbleHitNum : MonoBehaviour
{
    //ヒットしているオブジェクトのリスト
    private List<GameObject> m_hitList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Marble" && !m_hitList.Contains(collider.gameObject))
        {
            m_hitList.Add(collider.gameObject);
        }
    }

    //衝突判定
    void OnTriggerExit(Collider collider)
    {
        if (collider.transform.tag == "Marble" && m_hitList.Contains(collider.gameObject))
        {
            m_hitList.Remove(collider.gameObject);
        }
    }

    //回路がつながったフラグの取得・設定
    public int HitNum { get { return m_hitList.Count; }}

}
