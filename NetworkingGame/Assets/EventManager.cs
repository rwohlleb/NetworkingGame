using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class EventManager : MonoBehaviour
    {
        public delegate void ClickAction();
        public static event ClickAction TargetDestroyed;

        public static void TargetClicked()
        {
            if(TargetDestroyed != null)
            {
                TargetDestroyed();
            }
        }
    }
}
