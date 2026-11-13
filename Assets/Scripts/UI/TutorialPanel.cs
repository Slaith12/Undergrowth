using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    public event Action Opened;
    public event Action Closed;

    [Header("Objects")]
    [SerializeField] TMP_Text mainText;
    [SerializeField] Button backButton;
    [SerializeField] Button nextButton;
    [SerializeField] TMP_Text pageCountText;

    [Header("Data")]
    [SerializeField] List<string> textPages;

    [HideInInspector] public int currentPageNum;

    private void Start()
    {
        SetPage(0);
    }

    public void SetPage(int pageNum)
    {
        mainText.text = textPages[pageNum];
        pageCountText.text = $"{pageNum+1}/{textPages.Count}";
        backButton.gameObject.SetActive(pageNum > 0);
        nextButton.gameObject.SetActive(pageNum < textPages.Count - 1);
        currentPageNum = pageNum;
    }

    public void AdvancePage()
    {
        SetPage(currentPageNum + 1);
    }

    public void GoBackAPage()
    {
        SetPage(currentPageNum - 1);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Closed?.Invoke();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        Opened?.Invoke();
    }
}
