using System.Collections.Generic;
using System.Linq;
using Other;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(FieldReference))]
    public class FieldReferenceEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var fields = new List<string>(typeof(Globals).GetFields().Select(val => val.Name));

            int selected = EditorGUILayout.Popup(fields.IndexOf(serializedObject.FindProperty("currentField").stringValue), fields.ToArray());

            if (selected >= 0 && selected <= fields.Count)
            {
                serializedObject.FindProperty("currentField").stringValue = fields[selected];
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}