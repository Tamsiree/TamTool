using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TamLee;
/// <summary>
/// �ض����ǰ����ʹ��ǰ�������ã���Ȼ���ݻ��ʹ��ǰһ��
/// ����ʹ�ô������Ĺ��캯��������ʹ��Init()������Ϊ��ʼ����������������
/// </summary>
public class TestPool : MonoBehaviour
{
    private void Start()
    {
        GameEntry.Pool.SetClassObjectResident<CusUserData>(3);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            //StringBuilder sbr = GameEntry.Pool.Dequeue<StringBuilder>();
            //sbr.Length = 0;//���ã���Ȼ����ϸ���������һ��
            //sbr.Append("123");
            //Debug.Log(sbr.ToString());

            //GameEntry.Pool.Enqueue(sbr);
            //CusUserData data = GameEntry.Pool.Dequeue<CusUserData>();

            //data.Init(1, "Data.Name");
            //Debug.Log(data.Id);
            //Debug.Log(data.Name);

            //GameEntry.Pool.Enqueue(data);
            CusUserData data = GameEntry.Pool.DequeueClassObject<CusUserData>();

            CusUserDataAA data1 = GameEntry.Pool.DequeueClassObject<CusUserDataAA>();

            CusUserDataBB data2 = GameEntry.Pool.DequeueClassObject<CusUserDataBB>();

            CusUserDataCC data3 = GameEntry.Pool.DequeueClassObject<CusUserDataCC>();

            StartCoroutine(EnqueueClassObject(data));
            StartCoroutine(EnqueueClassObject(data1));
            StartCoroutine(EnqueueClassObject(data2));
            StartCoroutine(EnqueueClassObject(data3));

        }
    }
    private IEnumerator EnqueueClassObject(object obj)
    {
        yield return new WaitForSeconds(5.0f);
        GameEntry.Pool.EnqueueClassObject(obj);
    }
}
