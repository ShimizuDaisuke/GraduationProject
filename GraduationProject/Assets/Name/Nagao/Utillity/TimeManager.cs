using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // クリアしたか
    static private float IsTime;

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
    public float IsPlayerTime { get { return IsTime; } set { IsTime = value; } }
}
