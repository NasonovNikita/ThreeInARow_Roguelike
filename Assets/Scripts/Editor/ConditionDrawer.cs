using Battle;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(Condition))]
    public class ConditionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            
            float space = EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight;
            int n = 0;
        
            EditorGUI.BeginProperty(position, label, property);

            property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(position, property.isExpanded, label);
            position.y += space;

            EditorGUI.indentLevel++;

            if (property.isExpanded)
            {
                EditorGUI.PropertyField(position, property.FindPropertyRelative("modOrAction"),
                    new GUIContent("Mod/Action"));
                position.y += space;

                n++;
                
                switch (property.FindPropertyRelative("modOrAction").enumValueIndex)
                {
                    case 0:
                        EditorGUI.PropertyField(position, property.FindPropertyRelative("mod"), new GUIContent("Mod"));
                        position.y += space * ModifierDrawer.n;
                        n += ModifierDrawer.n;
                        break;
                    case 1:
                        EditorGUI.PropertyField(position, property.FindPropertyRelative("action"), new GUIContent("Action"));
                        position.y += space * ActionDrawer.n;
                        n += ActionDrawer.n;
                        break;
                }

                position.y += space;

                EditorGUI.PropertyField(position, property.FindPropertyRelative("condType"), new GUIContent("Type"));
                position.y += space;

                EditorGUI.PropertyField(position, property.FindPropertyRelative("unitType"),
                    new GUIContent("Unit Type"));
                position.y += space;
                
                n += 2;
                
                switch (property.FindPropertyRelative("condType").enumValueIndex)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2 or 3:
                        EditorGUI.PropertyField(position, property.FindPropertyRelative("statType"), new GUIContent("Stat Type"));
                        position.y += space;

                        EditorGUI.PropertyField(position, property.FindPropertyRelative("compareMethod"), 
                            new GUIContent("Compare Method"));
                        position.y += space;

                        EditorGUI.PropertyField(position, property.FindPropertyRelative("value"), 
                            new GUIContent("Value"));
                        
                        position.y += space;
                        n += 3;
                        break;
                    case 4 or 5:
                        EditorGUI.PropertyField(position, property.FindPropertyRelative("compareMethod"),
                            new GUIContent("Compare Method"));
                        position.y += space;

                        EditorGUI.PropertyField(position, property.FindPropertyRelative("value"),
                            new GUIContent("Value"));
                        
                        position.y += space;
                        n += 2;
                        break;
                }

                EditorGUI.PropertyField(position, property.FindPropertyRelative("targetType"),
                    new GUIContent("Target Type"));
                position.y += space;

                n++;
            }
            
            EditorGUI.EndFoldoutHeaderGroup();

            EditorGUI.indentLevel--;

            EditorGUILayout.Space(space * n);
            
            EditorGUI.EndProperty();
        }
    }
}