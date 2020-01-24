//=======================================================================================
//! @file   BrakeNeedle.cs
//! @brief  折れる針の処理
//! @author 田中歩夢
//! @date   01月22日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//折れる針のクラス
public class BrakeNeedle : MonoBehaviour
{
    //プレイヤーの属性
    [SerializeField]
    private PlayerType m_playerType = default;

    //針
    [SerializeField]
    private GameObject m_needleObj = default;

    //ボックスコライダー
    private BoxCollider m_boxCol = default;

    // Start is called before the first frame update
    void Start()
    {
        m_boxCol = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(m_playerType.IsPlayerType == PlayerType.Type.IRON)
            {
                m_needleObj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                m_needleObj.transform.localScale = new Vector3(1.0f, 0.25f, 1.0f);
                m_boxCol.enabled = false;
            } 
        }
    }

}
