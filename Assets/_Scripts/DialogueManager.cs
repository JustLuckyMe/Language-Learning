using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public List<QuestionWrapper> questions = new List<QuestionWrapper>();
    public Transform[] typeASpawnPoints;
    public Transform[] typeBSpawnPoints;
    public GameObject answerButtonPrefab; // A UI Button prefab to use for answers
    public Transform answerParent; // Parent object in UI to hold buttons

    private QuestionWrapper currentQuestion;

    private void Start()
    {
        SpawnAnswers(0);
    }

    public void SpawnAnswers(int questionIndex)
    {
        currentQuestion = questions[questionIndex]; // Store the current question

        // Clear old buttons
        foreach (Transform child in answerParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentQuestion.questionChoices.Count; i++)
        {
            Transform spawnPoint = (i % 2 == 0) ? typeASpawnPoints[i % typeASpawnPoints.Length] : typeBSpawnPoints[i % typeBSpawnPoints.Length];

            GameObject buttonObject = Instantiate(answerButtonPrefab, spawnPoint.position, Quaternion.identity, answerParent);

            if (buttonObject == null)
            {
                Debug.LogError("Instantiated buttonObject is null!");
                return;
            }

            
            if (!buttonObject.TryGetComponent<AnswerButton>(out var answerButton))
            {
                Debug.LogError("AnswerButton component is missing on the instantiated buttonObject!", buttonObject);
                return;
            }

            answerButton.Setup(currentQuestion.questionChoices[i], currentQuestion.correctAnswer, this);
        }
    }

    public void CheckAnswer(ItemSO selectedItem)
    {
        if (selectedItem == currentQuestion.correctAnswer)
        {
            Debug.Log("Correct Answer!");
            // Add logic to proceed to the next question or give positive feedback
        }
        else
        {
            Debug.Log("Wrong Answer!");
            // Handle incorrect answers if needed
        }
    }
}

[System.Serializable]
public class QuestionWrapper
{
    public string questionText;
    public AudioClip audioClip;
    public List<ItemSO> questionChoices = new List<ItemSO>();
    public ItemSO correctAnswer;
}