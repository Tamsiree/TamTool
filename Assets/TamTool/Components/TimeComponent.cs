using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamLee
{
    public class TimeComponent : TamLeeBaseComponent, IUpdateComponent
    {
        public TimeManager m_TimeManager;

        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);
            m_TimeManager = new TimeManager();
        }

        /// <summary>
        /// 创建定时器
        /// </summary>
        /// <returns></returns>
        public TimeAction CreateTimeAction()
        {
            TimeAction action = GameEntry.Pool.DequeueClassObject<TimeAction>();
            return action;
        }

        public void OnUpdate()
        {
            m_TimeManager.OnUpdate();
        }
        internal void RegisterActiion(TimeAction action)
        {
            m_TimeManager.RegisterActiion(action);
        }
        internal void RemoveActiion(TimeAction action)
        {
            m_TimeManager.RemoveActiion(action);
            GameEntry.Pool.EnqueueClassObject(action);
        }
        public override void Shutdown()
        {
            m_TimeManager.Dispose();
        }
    }
}