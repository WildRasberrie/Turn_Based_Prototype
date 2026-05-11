using System.Collections;
using TMPro;
using UnityEngine;

public class SpeechBubbleText : MonoBehaviour
{
    [SerializeField] int dialogueIndex= 0;
    public TextMeshProUGUI textBubble;
    public GameObject speechBubble;
    public Animator speechBubbleAnim;
    public string[] dialogueText;
    public float typingSpeed = 0.5f;

    [Header("Phone UI Sprite")]
    public GameObject phoneSprite;
    [Header("Player UI Sprite")]
    public GameObject playerSprite;

    private void Start()
    {
        StartCoroutine(StartDialogue());
    }

    void Update()
    {
        //StartCoroutine(TypeWriterEffect(dialogueText[dialogueIndex]));

        if (speechBubble.transform.localScale.x > 0.6f )
        {
            textBubble.enabled = true;
        }
        else
        {
            textBubble.enabled = false;
        }

        TextStyle();
        
    }

    public void TextStyle() {
        if (dialogueIndex % 2 == 0)
        {
            //italicize dialogue text for phone sprite
            textBubble.fontStyle = FontStyles.Italic | FontStyles.Bold;
            phoneSprite.SetActive(true);
            playerSprite.SetActive(false);
        }
        else
        {
            //normal dialogue text for player sprite
            textBubble.fontStyle = FontStyles.Normal;
            phoneSprite.SetActive(false);
            playerSprite.SetActive(true);
        }
    }

    IEnumerator StartDialogue() {
        speechBubbleAnim.Play("Appear"); 
        yield return new WaitForSeconds(0.5f);

    }

    IEnumerator TypeWriterEffect(string dialogue) {
        textBubble.maxVisibleCharacters = 0;
        for (int i = 0; i < dialogue.Length; i++) { 
            textBubble.maxVisibleCharacters ++;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void ChangeText(){
        StartCoroutine(StartDialogue());

        if (dialogueIndex < dialogueText.Length)
        {
            textBubble.text = dialogueText[dialogueIndex];
            dialogueIndex++;
        }
    }
}
