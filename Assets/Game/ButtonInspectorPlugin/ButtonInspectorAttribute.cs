using System;
using UnityEngine;

namespace ButtonInspectorPlugin
{
   [AttributeUsage(AttributeTargets.Method, Inherited = true)]
   public class ButtonInspectorAttribute : PropertyAttribute
   {
   }
}