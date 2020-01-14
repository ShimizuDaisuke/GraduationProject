using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearManagement : MonoBehaviour
{
    // クリアしたか
    private bool IsClear;
    // 現在遊んでいるステージ名
    private string stageeName = "NONE";

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
    public bool IsPlayerClear { get { return IsClear;} set { IsClear = value; } }

    // 現在プレイヤーが遊んでいるステージ名
    public string PlayingStageName { get { return stageeName; } set { stageeName = value; } }
}
