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

    public void DamageReceived(string attackType, string tag)
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
                    switch (tag)
                    {
                        case "High":
                            health -= lightDamage;
                            tag = null;
                            break;

                        case "Mid":
                            health -= lightDamage;
                            tag = null;
                            break;

                        case "Low":
                            health -= lightDamage;
                            tag = null;
                            break;
                    }
                    attackType = null;
                    break;

                case "Medium":
                    switch (tag)
                    {
                        case "High":
                            health -= mediumDamage;
                            tag = null;
                            break;

                        case "Mid":
                            health -= mediumDamage;
                            tag = null;
                            break;

                        case "Low":
                            health -= mediumDamage;
                            tag = null;
                            break;
                    }
                    attackType = null;
                    break;

                case "Heavy":
                    switch (tag)
                    {
                        case "High":
                            health -= heavyDamage;
                            tag = null;
                            break;

                        case "Mid":
                            health -= heavyDamage;
                            tag = null;
                            break;

                        case "Low":
                            health -= heavyDamage;
                            tag = null;
                            break;
                    }
                    attackType = null;
                    break;
            }
        }
    }
}
