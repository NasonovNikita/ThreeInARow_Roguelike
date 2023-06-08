using Battle;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ActiveAction))]
    public class ActionDrawer : PropertyDrawer
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
            n++;

            switch (property.FindPropertyRelative("type").enumValueIndex)
            {
                case 0:
                    EditorGUI.PropertyField(position, property.FindPropertyRelative("stunMoves"), 
                        new GUIContent("Moves"));
                    break;
                case 1:
                    EditorGUI.PropertyField(position, property.FindPropertyRelative("value"), 
                        new GUIContent("Damage"));
                    break; 
            }

            position.y += space;
            n++;

            EditorGUI.indentLevel--;

            EditorGUILayout.Space(space * n);
            
            EditorGUI.EndProperty();
        }
    }
}