using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ButtonInspectorPlugin
{
   [CustomEditor(typeof(MonoBehaviour), true)]
   public class ButtonInspectorAttributeEditor : Editor
   {
      public override void OnInspectorGUI()
      {
         // Рисуем стандартный инспектор
         DrawDefaultInspector();

         // Находим методы с атрибутом Button
         var methods = target.GetType()
                             .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                             .Where(m => m.GetCustomAttributes(typeof(ButtonInspectorAttribute), true).Length > 0);

         foreach (var method in methods)
         {
            // Подпись кнопки (по имени метода)
            string buttonLabel = ObjectNames.NicifyVariableName(method.Name);
            if (GUILayout.Button(buttonLabel))
            {
               method.Invoke(target, null);
               // Для инспектора: обновить отображение, если что-то обновилось
               EditorUtility.SetDirty(target);
            }
         }
      }
   }
}