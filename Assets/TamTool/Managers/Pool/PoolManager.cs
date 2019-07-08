using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamLee
{
    public class PoolManager : IDisposable
    {
        public ClassObjectPool ClassObjectPool
        {
            private set;
            get;
        }
        public PoolManager()
        {
            ClassObjectPool = new ClassObjectPool();
        }

        /// <summary>
        /// 释放类对池
        /// </summary>
        public void ClearClassObjectPool()
        {
            ClassObjectPool.Clear();
        }

        public void Dispose()
        {
            ClassObjectPool.Dispose();
        }


    }
}
