using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Question System/Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 5)]
    public string questionText;
    public AudioClip audioClip;

    public ItemSO[] questionChoices;
    public ItemSO correctAnswer;
}