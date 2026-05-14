using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [Header("Player UI")]
    public GameObject playerSprite;

    public AudioController AudioController;


    private void Start()
    {
        
            StartCoroutine(StartDialogue());
        if (SceneManager.GetActiveScene().name == "DialogueScene")
        {
            //Play ringing
            StartCoroutine(AudioController.PhoneRinging());
        }
    }

    void Update()
    {
        if (speechBubble.transform.localScale.x > 0.6f )
        {
            playerSprite.SetActive(true);
            textBubble.enabled = true;
        }
        else
        {
            playerSprite.SetActive(false);
            textBubble.enabled = false;
        }

        if (SceneManager.GetActiveScene().name == "DialogueScene")
        {
            if (dialogueIndex >= dialogueText.Length && Input.GetMouseButtonUp(0))
            {
                StartCoroutine(LoadCutScene());
            }
        }

        if (SceneManager.GetActiveScene().name == "CutScene")
        {
            if (dialogueIndex >= dialogueText.Length && Input.GetMouseButtonUp(0))
            {
                StartCoroutine(LoadDungeon());
            }
        }

        

            TextStyle();
        
    }

    public IEnumerator LoadCutScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("CutScene");
    }

    public IEnumerator LoadDungeon() { 
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Dungeon_lvl1");
    }

    public void TextStyle() {
        if (SceneManager.GetActiveScene().name != "DialogueScene") return;
        if (dialogueIndex % 2 == 0 && dialogueIndex < 7)
        {
            if (dialogueIndex != 0 && dialogueIndex < 7) StartCoroutine(TypeWriterEffect(dialogueText[dialogueIndex]));

            //italicize dialogue text for phone sprite
            textBubble.fontStyle = FontStyles.Italic | FontStyles.Bold;
            phoneSprite.SetActive(true);
            playerSprite.SetActive(false);
        }
        else if (dialogueIndex % 2 != 0 || dialogueIndex > 7)
        {
            //normal dialogue text for player sprite
            textBubble.fontStyle = FontStyles.Normal;
            phoneSprite.SetActive(false);
            playerSprite.SetActive(true);
        }
    }

    public void DialogueAudio() {
        if (SceneManager.GetActiveScene().name != "DialogueScene") return;
        if (dialogueIndex % 2 == 0 && dialogueIndex < 7)
        {
            StartCoroutine(AudioController.AnonSpeak());
        }
        else
        {
            StartCoroutine(AudioController.PlayerSpeak());
        }
        if (dialogueIndex == 7)
        {
            StartCoroutine(AudioController.PhoneHangup());
        }
    }

    IEnumerator StartDialogue() {
        speechBubbleAnim.Play("Appear"); 
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator TypeWriterEffect(string dialogue) {
        textBubble.maxVisibleCharacters = 0;
        for (int i = 0; i < textBubble.text.Length; i++) {
            yield return new WaitForSeconds(typingSpeed);
            textBubble.maxVisibleCharacters ++;
            yield return new WaitForSeconds(typingSpeed);

        }
    }

    public void ChangeText(){

        if (dialogueIndex < dialogueText.Length)
        {
            textBubble.text = dialogueText[dialogueIndex];
            dialogueIndex++;
        }
    }
}
