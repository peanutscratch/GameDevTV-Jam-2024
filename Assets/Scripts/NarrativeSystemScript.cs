using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NarrativeSystemScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    //public string[] character;
    public string[] lines;
    public float textSpeed;

    private int index;


    // Start is called before the first frame update
    void Start()
    {//on start, start dialogue
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {//on mouse click advance dialogue
        if(Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index]) 
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    { 
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine (TypeLine());
        }
       else {
            if (lines.Length > 0) {
                SceneManager.LoadScene("Dragon Fight Outcome");
            }
       }
        //{
         //   gameObject.SetActive(false);
       // }
    }
}
