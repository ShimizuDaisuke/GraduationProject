//=======================================================================================
//! @file   ReleaseBall
//! @brief  Flagに触れた時Stopperを外す
//! @author 志水大輔
//! @date   1/17
//! @note   
//=======================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseBall : MonoBehaviour
{
    GameObject Stopper;
    // Start is called before the first frame update
    void Start()
    {
        Stopper = GameObject.FindGameObjectWithTag("Stopper");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //=======================================================================================
    //! @brief 当たった時の処理
    //! @param[in] 当たり判定
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    void OnTriggerEnter(Collider col)
    {
            // Playerに当たったら
        if (col.gameObject.tag == "BallRelease")
        {
            Destroy(Stopper);
        }
    }
}
