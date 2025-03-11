using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(DialogueManager))]
public class DialogueManagerEditor : Editor
{
    private SerializedProperty questions;

    private void OnEnable()
    {
        questions = serializedObject.FindProperty("questions");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Display all fields **except** the custom-handled 'questions' list
        SerializedProperty property = serializedObject.GetIterator();
        bool enterChildren = true;

        while (property.NextVisible(enterChildren))
        {
            if (property.name != "questions") // Skip 'questions' since we handle it manually
            {
                EditorGUILayout.PropertyField(property, true);
            }
            enterChildren = false;
        }

        // Now manually handle the Questions List UI
        SerializedProperty questions = serializedObject.FindProperty("questions");

        EditorGUILayout.LabelField("Dialogue Questions", EditorStyles.boldLabel);
        for (int i = 0; i < questions.arraySize; i++)
        {
            SerializedProperty question = questions.GetArrayElementAtIndex(i);
            SerializedProperty questionText = question.FindPropertyRelative("questionText");
            SerializedProperty audioClip = question.FindPropertyRelative("audioClip");
            SerializedProperty choices = question.FindPropertyRelative("questionChoices");
            SerializedProperty correctAnswer = question.FindPropertyRelative("correctAnswer");

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.PropertyField(questionText, new GUIContent($"Question {i + 1}"));
            EditorGUILayout.PropertyField(audioClip, new GUIContent("Audio Clip"));

            EditorGUILayout.LabelField("Answer Choices");
            for (int j = 0; j < choices.arraySize; j++)
            {
                SerializedProperty choice = choices.GetArrayElementAtIndex(j);
                EditorGUILayout.PropertyField(choice, new GUIContent($"Choice {j + 1}"));
            }

            if (GUILayout.Button("Add Choice"))
            {
                choices.arraySize++;
            }

            if (choices.arraySize > 0 && GUILayout.Button("Remove Last Choice"))
            {
                choices.arraySize--;
            }

            EditorGUILayout.PropertyField(correctAnswer, new GUIContent("Correct Answer"));

            if (GUILayout.Button("Remove Question"))
            {
                questions.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Add New Question"))
        {
            questions.arraySize++;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
