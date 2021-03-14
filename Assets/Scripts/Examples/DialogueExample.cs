using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueExample : MonoBehaviour
{

    public DialogueData _firstDialogue;
    public List<DialogueData> _secondDialogue = new List<DialogueData>();

    public int _secondDialogueIndex;

    [SerializeField] private TextMeshProUGUI _tmproQuestion;
    [SerializeField] private TextMeshProUGUI _tmproAnswer;
    [SerializeField] private DialogueData _data;

    public int _questionIndex;
    public int _answerIndex;

    public void ButtonExample()
    {
        _questionIndex++;
        _answerIndex++;
        SetTexts();
    }

    public void GoToNextQuestion(int newIndex)
    {
        _questionIndex = newIndex;
        SetTexts();
    }

    private void SetTexts()
    {
        _tmproQuestion.SetText(_data._questions[_questionIndex]._text);

        _tmproAnswer.SetText(_data._questions[_questionIndex]._answer[_answerIndex]._text);
    }
}
