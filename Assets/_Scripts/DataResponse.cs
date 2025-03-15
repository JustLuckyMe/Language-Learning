[System.Serializable]
public class ResponseData
{
    public string participantID;
    public string questionText;
    public string correctAnswer;
    public string selectedAnswer;
    public float responseTime;
    public bool isCorrect;
    public string recordedAudioFile; // Path to saved voice recording
}