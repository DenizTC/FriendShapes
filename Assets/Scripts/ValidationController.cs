using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ValidationController : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI m_Text;

    [SerializeField]
    private Slider m_ScoreSlider;

    [SerializeField]
    private Button m_PlayAgainBtn;

    [SerializeField]
    private Material m_ScoreMat;

    [SerializeField]
    private Color m_ColorBad;

    [SerializeField]
    private Color m_ColorGood;

    private void Start()
    {
        m_ScoreSlider.onValueChanged.AddListener(UpdateScore);
        m_PlayAgainBtn.onClick.AddListener(RestartGame);
        UpdateScore(0);
    }

    private void UpdateScore(float n)
    {
        m_Text.text = ((int) m_ScoreSlider.value).ToString();
        m_ScoreMat.color = Color.Lerp(m_ColorBad, m_ColorGood, n / 10f);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
