using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public List<QuestionWrapper> questions = new List<QuestionWrapper>();
    public Transform[] typeASpawnPoints;  // Type A spawn points
    public Transform[] typeBSpawnPoints;  // Type B spawn points

    public GameObject correctAnswerPrefab;
    public GameObject wrongAnswerPrefab;

    private void Start()
    {
        SpawnAnswers(0);
    }

    public void SpawnAnswers(int questionIndex)
    {
        QuestionWrapper question = questions[questionIndex];

        for (int i = 0; i < question.questionChoices.Count; i++)
        {
            Transform spawnPoint = (i % 2 == 0) ? typeASpawnPoints[i % typeASpawnPoints.Length] : typeBSpawnPoints[i % typeBSpawnPoints.Length];

            GameObject prefabToSpawn = (question.questionChoices[i] == question.correctAnswer) ? correctAnswerPrefab : wrongAnswerPrefab;

            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
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