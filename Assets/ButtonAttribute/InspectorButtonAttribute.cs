using UnityEngine;

namespace ButtonAttribute
{
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class InspectorButtonAttribute : PropertyAttribute
    {
        public string ButtonText { get; private set; }
        public System.Type[] ParameterTypes { get; private set; }

        public InspectorButtonAttribute(string buttonText, params System.Type[] parameterTypes)
        {
            ButtonText = buttonText;
            ParameterTypes = parameterTypes;
        }
    }
}