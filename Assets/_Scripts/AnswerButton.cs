using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    private ItemSO assignedItem;
    private ItemSO correctAnswer;
    private DialogueManager dialogueManager;

    public Image itemImage;

    public void Setup(ItemSO item, ItemSO correct, DialogueManager manager)
    {
        if (item == null)
        {
            Debug.LogError("Setup Error: ItemSO (item) is NULL!", this);
            return;
        }

        if (correct == null)
        {
            Debug.LogError("Setup Error: CorrectAnswer ItemSO (correct) is NULL!", this);
            return;
        }

        if (manager == null)
        {
            Debug.LogError("Setup Error: DialogueManager (manager) is NULL!", this);
            return;
        }

        assignedItem = item;
        correctAnswer = correct;
        dialogueManager = manager;

        if (itemImage == null)
        {
            Debug.LogError("Setup Error: itemImage is NULL! Make sure to assign it in the prefab.", this);
            return;
        }

        itemImage.sprite = assignedItem.itemImage;
        GetComponent<Button>().onClick.AddListener(() => dialogueManager.CheckAnswer(assignedItem));
    }
}