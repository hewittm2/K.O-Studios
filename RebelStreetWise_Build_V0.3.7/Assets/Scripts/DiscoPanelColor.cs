using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoPanelColor : MonoBehaviour
{
    public List<Material> colors = new List<Material>();

    public float seconds = 0;
    public int start;

    private int c;

    private bool waiting = true;

    private IEnumerator Start()
    {
        c = start;
        
        yield return new WaitForSeconds(.1f);

        gameObject.GetComponent<Renderer>().material = colors[start];
        StartCoroutine(Wait());
    }

    private void Update()
    {
        if (!waiting)
        {
            if (c == colors.Count)
            {
                c = 1;
                gameObject.GetComponent<Renderer>().material = colors[c - 1];
                StartCoroutine(Wait());
            }
            else if (c < colors.Count)
            {
                c++;
                gameObject.GetComponent<Renderer>().material = colors[c - 1];
                StartCoroutine(Wait());
            }
        }
    }

    private IEnumerator Wait()
    {
        waiting = true;
        yield return new WaitForSeconds(seconds);
        waiting = false;
    }
}
