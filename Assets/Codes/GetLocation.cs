using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetLocation : MonoBehaviour
{
    public TextMeshProUGUI output;
    public SpawnItem spawnItem;

    void Start()
    {
        StartCoroutine(StartGeoLoc());
    }

    IEnumerator StartGeoLoc()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            output.text = "location not enabled by user";
            yield break;
        }
        // Start service before querying location
        Input.location.Start(1, 1); // set the accuracy and change for an update to 1 meter

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            output.text = "Timed out";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            output.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            StartCoroutine(UpdateLoc());
        }
    }

    IEnumerator UpdateLoc()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            output.text = "Latitude: " + Input.location.lastData.latitude + "\nLongitude: " + Input.location.lastData.longitude;
            spawnItem.LocationCheck(Input.location.lastData.latitude, Input.location.lastData.longitude);
        }
    }
}