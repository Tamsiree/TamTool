using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamLee
{
    public class TimeManager : ManagerBase, IDisposable
    {
        /// <summary>
        /// 定时器链表
        /// </summary>
        private LinkedList<TimeAction> m_TimeActionList;

        public TimeManager()
        {
            m_TimeActionList = new LinkedList<TimeAction>();
        }

        internal void RegisterActiion(TimeAction action)
        {
            m_TimeActionList.AddLast(action);
        }
        internal void RemoveActiion(TimeAction action)
        {
            m_TimeActionList.Remove(action);
        }

        internal void OnUpdate()
        {
            for (LinkedListNode<TimeAction> curr = m_TimeActionList.First; curr != null; curr = curr.Next)
            {
                curr.Value.OnUpdate();
            }
        }

        public void Dispose()
        {
            m_TimeActionList.Clear();
        }
    }
}
