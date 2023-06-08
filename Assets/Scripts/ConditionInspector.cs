using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ConditionInspectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float h = EditorGUIUtility.singleLineHeight;
            int n = 0;
        
            EditorGUI.BeginProperty(position, label, property);
        
            EditorGUI.LabelField(position, label);
            position.y += position.height;
        
            EditorGUI.PropertyField(position, property.FindPropertyRelative("type"), new GUIContent("Type"));
            position.y += position.height;
        
            n++;
        
            if (property.FindPropertyRelative("type").enumValueIndex == 0)
            {
                EditorGUI.PropertyField(position, property.FindPropertyRelative("gem"), new GUIContent("Gem"));
                position.y += position.height;
            
                n++;
            }
            else if (property.FindPropertyRelative("type").enumValueIndex == 1)
            {
                EditorGUI.PropertyField(position, property.FindPropertyRelative("unit"), new GUIContent("UnitType"));
                position.y += position.height;

                EditorGUI.PropertyField(position, property.FindPropertyRelative("stat"), new GUIContent("Stat"));
                position.y += position.height;
            
                n += 2;
            }

            if (property.FindPropertyRelative("type").enumValueIndex != 2)
            {
                EditorGUI.PropertyField(position, property.FindPropertyRelative("compare"), new GUIContent("Compare"));
                position.y += position.height;

                n++;
            }

            EditorGUI.PropertyField(position, property.FindPropertyRelative("value"), new GUIContent("Value"));
            position.y += position.height;
        
            n++;
        
            EditorGUILayout.Space(h * n);
        
            EditorGUI.EndProperty();
        }
    }
}