using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinPicker : MonoBehaviour
{
    static CoinPicker instance;

    [SerializeField] int collectable = 0;

    [SerializeField] TMP_Text landedNuts;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        landedNuts.text = "" + collectable;
    }
    void IncreasePoints()
    {

        landedNuts.text = "" + collectable;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cannonball"))
        {
            collectable++;
            //landedNuts.text = "" + collectable;
            landedNuts.text = "" + collectable;
            Debug.Log("Cannonball:" + collectable);
            //collectable = collectable + 1;
            //Destroy(collision.gameObject);
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("nut"))
    //    {
    //        collectable++;
    //        landedNuts.text = "" + collectable;
    //        Debug.Log("Nut:" + collectable);
    //        //collectable = collectable + 1;
    //        //Destroy(collision.gameObject);
    //    }
    //}
}
