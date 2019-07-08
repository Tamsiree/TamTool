using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamLee
{
    /// <summary>
    /// TamLee基类
    /// </summary>
    public class TamLeeComponent : MonoBehaviour
    {
        /// <summary>
        /// 组件实例编号
        /// </summary>
        private int m_InstaceId;
        private void Awake()
        {
            m_InstaceId = GetInstanceID();
            OnAwake();
        }
        public int InstanceId
        {
            get { return m_InstaceId; }
        }
        protected virtual void OnAwake()
        {

        }
    }
}