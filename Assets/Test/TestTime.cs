using System.Collections;
using System.Collections.Generic;
using TamLee;
using UnityEngine;

public class TestTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //创建定时器
            TimeAction action = GameEntry.Time.CreateTimeAction();
            action.Init(5, 1, 8, () =>
            {
                Debug.Log("定时器开始运行");
            }, (int loop) =>
            {
                Debug.Log("运行中 剩余次数=" + loop);
            }, () =>
            {
                Debug.Log("定时器运行完毕");
            }).Run();
        }
    }
}
