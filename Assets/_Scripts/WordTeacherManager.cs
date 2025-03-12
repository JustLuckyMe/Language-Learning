using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class WordTeacherManager : MonoBehaviour
{
    [Header("Word Teaching Settings")]
    public ItemSO[] ListOfWords; // Array of words to teach
    public int currentIndex = 0;

    [Header("UI Elements")]
    public Image wordImage;
    public Text wordText;
    public Button continueButton;
    public Text feedbackText;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioSource recordingSource; // Separate AudioSource for playback of the recorded voice

    [Header("Recording Settings")]
    public string microphoneName = null;
    private AudioClip recordedClip;
    private bool isRecording = false;

    [Header("Scene Transition")]
    public string nextScene; // Name of the scene to load when all words are taught

    private void Start()
    {
        continueButton.onClick.AddListener(ContinueToNextWord);
        StartTeaching();
    }

    private void StartTeaching()
    {
        if (currentIndex < ListOfWords.Length)
        {
            ShowWord();
            StartCoroutine(PlayTeachingSequence());
        }
        else
        {
            EndTeaching();
        }
    }

    private void ShowWord()
    {
        ItemSO currentWord = ListOfWords[currentIndex];
        wordImage.sprite = currentWord.itemImage;
        wordText.text = currentWord.itemName;
        feedbackText.text = "";
    }

    private IEnumerator PlayTeachingSequence()
    {
        ItemSO currentWord = ListOfWords[currentIndex];

        // Play the word's audio
        audioSource.clip = currentWord.itemAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 0.5f);

        // Prompt the player to repeat
        feedbackText.text = "Now repeat after me...";
        StartRecording();
    }

    private void StartRecording()
    {
        if (Microphone.devices.Length > 0)
        {
            microphoneName = Microphone.devices[0];
            recordedClip = Microphone.Start(microphoneName, false, 2, 44100);
            isRecording = true;
            Invoke("StopRecording", 2f); // Automatically stop after 2 seconds
        }
        else
        {
            feedbackText.text = "No microphone detected!";
        }
    }

    private void StopRecording()
    {
        if (!isRecording) return;

        isRecording = false;
        Microphone.End(microphoneName);

        feedbackText.text = "Good job!";
        recordingSource.clip = recordedClip;

        // Enable the continue button
        continueButton.gameObject.SetActive(true);
    }

    private void ContinueToNextWord()
    {
        currentIndex++;
        continueButton.gameObject.SetActive(false);
        StartTeaching();
    }

    private void EndTeaching()
    {
        SceneManager.LoadScene(nextScene);
    }
}