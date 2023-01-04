using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBracos : MonoBehaviour
{
    bool subir=true, descer;
    public Transform transform_;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }



    void Update()
    {
        animator.SetBool("braco", true);
        //if (descer)
        //{
        //    transform.position -= new Vector3(0.0f, 0.005f * Time.deltaTime, 0);
        //    StartCoroutine(espera());
        //}

        //if (subir)
        //{
        //    transform.position += new Vector3(0.0f, 0.005f * Time.deltaTime, 0);

        //    StartCoroutine(espera1());

        //}
    }

    //IEnumerator espera()
    //{
    //    yield return new WaitForSeconds(2f);
    //    subir = true;
    //    descer = false;
    //}

    //IEnumerator espera1()
    //{
    //    yield return new WaitForSeconds(2f);
    //    subir = false;
    //    descer = true;
    //}

}
