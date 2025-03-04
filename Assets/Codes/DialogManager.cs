using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text dialogText;
    public GameObject dialogPanel;
    public GameObject avatar;
    public GameObject tapToContinueText;
    
    
    [Header("Dialog Settings")]
    [TextArea(3, 10)]
    public List<string> dialogLines = new List<string>();
    
    private int currentLineIndex = 0;
    
    void Start()
    {
        // Show first line if we have any
        if (dialogLines.Count > 0)
        {
            dialogPanel.SetActive(true);
            dialogText.text = dialogLines[0];
            if (tapToContinueText != null)
            {
                tapToContinueText.SetActive(true);
            }
        }
        else
        {
            dialogPanel.SetActive(false);
        }
    }
    
    void Update()
    {
        // Check for tap on mobile
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
            || Input.GetMouseButtonDown(0)) // Also support mouse click for testing in editor
        {
            ShowNextLine();
        }
    }
    
    public void ShowNextLine()
    {
        currentLineIndex++;
        
        // Check if we have more lines to show
        if (currentLineIndex < dialogLines.Count)
        {
            dialogText.text = dialogLines[currentLineIndex];
            isLastLine = (currentLineIndex == dialogLines.Count - 1);
            if (tapToContinueText != null)
            {
                tapToContinueText.SetActive(!isLastLine);
            }
        }
        else
        {
            // No more lines, hide the dialog
            dialogPanel.SetActive(false);
            
            // Hide the bird
            if (avatar != null)
            {
                avatar.SetActive(false);
            }
            if (tapToContinueText != null)
            {
                tapToContinueText.SetActive(false);
            }
            
        }
    }
    
    // Call this when a new element is collected to change the dialog contents
    public void SetNewDialog(List<string> newLines)
    {
        dialogLines = newLines;
        currentLineIndex = 0;
        
        if (dialogLines.Count > 0)
        {
            dialogPanel.SetActive(true);
            dialogText.text = dialogLines[0];
            isLastLine = false;
            if (tapToContinueText != null)
            {
                tapToContinueText.SetActive(true);
            }
        }
        else
        {
            dialogPanel.SetActive(false);
        }
    }
    // Add to your DialogManager script
    private bool isLastLine = false;

    
    
}