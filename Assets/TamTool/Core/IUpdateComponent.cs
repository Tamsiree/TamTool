using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamLee
{
    public interface IUpdateComponent
    {
        /// <summary>
        /// 更新方法
        /// </summary>
        void OnUpdate();

        /// <summary>
        /// 实例编号
        /// </summary>
        int InstanceId { get; }
    }
}