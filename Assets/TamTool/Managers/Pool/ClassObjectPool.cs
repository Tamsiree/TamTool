using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamLee
{
    /// <summary>
    /// ������
    /// </summary>
    public class ClassObjectPool : IDisposable
    {
        /// <summary>
        /// ������ڳ��еĳ�פ����
        /// </summary>
        public Dictionary<int, byte> ClassObjectCount
        {
            get;
            private set;
        }
        /// <summary>
        /// �������ֵ�
        /// </summary>
        private Dictionary<int, Queue<object>> m_ClassObjectPoolDic;

#if UNITY_EDITOR
        /// <summary>
        /// �ڼ��������ʾ����Ϣ
        /// </summary>
#endif
        public Dictionary<Type, int> InspectorDic = new Dictionary<Type, int>();

        public ClassObjectPool()
        {
            ClassObjectCount = new Dictionary<int, byte>();
            m_ClassObjectPoolDic = new Dictionary<int, Queue<object>>();
        }
        #region SetResideCount �����ೣפ����
        /// <summary>
        /// �����ೣפ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        public void SetResideCount<T>(byte count) where T : class
        {
            int key = typeof(T).GetHashCode();
            ClassObjectCount[key] = count;
        }
        #endregion
        #region Dequeue ȡ��һ������
        /// <summary>
        /// ȡ��һ������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Dequeue<T>() where T : class, new()
        {
            lock (m_ClassObjectPoolDic)
            {
                //���ҵ�������͵Ĺ�ϣ
                int key = typeof(T).GetHashCode();

                Queue<object> queue = null;
                m_ClassObjectPoolDic.TryGetValue(key, out queue);

                if (queue == null)
                {
                    queue = new Queue<object>();
                    m_ClassObjectPoolDic[key] = queue;
                }
                ///��ʼ��ȡ����
                if (queue.Count > 0)
                {
                    ///˵�������������õ�
                    Debug.Log("���� " + key + "���ڣ����л�ȡ");
                    object obj = queue.Dequeue();
#if UNITY_EDITOR
                    Type t = obj.GetType();
                    if (InspectorDic.ContainsKey(t))
                    {
                        InspectorDic[t]--;
                    }
                    else
                    {
                        InspectorDic[t] = 0;
                    }
#endif 

                    return (T)obj;
                }
                else
                {
                    //���������û�� ��ʵ����һ��
                    Debug.Log("���� " + key + "�����ڣ�ʵ����һ��");
                    return new T();
                }
            }
        }
        #endregion

        #region Enqueue ����س�
        /// <summary>
        /// ���󷵳�
        /// </summary>
        /// <param name="obj"></param>
        public void Enqueue(object obj)
        {
            lock (m_ClassObjectPoolDic)
            {
                int key = obj.GetType().GetHashCode();
                Debug.Log("���� " + key + "�س���");
                Queue<object> queue = null;
                m_ClassObjectPoolDic.TryGetValue(key, out queue);


#if UNITY_EDITOR
                Type t = obj.GetType();
                if (InspectorDic.ContainsKey(t))
                {
                    InspectorDic[t]++;
                }
                else
                {
                    InspectorDic[t] = 1;
                }
#endif 
                if (queue != null)
                {
                    queue.Enqueue(obj);
                }
            }
        }
        #endregion

        /// <summary>
        /// �ͷŶ����
        /// </summary>
        public void Clear()
        {
            lock (m_ClassObjectPoolDic)
            {
                Debug.Log("�ͷŶ������");
                List<int> lst = new List<int>(m_ClassObjectPoolDic.Keys);
                int lstCount = lst.Count;
                int queueCount = 0;
                for (int i = 0; i < lstCount; i++)
                {
                    int key = lst[i];
                    //�õ�����
                    Queue<object> queue = m_ClassObjectPoolDic[key];
#if UNITY_EDITOR
                    Type t = null;
#endif
                    queueCount = queue.Count;

                    ///�û��ͷŵ�ʱ�� �ж�
                    byte residentCount = 0;
                    ClassObjectCount.TryGetValue(key, out residentCount);
                    while (queueCount > residentCount)
                    {
                        //�������п��ͷŵĶ���
                        queueCount--;
                        object obj = queue.Dequeue();//�Ӷ�����ȡ��һ�����������û���κ����ã��ͱ����Ұָ��,�ȴ�gc����
#if UNITY_EDITOR
                        t = obj.GetType();
                        InspectorDic[t]--;
#endif
                    }
                    if (queueCount == 0)
                    {
                        if (residentCount == 0)
                        {
                            m_ClassObjectPoolDic[key] = null;
                            m_ClassObjectPoolDic.Remove(key);
                        }
#if UNITY_EDITOR
                        if (t != null)
                        {
                            InspectorDic.Remove(t);
                        }
#endif
                    }
                }
                //GC ������Ŀ�У���һ������
                GC.Collect();
            }
        }

        public void Dispose()
        {
            m_ClassObjectPoolDic.Clear();
        }
    }
}