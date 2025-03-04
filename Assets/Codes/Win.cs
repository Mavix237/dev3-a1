using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Win : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text dialogText;
    public GameObject dialogPanel;
    public GameObject avatar;
    public GameObject prize;
    
    [Header("Dialog Settings")]
    [TextArea(3, 10)]
    public List<string> dialogLines = new List<string>();
    
    private int currentLineIndex = 0;
    
    
    void Start()
    {
        if (prize != null)
        {
            prize.SetActive(false);
        }
        // Show first line if we have any
        if (dialogLines.Count > 0)
        {
            dialogPanel.SetActive(true);
            dialogText.text = dialogLines[0];
        }
        else
        {
            dialogPanel.SetActive(false);
        }
    }
    
    void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
            || Input.GetMouseButtonDown(0))
        {
            ShowNextLine();
        }
    }
    
    public void ShowNextLine()
    {
        currentLineIndex++;
        
        if (currentLineIndex < dialogLines.Count)
        {
            dialogText.text = dialogLines[currentLineIndex];
        }
        else
        {
            dialogPanel.SetActive(false);
            ShowPrize();
        }
    }
    
    public void SetNewDialog(List<string> newLines)
    {
        dialogLines = newLines;
        currentLineIndex = 0;
        
        if (dialogLines.Count > 0)
        {
            dialogPanel.SetActive(true);
            dialogText.text = dialogLines[0];
        }
        else
        {
            dialogPanel.SetActive(false);
        }
    }
    
    public void ShowPrize()
    {
        if (prize != null)
        {
            prize.SetActive(true);
        }
    }
}