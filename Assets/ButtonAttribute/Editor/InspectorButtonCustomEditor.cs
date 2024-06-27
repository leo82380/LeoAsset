using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ButtonAttribute
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class InspectorButtonCustomEditor : UnityEditor.Editor
    {
        private object[] parameters;
        private System.Action<object[]> callback;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var monoBehaviour = (MonoBehaviour)target;
            var methods = monoBehaviour.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var buttonAttributes = method.GetCustomAttributes(typeof(InspectorButtonAttribute), true) as InspectorButtonAttribute[];
                if (buttonAttributes == null) continue;

                foreach (var buttonAttribute in buttonAttributes)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button(buttonAttribute.ButtonText))
                    {
                        // Initialize parameters if they are null or if the number of parameters has changed
                        if (parameters == null || parameters.Length != buttonAttribute.ParameterTypes.Length)
                            parameters = new object[buttonAttribute.ParameterTypes.Length];

                        // Show parameter fields
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            if (buttonAttribute.ParameterTypes[i] == typeof(int))
                                parameters[i] = EditorGUILayout.IntField((int)(parameters[i] ?? 0));
                            else if (buttonAttribute.ParameterTypes[i] == typeof(float))
                                parameters[i] = EditorGUILayout.FloatField((float)(parameters[i] ?? 0f));
                            else if (buttonAttribute.ParameterTypes[i] == typeof(string))
                                parameters[i] = EditorGUILayout.TextField((string)(parameters[i] ?? ""));
                            else if (buttonAttribute.ParameterTypes[i] == typeof(GameObject))
                                parameters[i] = EditorGUILayout.ObjectField((GameObject)parameters[i], typeof(GameObject), true);
                        }

                        // Show OK button to invoke the method with parameters
                        if (GUILayout.Button("OK", GUILayout.Width(80)))
                        {
                            if (callback == null)
                            {
                                callback = (object[] values) =>
                                {
                                    method.Invoke(monoBehaviour, values);
                                };
                            }

                            callback.Invoke(parameters);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }
}
