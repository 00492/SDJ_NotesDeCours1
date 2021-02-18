using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bart/Dialogue")]
public class DialogueData : ScriptableObject
{
    public List<Question> _questions = new List<Question>();

    [System.Serializable]
    public struct Question
    {
        public string _text;
        public List<Answer> _answer;
    }

    [System.Serializable]
    public struct Answer
    {
        public string _text;
    }
}
