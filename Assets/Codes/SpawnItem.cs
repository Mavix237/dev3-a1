using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class SpawnItem : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI winText;
    public GameObject[] itemPrefabs;
    public Vector3[] Pos;
    public AudioClip[] elementSounds;
    public AudioSource audioSource;
    public Vector2[] Locations;
    
    bool[] wasCollected;
    
    void Start() {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
            
        // Initialize the collection tracking array
        wasCollected = new bool[itemPrefabs.Length];
        print("wasCollected: " + wasCollected[0]);
        
        // Make sure win text is hidden at start
        if (winText != null) {
            winText.gameObject.SetActive(false);
        }
    }
    
    public void Spawn(int itemNum) {
        string[] welcomeOut = {
            "Santa Monica: \nThe water element appears as a shimmering blue crystal near the lake's edge, pulsing with the rhythm of the fountain waters.",
            "Iovine and Young Academy: \nThe sky's the limit!",
            "Pershing Square @ DTLA: \nThe earth element takes the form of a small floating rock formation with moss and tiny flowers growing on it, hovering near the garden areas.",
            "Echo Part: \nThe fire element manifests as a glowing orb, floating near the fountain light displays at the park's center."
        };
        titleText.text = welcomeOut[itemNum];
        
        // Clear existing items
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        foreach (GameObject collectible in collectibles) {
            Destroy(collectible);
        }
        
        // Play sound for this element
        if (elementSounds != null && itemNum < elementSounds.Length && elementSounds[itemNum] != null) {
            audioSource.clip = elementSounds[itemNum];
            audioSource.Play();
        }
        
        // Spawn the new item and set its itemIndex
        GameObject newItem = Instantiate(itemPrefabs[itemNum], Pos[itemNum], Quaternion.identity);
        
        // Set the item index on the TapAction component if it exists
        TapAction tapAction = newItem.GetComponent<TapAction>();
        if (tapAction != null) {
            tapAction.itemIndex = itemNum;
        }
    }
    
    public void LocationCheck(float lat, float lon) {
        Vector2 myLocation = new(lat, lon);
        for (int i = 0; i < Locations.Length; i++) {
            // If it hasn't been collected and the distance is within range - spawn it
            if (!wasCollected[i] && Vector2.Distance(myLocation, Locations[i]) < .00008f) {
                Spawn(i);
            }
        }
    }
    
    bool CheckWin() {
        // Check if ALL elements have been collected
        for (int i = 0; i < wasCollected.Length; i++) {
            if (!wasCollected[i]) {
                return false;
            }
        }
        return true;
    }
    
    public void ButtonSpawn(int itemNum) {
        if (!wasCollected[itemNum]) {
            // Spawn the item but don't mark as collected yet
            Spawn(itemNum);
            
            // Gray out button to show it was visited
            UnityEngine.UI.Button clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<UnityEngine.UI.Button>();
            if (clickedButton != null) {
                var colors = clickedButton.colors;
                colors.normalColor = new Color(0.7f, 0.7f, 0.7f, 0.5f);
                clickedButton.colors = colors;
                
                TextMeshProUGUI buttonText = clickedButton.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null) {
                    buttonText.color = new Color(0.5f, 0.5f, 0.5f, 0.7f);
                }
            }
        } else {
            titleText.text = "You've already collected this element!";
        }
    }
    
        public void ItemCollected(int itemNum) {
    // Mark as collected
    wasCollected[itemNum] = true;
    
    // Check if all elements are collected
    if (CheckWin()) {
        // Option 1: Load the Win scene immediately
        // SceneManager.LoadScene("Win");
        
        // Option 2: Show win message briefly, then load the Win scene
        StartCoroutine(LoadWinScene(2.0f));
        
        // Show win text temporarily
        if (winText != null) {
            winText.gameObject.SetActive(true);
            winText.text = "Congratulations! You've collected all the elements!";
        }
        
        // Clear description text
        if (titleText != null) {
            titleText.text = "";
        }
        
        // Remove any remaining objects
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        foreach (GameObject collectible in collectibles) {
            Destroy(collectible);
        }
        
        // Play victory sound if available
        if (audioSource != null && elementSounds.Length > itemPrefabs.Length) {
            audioSource.clip = elementSounds[itemPrefabs.Length];
            audioSource.Play();
        }
    }
}

    private IEnumerator LoadWinScene(float delay) {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);
        
        // Load the Win scene
        SceneManager.LoadScene("Win");
    }
}