using System.Collections;
using TMPro;
using UnityEngine;

public class SelectButtonHighlight : MonoBehaviour
{ 
    TextMeshProUGUI selected_text;
    public Animator animator;

    void Start()
    {
        selected_text = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void HighlightSelectedButton()
    {
        
            selected_text.color = Color.red;
            selected_text.fontStyle = FontStyles.Bold;
            selected_text.fontSize +=5;
            StartCoroutine(MoveCanvasUp());

    }

       public void ResetButtonColors()
        {
                selected_text.color = Color.yellow;
                selected_text.fontStyle = FontStyles.Normal;
                selected_text.fontSize -=5;
                StartCoroutine(MoveCanvasDown());
    }

    IEnumerator MoveCanvasUp() 
    {
        animator.Play("MoveCanvasUp");
         yield return new WaitForSeconds(.1f);
    }
    IEnumerator MoveCanvasDown() {
        animator.Play("MoveCanvasDown");

            yield return new WaitForSeconds(.1f);
    }
}
