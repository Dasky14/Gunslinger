using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI m_gcScoreText = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    
    public void UpdateScoreText()
    {
        m_gcScoreText.text = GameManager.instance.m_iScore.ToString();
    }
}
