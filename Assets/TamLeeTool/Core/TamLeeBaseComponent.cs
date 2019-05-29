using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamLee
{
    public abstract class TamLeeBaseComponent : TamLeeComponent
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterBaseComponent(this);
        }
        public abstract void Shutdown();
    }
}