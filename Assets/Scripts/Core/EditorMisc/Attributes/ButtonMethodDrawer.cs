using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Core.EditorMisc
{
#if UNITY_EDITOR
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ButtonMethodDrawer : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        
            var targetObject = target as MonoBehaviour;

            if (targetObject != null)
            {
                var methods = targetObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        
                foreach (var method in methods)
                {
                    var buttonAttributes = method.GetCustomAttributes(typeof(ButtonAttribute), true);
            
                    if (buttonAttributes.Length > 0)
                    {
                        var buttonAttribute = (ButtonAttribute)buttonAttributes[0];
                        string buttonName = string.IsNullOrEmpty(buttonAttribute.ButtonName) ? method.Name : buttonAttribute.ButtonName;
                
                        if (GUILayout.Button(buttonName))
                        {
                            method.Invoke(targetObject, null);
                        }
                    }
                }
            }
        }
    }
#endif
}