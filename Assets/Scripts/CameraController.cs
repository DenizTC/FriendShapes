using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private CooldownEvent m_Timer;

    [SerializeField]
    private Image m_TimerFG;

    [SerializeField]
    private WebCamTexture m_CamTexture;

    private Texture2D m_TargetTexture;

    [SerializeField]
    private Material m_TargetMat;

    [SerializeField]
    private string m_Path = "/";

    [SerializeField]
    private Image m_CamBG;

    private bool m_IsActive = false;

    public UnityEvent OnSwitchToValidationScreen = new UnityEvent();

    private void Start()
    {
        m_CamTexture = new WebCamTexture();
        m_CamBG.material.mainTexture = m_CamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to
        m_CamTexture.Play();
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
                m_IsActive = false;
                StartCoroutine(TakePhoto());
                //TakeSnapshotComplete();
            }
        }
    }

    private void Activate()
    {
        m_IsActive = true;
        m_Timer.Reset();
        m_TimerFG.fillAmount = 1f;
    }


    private IEnumerator TakePhoto()
    {
        yield return new WaitForEndOfFrame();

        // it's a rare case where the Unity doco is pretty clear,
        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
        // be sure to scroll down to the SECOND long example on that doco page 

        m_TargetTexture = new Texture2D(m_CamTexture.width, m_CamTexture.height);
        m_TargetTexture.SetPixels(m_CamTexture.GetPixels());
        m_TargetTexture.Apply();

        m_TargetMat.mainTexture = m_TargetTexture;

        //Encode to a PNG
        //byte[] bytes = photo.EncodeToPNG();
        ////Write out the PNG. Of course you have to substitute your_path for something sensible
        //File.WriteAllBytes(m_Path + "FriendShapes.png", bytes);

        TakeSnapshotComplete();
    }

    private void TakeSnapshotComplete()
    {
        OnSwitchToValidationScreen.Invoke();
        m_IsActive = false;

        m_CamTexture.Stop();
    }

}
