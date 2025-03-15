using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetParticipantId : MonoBehaviour
{
    [SerializeField] private TMP_InputField idInputField;
    [SerializeField] private Button submitButton;

    private string participantID;

    #region
    public void SetParticipantID()
    {
        participantID = idInputField.text;
        Debug.Log("Participant ID set: " + participantID);
    }

    public void SetFieldsUnusable()
    {
        idInputField.interactable = false;
        submitButton.interactable = false;
    }

    public void MoveToNextQuestion(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }

    #endregion
}
