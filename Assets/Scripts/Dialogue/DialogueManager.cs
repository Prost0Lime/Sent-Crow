using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Text nameText;
    public Text answerText;

    public bool continueText;
    public bool startQuest;

    public int ndtqs; //номер диалога дл€ старта квеста (берЄтс€ автоматич)

    public Animator boxAnim;
    public Animator startAnim;

    public DialogueList DL;

    public Queue<string> sentences;
    public Queue<string> answers;

    public int numToSkipPastSentences;  // номер дл€ скипа завершЄнных реплик

    private void Start()
    {
        sentences = new Queue<string>();        //очередь из вопросов
        answers = new Queue<string>();          // очередь из ответов
        continueText = true;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        boxAnim.SetBool("boxOpen", true);
        startAnim.SetBool("startOpen", false);

        nameText.text = dialogue.name;
        sentences.Clear();
        answers.Clear();


        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (string answer in dialogue.answers)
        {
            answers.Enqueue(answer);
        }

        ReloadReplic();
    }

    public void ReloadReplic()      //метод удалени€ из очереди прошедших диалогов
    {
        if (numToSkipPastSentences != 0)
        {
            for (int i = 0; i < numToSkipPastSentences; i++)
            {
                if (numToSkipPastSentences != 0)
                { 
                    string sentence = sentences.Dequeue();
                    StopAllCoroutines();
                    StartCoroutine(TypeSentence(sentence));
                    DisplayNextAnswer();
                }
            }
        }
       
        if (startQuest == false)
        {
            DisplayControlSentence();
        }
    }

    public void DisplayControlSentence()
    {
        
        if (continueText == true)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            else if (sentences.Count == ndtqs)
            {
                continueText = false;
                startQuest = true;
            }

            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        string sentence = sentences.Dequeue();
        DL.SkipSentences();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        DisplayNextAnswer();     
    }

    public void DisplayNextAnswer()
    {
        string answer = answers.Dequeue();
        StartCoroutine(TypeAnswer(answer));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter1 in sentence.ToCharArray())
        {
            dialogueText.text += letter1;
            yield return null;
        }
    }

    IEnumerator TypeAnswer(string answer)
    {
        answerText.text = "";
        foreach (char letter2 in answer.ToCharArray())
        {
            answerText.text += letter2;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        boxAnim.SetBool("boxOpen", false);
    }

    public void DMDLDT()        //метод дл€ кнопки говорить
    {
        DL.SearchForTheRight();
    }

}