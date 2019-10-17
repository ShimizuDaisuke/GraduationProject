using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AutoDestroy : MonoBehaviour
{
    // オブジェクトが消滅するまでカウントダウン
    private float count = 0.0f;

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 消滅のカウントダウンが始まったら
        if (count > 0.0f)
        {
            // 時間を図る
            count += -Time.deltaTime;

            // 消滅カウントダウンが0になったら
            if (count <= 0.0f)
            {
                // このオブジェクトを消滅させる
                Destroy(this.gameObject);
            }
        }
    }

    /// <summary>
    /// 取得・設定関数
    /// </summary>

        // オブジェクトが消滅するまでカウントダウン
         public float time { get { return count; } set { count = value; } }

}

