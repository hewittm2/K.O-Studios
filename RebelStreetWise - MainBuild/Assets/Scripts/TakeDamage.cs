//Jake Poshepny
//10 31 18 (spooky)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public int health = 100;

    public int lightDamage = 5;
    public int mediumDamage = 10;
    public int heavyDamage = 20;

    public BoxCollider hurtHigh;
    public BoxCollider hurtMid;
    public BoxCollider hurtLow;

    public void DamageReceived(string attackType, string heightTag)
    {
        if (attackType == null)
        {
            return;
        }
        
        else if (attackType != null)
        {
            switch (attackType)
            {
                case "Light":
                    switch (heightTag)
                    {
                        case "High":
                            health -= lightDamage;
                            tag = null;
                            Debug.Log("Light - High");
                            break;

                        case "Mid":
                            health -= lightDamage;
                            tag = null;
                            Debug.Log("Light - Mid");
                            break;

                        case "Low":
                            health -= lightDamage;
                            tag = null;
                            Debug.Log("Light - Low");
                            break;
                    }
                    attackType = null;
                    break;

                case "Medium":
                    switch (heightTag)
                    {
                        case "High":
                            health -= mediumDamage;
                            tag = null;
                            Debug.Log("Medium - High");
                            break;

                        case "Mid":
                            health -= mediumDamage;
                            tag = null;
                            Debug.Log("Medium - Mid");
                            break;

                        case "Low":
                            health -= mediumDamage;
                            tag = null;
                            Debug.Log("Medium - Low");
                            break;
                    }
                    attackType = null;
                    break;

                case "Heavy":
                    switch (heightTag)
                    {
                        case "High":
                            health -= heavyDamage;
                            tag = null;
                            Debug.Log("Heavy - High");
                            break;

                        case "Mid":
                            health -= heavyDamage;
                            tag = null;
                            Debug.Log("Heavy - Mid");
                            break;

                        case "Low":
                            health -= heavyDamage;
                            tag = null;
                            Debug.Log("Heavy - Low");
                            break;
                    }
                    attackType = null;
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            //DamageReceived(other.gameObject.GetComponent<Attack>().)
        }
    }
}
