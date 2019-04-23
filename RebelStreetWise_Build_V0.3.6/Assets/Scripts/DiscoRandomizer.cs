using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoRandomizer : MonoBehaviour
{
    public List<DiscoPanelColor> discoPanels = new List<DiscoPanelColor>();

    private void Start()
    {
        foreach (DiscoPanelColor dpc in discoPanels)
        {
            dpc.start = Random.Range(0, 5);
        }
    }
}
