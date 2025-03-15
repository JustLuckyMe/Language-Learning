using UnityEngine;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class DataTracker : MonoBehaviour
{
    public static DataTracker Instance { get; private set; }

    private string participantID;
    private List<ResponseData> responseList = new List<ResponseData>();
    private float questionStartTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void StartQuestion()
    {
        questionStartTime = Time.time;
    }

    public void RecordResponse(string question, string correctAnswer, string selectedAnswer, string audioFile)
    {
        float responseTime = Time.time - questionStartTime;
        bool isCorrect = selectedAnswer == correctAnswer;

        ResponseData data = new ResponseData
        {
            participantID = participantID,
            questionText = question,
            correctAnswer = correctAnswer,
            selectedAnswer = selectedAnswer,
            responseTime = responseTime,
            isCorrect = isCorrect,
            recordedAudioFile = audioFile
        };

        responseList.Add(data);
    }

    public void ExportData()
    {
        string filePath = Application.persistentDataPath + "/SessionData_" + participantID + ".json";
        File.WriteAllText(filePath, JsonUtility.ToJson(responseList, true));
        Debug.Log("Data exported to: " + filePath);
    }
}