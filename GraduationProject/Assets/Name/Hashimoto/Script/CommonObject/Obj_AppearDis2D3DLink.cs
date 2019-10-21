// -----------------------------------------------------------------------------------------
//! @file       AppearDis2D3DObjLink.cs
//!
//! @brief      2Dのみや3Dのみ表示されるオブジェクトのデータに関係を持たせる
//!
//! @author     橋本 奉武
//!
//! @date       2019.10.05
// -----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_AppearDis2D3DLink : MonoBehaviour
{
    // このオブジェクトのデータに関係を持たせるオブジェクトの種類
    public enum Kind
    {
        ERR = -1,       // 例外
        GROUND          // 地面
    }

    // このオブジェクトのデータに関係を持たせるオブジェクトの種類
    [SerializeField]
    private Kind LinkedObjKind = Kind.ERR;

    // このオブジェクトのデータに関係を持たせるオブジェクト
    [SerializeField]
    private GameObject LinkedObj = default;

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // このオブジェクトのデータに関係を持たせるオブジェクトがない場合、何もしない
        if (LinkedObj == null) return;

        // このオブジェクトが非表示にされて、関係を持たせるオブジェクトが表示されている場合
        if ((this.gameObject.activeInHierarchy == false) && (LinkedObj.activeInHierarchy == true))
        {
            // オブジェクトの種類別にスクリプトのデータを繋げる
            switch (LinkedObjKind)
            {
                // 地面
                case Kind.GROUND:
                {
                    // 何もしない

                    break;
                }
            }
        }
    }

    /// <summary>
    /// 取得・設定関数
    /// </summary>

        // このオブジェクトのデータに関係を持たせるオブジェクト
        public GameObject ThisObjLinkedOtherObj { get { return LinkedObj; } private set{ LinkedObj = value; }}
}
