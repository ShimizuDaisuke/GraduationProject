using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //ジェイスティック
    [SerializeField]
    private Joystick m_joystick = null;
    //速度
    [SerializeField]
    private float m_vel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dx = m_joystick.Horizontal;
        float dy = m_joystick.Vertical;

        transform.Translate(dx * m_vel, 0.0f, dy * m_vel);
    }
}
