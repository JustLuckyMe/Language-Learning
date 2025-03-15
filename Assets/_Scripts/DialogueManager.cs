using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public List<QuestionWrapper> questions = new List<QuestionWrapper>();
    public Transform[] typeASpawnPoints;
    public Transform[] typeBSpawnPoints;
    public GameObject answerButtonPrefab;
    public Transform answerParent;

    private QuestionWrapper currentQuestion;
    [SerializeField] DataTracker tracker;

    private void Start()
    {
        SpawnAnswers(0);
    }

    public void SpawnAnswers(int questionIndex)
    {
        currentQuestion = questions[questionIndex];

        // Clear old buttons
        foreach (Transform child in answerParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentQuestion.questionChoices.Count; i++)
        {
            Transform spawnPoint = (i % 2 == 0) ? typeASpawnPoints[i % typeASpawnPoints.Length] : typeBSpawnPoints[i % typeBSpawnPoints.Length];

            GameObject buttonObject = Instantiate(answerButtonPrefab, spawnPoint.position, Quaternion.identity, answerParent);

            AnswerButton answerButton = buttonObject.GetComponent<AnswerButton>();
            if (answerButton != null)
            {
                answerButton.Setup(currentQuestion.questionChoices[i], currentQuestion.correctAnswer, this);
            }
            else
            {
                Debug.LogError("SpawnAnswers Error: AnswerButton component is missing on the instantiated prefab!", buttonObject);
            }
        }
    }


    public void CheckAnswer(ItemSO selectedItem)
    {
        if (selectedItem == currentQuestion.correctAnswer)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    #region Animation

    private void WrongAnswer()
    {
        Debug.Log("Wrong Answer!");

    }

    private void CorrectAnswer()
    {
        Debug.Log("Correct Answer!");

    }

    #endregion
}

[System.Serializable]
public class QuestionWrapper
{
    public string questionText;
    public AudioClip audioClip;
    public List<ItemSO> questionChoices = new List<ItemSO>();
    public ItemSO correctAnswer;
}