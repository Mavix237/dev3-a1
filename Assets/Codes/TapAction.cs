using System.Collections;
using UnityEngine;

public class TapAction : MonoBehaviour
{
    public string url;
    public int itemIndex;
    Animator anim;
    private SpawnItem spawnManager;
    void Start()
    {
        anim = GetComponent<Animator>();
        spawnManager = FindObjectOfType<SpawnItem>();
    }
    public void Tapped()
    {
        //reference to the animator trigger
        anim.SetTrigger("Tapped");
        StartCoroutine(Collect());
    }
    void OnMouseDown()
{
    Tapped();
}
    IEnumerator Collect(){
        yield return new WaitForSeconds(2);
        if (spawnManager != null)
        {
            spawnManager.ItemCollected(itemIndex);
        }
        Application.OpenURL(url);
        Destroy(gameObject);
    }

}
