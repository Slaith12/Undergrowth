using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [Space]
    [SerializeField] TMP_Text text;
    [SerializeField] Button button;

    /// <summary>
    /// Displays the dialogue on screen. Also activates the button to advance dialogue.
    /// </summary>
    public void DisplayDialogue(string dialogue)
    {
        text.text = dialogue;
        button.gameObject.SetActive(true);
        ShowDialogue();
    }

    public void HideDialogue()
    {
        //nothing on this object uses the update function right now, so this is a good enough way to handle this
        gameObject.SetActive(false);
    }

    public void ShowDialogue()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Event handler for the Advance button. Advances the dialogue and disables the advance button.
    /// </summary>
    public void OnAdvanceButtonPressed()
    {
        button.gameObject.SetActive(false);
        dialogueManager.AdvanceDialogue();
    }
}
