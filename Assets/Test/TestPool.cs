using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TamLee;
/// <summary>
/// 回对象池前或者使用前必须重置，不然数据会和使用前一样
/// 不能使用带参数的构造函数，可以使用Init()方法作为初始化方法来传递数据
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
            //sbr.Length = 0;//重置，不然会和上个对象数据一样
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
