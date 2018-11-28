using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectors : MonoBehaviour {

    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;

    public bool inputDelayed1;
    public bool inputDelayed2;
    public bool inputDelayed3;
    public bool inputDelayed4;

    public bool player1Ready;
    public bool player2Ready;
    public bool player3Ready;
    public bool player4Ready;


    public GameObject topLeft;
    public GameObject topMiddle;
    public GameObject topRight;
    public GameObject bottomLeft;
    public GameObject bottomMiddle;
    public GameObject bottomRight;

    private CharacterSelectManager csm;
    private Manager switchS;

    private void Start()
    {
        csm = GameObject.FindGameObjectWithTag("Manager").GetComponent<CharacterSelectManager>();
        switchS = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }

    void Update ()
    {
        //Player One
        if (player1Ready == false)
            PlayerOneMove();
        if (Input.GetButtonDown("A_1"))
        {
            PlayerOneSelect();
            player1Ready = true;
        }
        //Player Two
        if (player2Ready == false)
            PlayerTwoMove();
        if (Input.GetButtonDown("A_2"))
        {
            PlayerTwoSelect();
            player2Ready = true;
        }
        //Player Three
        if (player3Ready == false)
            PlayerThreeMove();
        if (Input.GetButtonDown("A_3"))
        {
            PlayerThreeSelect();
            player3Ready = true;
        }
        // Player Four
        if (player4Ready == false)
            PlayerFourMove();
        if (Input.GetButtonDown("A_4"))
        {
            PlayerFourSelect();
            player4Ready = true;
        }

        ReadyUP();

        if (player1Ready == true && player2Ready == true && player3Ready == true && player4Ready == true)
            switchS.ChangeScene("FighterTest");

    }
    // ------- Ready UP

    void ReadyUP()
    {
        if (Input.GetButtonDown("B_1"))
            player1Ready = false;

        if (Input.GetButtonDown("B_2"))
            player2Ready = false;

        if (Input.GetButtonDown("B_3"))
            player3Ready = false;

        if (Input.GetButtonDown("B_4"))
            player4Ready = false;
    }

    //---------------------------------------

    void PlayerOneSelect()
    {
        if (playerOne.transform.position == topLeft.transform.position)
        {
            csm.SetPlayerCharacter(1, topLeft.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerOne.transform.position == topMiddle.transform.position)
        {
            csm.SetPlayerCharacter(1, topMiddle.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerOne.transform.position == topRight.transform.position)
        {
            csm.SetPlayerCharacter(1, topRight.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerOne.transform.position == bottomLeft.transform.position)
        {
            csm.SetPlayerCharacter(1, bottomLeft.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerOne.transform.position == bottomMiddle.transform.position)
        {
            csm.SetPlayerCharacter(1, bottomMiddle.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerOne.transform.position == bottomRight.transform.position)
        {
            csm.SetPlayerCharacter(1, bottomRight.gameObject.GetComponent<CharacterHolder>().character);
        }
    }

    void PlayerTwoSelect()
    {
        if (playerTwo.transform.position == topLeft.transform.position)
        {
            csm.SetPlayerCharacter(2, topLeft.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerTwo.transform.position == topMiddle.transform.position)
        {
            csm.SetPlayerCharacter(2, topMiddle.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerTwo.transform.position == topRight.transform.position)
        {
            csm.SetPlayerCharacter(2, topRight.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerTwo.transform.position == bottomLeft.transform.position)
        {
            csm.SetPlayerCharacter(2, bottomLeft.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerTwo.transform.position == bottomMiddle.transform.position)
        {
            csm.SetPlayerCharacter(2, bottomMiddle.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerTwo.transform.position == bottomRight.transform.position)
        {
            csm.SetPlayerCharacter(2, bottomRight.gameObject.GetComponent<CharacterHolder>().character);
        }
    }

    void PlayerThreeSelect()
    {
        if (playerThree.transform.position == topLeft.transform.position)
        {
            csm.SetPlayerCharacter(3, topLeft.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerThree.transform.position == topMiddle.transform.position)
        {
            csm.SetPlayerCharacter(3, topMiddle.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerThree.transform.position == topRight.transform.position)
        {
            csm.SetPlayerCharacter(3, topRight.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerThree.transform.position == bottomLeft.transform.position)
        {
            csm.SetPlayerCharacter(3, bottomLeft.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerThree.transform.position == bottomMiddle.transform.position)
        {
            csm.SetPlayerCharacter(3, bottomMiddle.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerThree.transform.position == bottomRight.transform.position)
        {
            csm.SetPlayerCharacter(3, bottomRight.gameObject.GetComponent<CharacterHolder>().character);
        }
    }

    void PlayerFourSelect()
    {
        if (playerFour.transform.position == topLeft.transform.position)
        {
            csm.SetPlayerCharacter(4, topLeft.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerFour.transform.position == topMiddle.transform.position)
        {
            csm.SetPlayerCharacter(4, topMiddle.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerFour.transform.position == topRight.transform.position)
        {
            csm.SetPlayerCharacter(4, topRight.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerFour.transform.position == bottomLeft.transform.position)
        {
            csm.SetPlayerCharacter(4, bottomLeft.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerFour.transform.position == bottomMiddle.transform.position)
        {
            csm.SetPlayerCharacter(4, bottomMiddle.gameObject.GetComponent<CharacterHolder>().character);
        }
        if (playerFour.transform.position == bottomRight.transform.position)
        {
            csm.SetPlayerCharacter(4, bottomRight.gameObject.GetComponent<CharacterHolder>().character);
        }
    }

    // -------------------------------------------------------------------------------------
    void PlayerOneMove()
    {
        if (inputDelayed1 == false)
        {
            // Replace Inputs with controller inputs 
            if (Input.GetAxisRaw("L_YAxis_1") > 0)
            {
                MoveUp1();
                inputDelayed1 = true;
            }
            if (Input.GetAxisRaw("L_YAxis_1") < 0)
            {
                MoveDown1();
                inputDelayed1 = true;
            }
            if (Input.GetAxisRaw("L_XAxis_1") < 0)
            {
                MoveLeft1();
                inputDelayed1 = true;
            }
            if (Input.GetAxisRaw("L_XAxis_1") > 0)
            {
                MoveRight1();
                inputDelayed1 = true;
            }
        }
        else { StartCoroutine(OneWait()); }
    }

    void PlayerTwoMove()
    {
        if (inputDelayed2 == false)
        {
            // Replace Inputs with controller inputs 
            if (Input.GetAxisRaw("L_YAxis_2") > 0)
            {
                MoveUp2();
                inputDelayed2 = true;
            }
            if (Input.GetAxisRaw("L_YAxis_2") < 0)
            {
                MoveDown2();
                inputDelayed2 = true;
            }
            if (Input.GetAxisRaw("L_XAxis_2") < 0)
            {
                MoveLeft2();
                inputDelayed2 = true;
            }
            if (Input.GetAxisRaw("L_XAxis_2") > 0)
            {
                MoveRight2();
                inputDelayed2 = true;
            }
        }
        else { StartCoroutine(TwoWait()); }
    }

    void PlayerThreeMove()
    {
        if (inputDelayed3 == false)
        {
            // Replace Inputs with controller inputs 
            if (Input.GetAxisRaw("L_YAxis_3") > 0)
            {
                MoveUp3();
                inputDelayed3 = true;
            }
            if (Input.GetAxisRaw("L_YAxis_3") < 0)
            {
                MoveDown3();
                inputDelayed3 = true;
            }
            if (Input.GetAxisRaw("L_XAxis_3") < 0)
            {
                MoveLeft3();
                inputDelayed3 = true;
            }
            if (Input.GetAxisRaw("L_XAxis_3") > 0)
            {
                MoveRight3();
                inputDelayed3 = true;
            }
        }
        else { StartCoroutine(ThreeWait()); }
    }

    void PlayerFourMove()
    {
        if (inputDelayed4 == false)
        {
            // Replace Inputs with controller inputs 
            if (Input.GetAxisRaw("L_YAxis_4") > 0)
            {
                MoveUp4();
                inputDelayed4 = true;
            }
            if (Input.GetAxisRaw("L_YAxis_4") < 0)
            {
                MoveDown4();
                inputDelayed4 = true;
            }
            if (Input.GetAxisRaw("L_XAxis_4") < 0)
            {
                MoveLeft4();
                inputDelayed4 = true;
            }
            if (Input.GetAxisRaw("L_XAxis_4") > 0)
            {
                MoveRight4();
                inputDelayed4 = true;
            }
        }
        else { StartCoroutine(FourWait()); }
    }


    //------------------------------------------------------------------------------------------
    IEnumerator OneWait()
    {
        yield return new WaitForSeconds(0.05f);
        inputDelayed1 = false;
    }
    IEnumerator TwoWait()
    {
        yield return new WaitForSeconds(0.05f);
        inputDelayed2 = false;
    }
    IEnumerator ThreeWait()
    {
        yield return new WaitForSeconds(0.05f);
        inputDelayed3 = false;
    }
    IEnumerator FourWait()
    {
        yield return new WaitForSeconds(0.05f);
        inputDelayed4 = false;
    }

    void MoveUp1()
    {
        if (playerOne.transform.position == bottomMiddle.transform.position)
        {
            playerOne.transform.position = topMiddle.transform.position;
        }
        if (playerOne.transform.position == bottomLeft.transform.position)
        {
            playerOne.transform.position = topLeft.transform.position;
        }
        if (playerOne.transform.position == bottomRight.transform.position)
        {
            playerOne.transform.position = topRight.transform.position;
        }
    }
    void MoveDown1()
    {
        if (playerOne.transform.position == topMiddle.transform.position)
        {
            playerOne.transform.position = bottomMiddle.transform.position;
        }
        if (playerOne.transform.position == topLeft.transform.position)
        {
            playerOne.transform.position = bottomLeft.transform.position;
        }
        if (playerOne.transform.position == topRight.transform.position)
        {
            playerOne.transform.position = bottomRight.transform.position;
        }
    }

    void MoveRight1()
    {
        if (playerOne.transform.position == topLeft.transform.position)
        {
            playerOne.transform.position = topMiddle.transform.position;
            return;
        }
        if (playerOne.transform.position == topMiddle.transform.position)
        {
            playerOne.transform.position = topRight.transform.position;
            return;
        }
        if (playerOne.transform.position == bottomLeft.transform.position)
        {
            playerOne.transform.position = bottomMiddle.transform.position;
            return;
        }
        if (playerOne.transform.position == bottomMiddle.transform.position)
        {
            playerOne.transform.position = bottomRight.transform.position;
            return;
        }
    }

    void MoveLeft1()
    {
        if (playerOne.transform.position == topRight.transform.position)
        {
            playerOne.transform.position = topMiddle.transform.position;
            return;
        }
        if (playerOne.transform.position == topMiddle.transform.position)
        {
            playerOne.transform.position = topLeft.transform.position;
            return;
        }
        if (playerOne.transform.position == bottomRight.transform.position)
        {
            playerOne.transform.position = bottomMiddle.transform.position;
            return;
        }
        if (playerOne.transform.position == bottomMiddle.transform.position)
        {
            playerOne.transform.position = bottomLeft.transform.position;
            return;
        }
    }

    //Player Two
    void MoveUp2()
    {
        if (playerTwo.transform.position == bottomMiddle.transform.position)
        {
            playerTwo.transform.position = topMiddle.transform.position;
        }
        if (playerTwo.transform.position == bottomLeft.transform.position)
        {
            playerTwo.transform.position = topLeft.transform.position;
        }
        if (playerTwo.transform.position == bottomRight.transform.position)
        {
            playerTwo.transform.position = topRight.transform.position;
        }
    }
    void MoveDown2()
    {
        if (playerTwo.transform.position == topMiddle.transform.position)
        {
            playerTwo.transform.position = bottomMiddle.transform.position;
        }
        if (playerTwo.transform.position == topLeft.transform.position)
        {
            playerTwo.transform.position = bottomLeft.transform.position;
        }
        if (playerTwo.transform.position == topRight.transform.position)
        {
            playerTwo.transform.position = bottomRight.transform.position;
        }
    }

    void MoveRight2()
    {
        if (playerTwo.transform.position == topLeft.transform.position)
        {
            playerTwo.transform.position = topMiddle.transform.position;
            return;
        }
        if (playerTwo.transform.position == topMiddle.transform.position)
        {
            playerTwo.transform.position = topRight.transform.position;
            return;
        }
        if (playerTwo.transform.position == bottomLeft.transform.position)
        {
            playerTwo.transform.position = bottomMiddle.transform.position;
            return;
        }
        if (playerTwo.transform.position == bottomMiddle.transform.position)
        {
            playerTwo.transform.position = bottomRight.transform.position;
            return;
        }
    }

    void MoveLeft2()
    {
        if (playerTwo.transform.position == topRight.transform.position)
        {
            playerTwo.transform.position = topMiddle.transform.position;
            return;
        }
        if (playerTwo.transform.position == topMiddle.transform.position)
        {
            playerTwo.transform.position = topLeft.transform.position;
            return;
        }
        if (playerTwo.transform.position == bottomRight.transform.position)
        {
            playerTwo.transform.position = bottomMiddle.transform.position;
            return;
        }
        if (playerTwo.transform.position == bottomMiddle.transform.position)
        {
            playerTwo.transform.position = bottomLeft.transform.position;
            return;
        }
    }
    //Player Three
    void MoveUp3()
    {
        if (playerThree.transform.position == bottomMiddle.transform.position)
        {
            playerThree.transform.position = topMiddle.transform.position;
        }
        if (playerThree.transform.position == bottomLeft.transform.position)
        {
            playerThree.transform.position = topLeft.transform.position;
        }
        if (playerThree.transform.position == bottomRight.transform.position)
        {
            playerThree.transform.position = topRight.transform.position;
        }
    }
    void MoveDown3()
    {
        if (playerThree.transform.position == topMiddle.transform.position)
        {
            playerThree.transform.position = bottomMiddle.transform.position;
        }
        if (playerThree.transform.position == topLeft.transform.position)
        {
            playerThree.transform.position = bottomLeft.transform.position;
        }
        if (playerThree.transform.position == topRight.transform.position)
        {
            playerThree.transform.position = bottomRight.transform.position;
        }
    }

    void MoveRight3()
    {
        if (playerThree.transform.position == topLeft.transform.position)
        {
            playerThree.transform.position = topMiddle.transform.position;
            return;
        }
        if (playerThree.transform.position == topMiddle.transform.position)
        {
            playerThree.transform.position = topRight.transform.position;
            return;
        }
        if (playerThree.transform.position == bottomLeft.transform.position)
        {
            playerThree.transform.position = bottomMiddle.transform.position;
            return;
        }
        if (playerThree.transform.position == bottomMiddle.transform.position)
        {
            playerThree.transform.position = bottomRight.transform.position;
            return;
        }
    }

    void MoveLeft3()
    {
        if (playerThree.transform.position == topRight.transform.position)
        {
            playerThree.transform.position = topMiddle.transform.position;
            return;
        }
        if (playerThree.transform.position == topMiddle.transform.position)
        {
            playerThree.transform.position = topLeft.transform.position;
            return;
        }
        if (playerThree.transform.position == bottomRight.transform.position)
        {
            playerThree.transform.position = bottomMiddle.transform.position;
            return;
        }
        if (playerThree.transform.position == bottomMiddle.transform.position)
        {
            playerThree.transform.position = bottomLeft.transform.position;
            return;
        }
    }

    //Player Four
    void MoveUp4()
    {
        if (playerFour.transform.position == bottomMiddle.transform.position)
        {
            playerFour.transform.position = topMiddle.transform.position;
        }
        if (playerFour.transform.position == bottomLeft.transform.position)
        {
            playerFour.transform.position = topLeft.transform.position;
        }
        if (playerFour.transform.position == bottomRight.transform.position)
        {
            playerFour.transform.position = topRight.transform.position;
        }
    }
    void MoveDown4()
    {
        if (playerFour.transform.position == topMiddle.transform.position)
        {
            playerFour.transform.position = bottomMiddle.transform.position;
        }
        if (playerFour.transform.position == topLeft.transform.position)
        {
            playerFour.transform.position = bottomLeft.transform.position;
        }
        if (playerFour.transform.position == topRight.transform.position)
        {
            playerFour.transform.position = bottomRight.transform.position;
        }
    }

    void MoveRight4()
    {
        if (playerFour.transform.position == topLeft.transform.position)
        {
            playerFour.transform.position = topMiddle.transform.position;
            return;
        }
        if (playerFour.transform.position == topMiddle.transform.position)
        {
            playerFour.transform.position = topRight.transform.position;
            return;
        }
        if (playerFour.transform.position == bottomLeft.transform.position)
        {
            playerFour.transform.position = bottomMiddle.transform.position;
            return;
        }
        if (playerFour.transform.position == bottomMiddle.transform.position)
        {
            playerFour.transform.position = bottomRight.transform.position;
            return;
        }
    }

    void MoveLeft4()
    {
        if (playerFour.transform.position == topRight.transform.position)
        {
            playerFour.transform.position = topMiddle.transform.position;
            return;
        }
        if (playerFour.transform.position == topMiddle.transform.position)
        {
            playerFour.transform.position = topLeft.transform.position;
            return;
        }
        if (playerFour.transform.position == bottomRight.transform.position)
        {
            playerFour.transform.position = bottomMiddle.transform.position;
            return;
        }
        if (playerFour.transform.position == bottomMiddle.transform.position)
        {
            playerFour.transform.position = bottomLeft.transform.position;
            return;
        }
    }
}
