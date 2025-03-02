using UnityEditor;
using UnityEngine;

namespace Core.EditorMisc.Attributes
{ 
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public class ButtonDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 0;
        }
    }
#endif
}