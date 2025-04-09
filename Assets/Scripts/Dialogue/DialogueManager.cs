using Dialogue.Data;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : SingletonBehaviour<DialogueManager>
{
    [Header("Text Elements")]
    public TextMeshProUGUI Nameplate;
    public Image NameplateBG; 
    public TextMeshProUGUI MainText;

    [Header("References")]
    [SerializeField] private Camera UICam;
    [SerializeField] private InputController input;

    [SerializeField] private DialogueElement currentElement;
    [Space(), Header("Flags")]
    public bool NodeHasCompleted;
    public bool CanContinue;
    public bool QuickFinishWriting;

    protected override void Awake()
    {
        base.Awake();
    }

    public void OnContinue()
    {
        if (!CanContinue)
        {
            QuickFinishWriting = true;
            return;
        }
        NodeHasCompleted = true;
    }

    public void EnableDialogueControl() {
        UICam.enabled = true;
        input.actions.UI.Enable();
        input.actions.Player.Disable();
    }
    public void DisableDialogueControl() {
        UICam.enabled = false;
        input.actions.UI.Disable();
        input.actions.Player.Enable();
    }

    public void DoDialogue(DialogueData dialogue) => StartCoroutine(DialogueLoop(dialogue));
    private IEnumerator DialogueLoop(DialogueData dialogue) {
        DialogueElement element = dialogue.GetStartingElement();
        while (element != null) {
            NodeHasCompleted = false;
            element.OnEnter(this);

            while (!NodeHasCompleted)
                yield return new WaitForEndOfFrame();

            element = element.GetNextElement(dialogue);
        }
        //Wait for final node
        NodeHasCompleted = false;
        CanContinue = true; 
        while (!NodeHasCompleted)
            yield return new WaitForEndOfFrame();
        DisableDialogueControl();
    }
    public void ShowSentence(Sentence sentence) {
        Nameplate.text = sentence.Speaker.Name;
        NameplateBG.color = sentence.Speaker.NameColor;
        StartCoroutine(WriteText(sentence.Text, sentence.Speaker));
    }
    private IEnumerator WriteText(string text, Speaker speaker) {
        CanContinue = false; 
        MainText.text = "";
        float delay = speaker.SpeechSpeed / text.Length;
        for (int i = 0; i < text.Length; i++) {
            MainText.text += text[i];
            if(!QuickFinishWriting)
                yield return new WaitForSeconds(delay + Random.Range(-(delay * speaker.SpeechSpeedWiggle), delay * speaker.SpeechSpeedWiggle));
        }
        QuickFinishWriting = false;
        CanContinue = true; 
        
    }
}
