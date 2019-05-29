using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamLee
{
    public class UIComponent : TamLeeBaseComponent, IUpdateComponent
    {


        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);
        }
        public void OnUpdate()
        {
            //Debug.Log("UI界面");
        }

        public override void Shutdown()
        {
            //Debug.Log("UI关闭");
        }
    }
}
