using System.Collections;
using System;

using System.Collections.Generic;
using UnityEngine;

namespace TamLee
{
    public class SocketEvent : IDisposable
    {
        public delegate void OnActionHandler(byte[] buffer);
        public Dictionary<ushort, List<OnActionHandler>> dic = new Dictionary<ushort, List<OnActionHandler>>();

        #region 增加监听
        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="key"></param>
        /// <param name="handler"></param>
        public void AddEventListener(ushort key, OnActionHandler handler)
        {
            List<OnActionHandler> lstHander = null;
            dic.TryGetValue(key, out lstHander);
            if (lstHander == null)
            {
                lstHander = new List<OnActionHandler>();
                dic[key] = lstHander;
            }
            lstHander.Add(handler);
        }
        #endregion

        #region 移除监听
        public void RemoveEventListener(ushort key, OnActionHandler handler)
        {
            List<OnActionHandler> lstHander = null;
            dic.TryGetValue(key, out lstHander);

            if (lstHander != null)
            {
                lstHander.Remove(handler);
                if (lstHander.Count == 0)
                {
                    dic.Remove(key);
                }
            }
        }
        #endregion

        #region 派发
        /// <summary>
        /// 派发
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userData"></param>
        public void Dispatch(ushort key, byte[] buffer)
        {
            List<OnActionHandler> lstHandler = null;
            dic.TryGetValue(key, out lstHandler);
            if (lstHandler != null)
            {
                int lstCount = lstHandler.Count;//获取集合数量 只调用一次
                for (int i = 0; i < lstCount; i++)
                {
                    OnActionHandler handler = lstHandler[i];
                    if (handler != null && handler.Target != null)
                    {
                        handler(buffer);
                    }
                }
            }
        }
        public void Dispatch(ushort key)
        {
            Dispatch(key, null);
        }

        #endregion

        public void Dispose()
        {
            dic.Clear();
        }
    }
}