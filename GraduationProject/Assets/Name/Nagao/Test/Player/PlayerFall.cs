//=======================================================================================
//! @file   GoolFlag.cs
//! @brief  プレイヤーが落下したらシーン切り替えの処理
//! @author 長尾昌輝
//! @date   2019/11/13
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFall : MonoBehaviour
{
    //リザルトメインのスクリプト
    private ClearManagement clearManager;

    //破棄しないように設定したオブジェクト
    [SerializeField]
    private GameObject ClearObject = default;

    //落下位置
    [SerializeField]
    private float m_fallPos = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //リザルトメインのスクリプトの割り当て
        clearManager = ClearObject.GetComponent<ClearManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        //落下位置よりもプレイヤーが下に行ったら
        if(this.gameObject.transform.position.y < m_fallPos)
        {
            //ゲームを失敗判定に
            clearManager.IsPlayerClear = false;
            //リザルトシーンへ
            SceneManager.LoadScene("Result");
        }
    }
}
