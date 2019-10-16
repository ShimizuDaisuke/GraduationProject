using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // クリアしたか
    static private int IsScore;

    // Start is called before the first frame update
    void Start()
    {
        // このオブジェクトを他のシーンへ遷移しても破棄しないように設定
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 取得設定関数
    public int IsPlayerScore { get { return IsScore; } set { IsScore = value; } }
}
