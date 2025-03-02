using UnityEngine;

namespace Core.EditorMisc
{
#if UNITY_EDITOR
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string ButtonName { get; private set; }

        public ButtonAttribute(string buttonName = null)
        {
            ButtonName = buttonName;
        }
    }
#endif
}