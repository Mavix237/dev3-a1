using System.Collections;
using UnityEngine;

public class PrizeTap : MonoBehaviour
{
    public string url;
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    // This handles tap/click on the object itself
    void OnMouseDown()
    {
        Tapped();
    }
    
    public void Tapped()
    {
        // Trigger animation
        if (anim != null)
        {
            anim.SetTrigger("Tapped");
        }
        
        // Start coroutine for delayed URL opening
        StartCoroutine(OpenURLAfterDelay());
    }
    
    IEnumerator OpenURLAfterDelay()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Opening URL: " + url);
        Application.OpenURL(url);
        Destroy(gameObject);
    }
}