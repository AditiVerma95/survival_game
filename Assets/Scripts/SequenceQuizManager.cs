using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SequenceQuizManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI solutionText;
    public TextMeshProUGUI scoreText;

    public Button[] optionButtons;
    public TextMeshProUGUI[] optionTexts;

    [System.Serializable]
    public class QuizData
    {
        public string question;
        public string[] options;
        public int[] correctSequence;
        public string explanation;
    }

    [Header("Quiz Data")]
    public List<QuizData> quizList;

    private int currentQuestionIndex = 0;
    private List<int> selectedSequence = new List<int>();
    private float timeRemaining = 30f;
    private bool isAnswering = false;
    private int score = 0;

    void Start()
    {
        LoadQuestion();
    }

    void Update()
    {
        if (isAnswering)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();

            if (timeRemaining <= 0)
            {
                isAnswering = false;
                StartCoroutine(HandleWrongAnswer());
            }
        }
    }

    void LoadQuestion()
    {
        isAnswering = true;
        timeRemaining = 30f;
        selectedSequence.Clear();
        solutionText.gameObject.SetActive(false);

        QuizData q = quizList[currentQuestionIndex];
        questionText.text = q.question;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i;
            optionTexts[i].text = q.options[i];
            optionButtons[i].interactable = true;
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnOptionClicked(index));
        }

        UpdateScoreText();
    }

    void OnOptionClicked(int index)
    {
        if (selectedSequence.Contains(index)) return;

        selectedSequence.Add(index);
        optionTexts[index].text = (selectedSequence.Count) + " -> " + quizList[currentQuestionIndex].options[index];
        optionButtons[index].interactable = false;

        if (selectedSequence.Count == optionButtons.Length)
        {
            isAnswering = false;
            CheckAnswer();
        }
    }

    void CheckAnswer()
    {
        bool isCorrect = true;

        // Convert correct sequence from 1-based to 0-based
        var correct = new List<int>();
        foreach (int i in quizList[currentQuestionIndex].correctSequence)
        {
            correct.Add(i - 1);
        }

        for (int i = 0; i < correct.Count; i++)
        {
            if (selectedSequence[i] != correct[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
            StartCoroutine(HandleCorrectAnswer());
        else
            StartCoroutine(HandleWrongAnswer());
    }


    IEnumerator HandleCorrectAnswer()
    {
        score += 1;
        questionText.text = "Correct!";
        UpdateScoreText();
        yield return new WaitForSeconds(3f);
        NextQuestion();
    }

    IEnumerator HandleWrongAnswer()
    {
        questionText.text = "Incorrect!";
        solutionText.gameObject.SetActive(true);
        solutionText.text = "Solution: " + quizList[currentQuestionIndex].explanation;
        yield return new WaitForSeconds(30f);
        NextQuestion();
    }

    void NextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex < quizList.Count)
        {
            LoadQuestion();
        }
        else
        {
            StartCoroutine(EndQuiz());
        }
    }

    IEnumerator EndQuiz()
    {
        questionText.text = "🎉 Quiz Complete!";
        solutionText.gameObject.SetActive(true);
        solutionText.text = "Your final score: " + score + "/" + quizList.Count;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

    public void GoBack() {
        SceneManager.LoadScene(0);
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score + "/" + quizList.Count;
    }
}
