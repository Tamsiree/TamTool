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
            //������ʱ��
            TimeAction action = GameEntry.Time.CreateTimeAction();
            action.Init(5, 1, 8, () =>
            {
                Debug.Log("��ʱ����ʼ����");
            }, (int loop) =>
            {
                Debug.Log("������ ʣ�����=" + loop);
            }, () =>
            {
                Debug.Log("��ʱ���������");
            }).Run();
        }
    }
}
