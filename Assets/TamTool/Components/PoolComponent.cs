using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamLee
{
    public class PoolComponent : TamLeeBaseComponent, IUpdateComponent
    {
        public PoolManager PoolManager
        {
            get;
            private set;
        }
        protected override void OnAwake()
        {
            base.OnAwake();
            PoolManager = new PoolManager();
            GameEntry.RegisterUpdateComponent(this);

            m_NextRuntime = Time.time;
        }

        public void SetClassObjectResident<T>(byte count) where T : class
        {
            PoolManager.ClassObjectPool.SetResideCount<T>(count);
        }

        public T DequeueClassObject<T>() where T : class, new()
        {
            return PoolManager.ClassObjectPool.Dequeue<T>();
        }

        public void EnqueueClassObject(object obj)
        {
            PoolManager.ClassObjectPool.Enqueue(obj);
        }

        public override void Shutdown()
        {
            PoolManager.Dispose();
        }
        /// <summary>
        /// 释放间隔
        /// </summary>
        [SerializeField]
        public int m_ClearInterval = 30;

        private float m_NextRuntime = 0f;

        public void OnUpdate()
        {
            if (Time.time > m_NextRuntime + m_ClearInterval)
            {
                //该释放了
                m_NextRuntime = Time.time;
                PoolManager.ClearClassObjectPool();//释放类对象池
            }
        }
    }
}
