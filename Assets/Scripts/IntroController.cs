using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{

    [SerializeField]
    private Button m_StartGameBtn;

    public UnityEvent OnStartGame = new UnityEvent();

    private void Start()
    {
        m_StartGameBtn.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        OnStartGame.Invoke();
    }


}
