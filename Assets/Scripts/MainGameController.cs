using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainGameController : MonoBehaviour
{

    [SerializeField]
    private CooldownEvent m_Timer;

    [SerializeField]
    private Button m_ScanBtn;

    [SerializeField]
    private Image m_TimerFG;

    private bool m_IsActive = false;

    public UnityEvent OnSwitchToScanScreen = new UnityEvent();

    private void Start()
    {
        m_Timer.Reset();
        m_TimerFG.fillAmount = 1f;
        m_ScanBtn.onClick.AddListener(SwitchToScanScreen);
    }

    private void OnEnable()
    {
        Activate();
    }

    private void Update()
    {
        if (m_IsActive)
        {
            m_Timer.Decrement(Time.deltaTime);

            m_TimerFG.fillAmount = m_Timer.GetRemainingNormalized();

            if (m_Timer.IsDone)
            {
                SwitchToScanScreen();
            }
        }
    }

    private void SwitchToScanScreen()
    {
        OnSwitchToScanScreen.Invoke();
    }

    public void Activate()
    {
        m_IsActive = true;
        m_Timer.Reset();
        m_TimerFG.fillAmount = 1f;
    }

}
