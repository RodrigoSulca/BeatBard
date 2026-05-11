using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Note))]
public class NoteDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(
        SerializedProperty property,
        GUIContent label)
    {
        SerializedProperty typeProp =
            property.FindPropertyRelative("type");

        Note.NoteType type =
            (Note.NoteType)typeProp.enumValueIndex;

        // Label + campos base
        float height = EditorGUIUtility.singleLineHeight * 4;

        // HoldTime extra
        if (type == Note.NoteType.Hold)
        {
            height += EditorGUIUtility.singleLineHeight;
        }

        // Espaciado
        height += 10f;

        return height;
    }

    public override void OnGUI(
        Rect position,
        SerializedProperty property,
        GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = 2f;

        Rect rect = new Rect(
            position.x,
            position.y,
            position.width,
            lineHeight);

        EditorGUI.LabelField(rect, label);

        SerializedProperty spawnTimeProp =
            property.FindPropertyRelative("spawnTime");

        SerializedProperty lineProp =
            property.FindPropertyRelative("line");

        SerializedProperty typeProp =
            property.FindPropertyRelative("type");

        SerializedProperty holdTimeProp =
            property.FindPropertyRelative("holdTime");

        // SpawnTime
        rect.y += lineHeight + spacing;
        EditorGUI.PropertyField(rect, spawnTimeProp);

        // Line
        rect.y += lineHeight + spacing;
        EditorGUI.PropertyField(rect, lineProp);

        // Type
        rect.y += lineHeight + spacing;
        EditorGUI.PropertyField(rect, typeProp);

        // HoldTime solo si es Hold
        Note.NoteType type =
            (Note.NoteType)typeProp.enumValueIndex;

        if (type == Note.NoteType.Hold)
        {
            rect.y += lineHeight + spacing;

            EditorGUI.PropertyField(rect, holdTimeProp);
        }

        EditorGUI.EndProperty();
    }
}