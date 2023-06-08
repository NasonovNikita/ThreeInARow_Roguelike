using Battle;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(Modifier))]
    public class ModifierDrawer : PropertyDrawer
    {
        public static int n;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float space = EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight;
            n = 0;
            
            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.LabelField(position, label);
            position.y += space;

            EditorGUI.indentLevel++;


            EditorGUI.PropertyField(position, property.FindPropertyRelative("type"), new GUIContent("Type"));
            position.y += space;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("moves"), new GUIContent("Moves"));
            position.y += space;

            n += 2;

            if (property.FindPropertyRelative("type").enumValueIndex is not 2)
            {
                EditorGUI.PropertyField(position, property.FindPropertyRelative("value"), new GUIContent("value"));
                position.y += space;

                EditorGUI.PropertyField(position, property.FindPropertyRelative("funcAffect"), 
                    new GUIContent("Affected Func"));
                position.y += space;

                EditorGUI.PropertyField(position, property.FindPropertyRelative("statAffect"), 
                    new GUIContent("Affected Stat"));
                position.y += space;

                n += 3;
            }

            EditorGUI.indentLevel--;

            EditorGUILayout.Space(space * n);

            EditorGUI.EndProperty();
        }
    }
}