using System.Linq;
using Other;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(FieldReference))]
    public class FieldReferenceEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fromPromised"));

            if (serializedObject.FindProperty("fromPromised").boolValue)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("promisedObject"));
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("obj"));
            
            serializedObject.ApplyModifiedProperties();
            
            FieldReference reference = (FieldReference)target;
            
            if (reference.obj == null) return;

            #region choosing component
            
            var componentNames = reference.obj.GetComponents<Component>().Select(val => val.GetType().Name).ToList();

            int currentComponent = componentNames.IndexOf(serializedObject.FindProperty("component").stringValue);
            currentComponent = currentComponent == -1 ? 0 : currentComponent;

            int chosenComponent = EditorGUILayout.Popup(currentComponent, componentNames.ToArray());

            serializedObject.FindProperty("component").stringValue = componentNames[chosenComponent];
            
            #endregion

            serializedObject.ApplyModifiedProperties();
            
            if (reference.component == "") return;
            
            #region choosing field
            
            var fieldNames = reference.obj.GetComponent(reference.component)
                .GetType().GetFields().Select(val => val.Name).ToList();
            
            if (fieldNames.Count == 0) return;

            int currentField = fieldNames.IndexOf(serializedObject.FindProperty("field").stringValue);
            currentField = currentField == -1 ? 0 : currentField;

            int chosenField = EditorGUILayout.Popup(currentField, fieldNames.ToArray());

            serializedObject.FindProperty("field").stringValue = fieldNames[chosenField];

            #endregion

            serializedObject.ApplyModifiedProperties();
        }
    }
}