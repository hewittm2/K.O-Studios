using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttacks : MonoBehaviour
{
    private FighterClass self;
    [Header("All Special Attack Methods")] [Space(5)]
    [Tooltip("0 = Apollo Selected, 1 = Character 2 Selected")]
    public bool[] characters;

    //Apollo
    [System.Serializable] public class ApolloSpecial
    { 
        public Vector3 knockback;
        public int damage;
        public int stunLength;
        public GameObject partEffect;

    } public ApolloSpecial apolloSpecial = new ApolloSpecial();
    //Second Character
    [System.Serializable]    public class Temper2
    {
        public int testVar;
        public int damage;
        public int stunLength;
        public GameObject partEffect;

    } public Temper2 temper2 = new Temper2();


    private void Start(){
        self = GetComponent<FighterClass>();
    }
  
    public void NeutralSA() //Neutral Special Attack
    {
        if (characters[0] == true)
            StartCoroutine(ApolloNSA());

    }
    public void ForwardSA() //Forward Special Attack
    {
        if (characters[0] == true)
            StartCoroutine(ApolloFSA());
    }
    public void BackSA() //Back Special Attack
    {
        if (characters[0] == true)
            StartCoroutine(ApolloBSA());
    }
    public void DownSA() //Down Special Attack
    {
        if (characters[0] == true)
            StartCoroutine(ApolloDSA());
    }
    public void JumpSA() //Jump Special Attack
    {
        if (characters[0] == true)
            StartCoroutine(ApolloJSA());
    }
    //Functions for all Characters Special Attacks
    //Apollo
    IEnumerator ApolloNSA() //Apollo Netural Special Attack
    {
        self.knockBack = apolloSpecial.knockback;
        yield return new WaitForSeconds(self.anim.GetCurrentAnimatorClipInfo(0).Length);
		//self.knockBack = 0;
    }
    IEnumerator ApolloFSA() //Apollo Forward Special Attack
    {
        self.knockBack = apolloSpecial.knockback;
        yield return new WaitForSeconds(self.anim.GetCurrentAnimatorClipInfo(0).Length);
        //self.knockBack = 0;
    }
    IEnumerator ApolloBSA() //Apollo Back Special Attack
    {
        self.knockBack = apolloSpecial.knockback;
        yield return new WaitForSeconds(self.anim.GetCurrentAnimatorClipInfo(0).Length);
        //self.knockBack = 0;
    }
    IEnumerator ApolloDSA() //Apollo Down Special Attack
    {
        self.knockBack = apolloSpecial.knockback;
        yield return new WaitForSeconds(self.anim.GetCurrentAnimatorClipInfo(0).Length);
        //self.knockBack = 0;
    }
    IEnumerator ApolloJSA() //Apollo Jump Special Attack
    {
        self.knockBack = apolloSpecial.knockback;
        yield return new WaitForSeconds(self.anim.GetCurrentAnimatorClipInfo(0).Length);
        //self.knockBack = 0;
    }
}
