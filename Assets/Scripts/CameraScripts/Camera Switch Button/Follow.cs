using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject Target = null;
    public Transform Pos1;
    public Transform Pos2;
    public Transform Pos3;
    public Transform Pos4;
    //public Transform Pos5;

    public GameObject T = null;
    public float speed = 1.5f;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        //Find cannon by target
        Target = GameObject.FindGameObjectWithTag("Player");
        //Find target by tag
        T = GameObject.FindGameObjectWithTag("Target");

        //Find position game objects by tag
        Pos1 = GameObject.FindGameObjectWithTag("Pos1").GetComponent<Transform>();
        Pos2 = GameObject.FindGameObjectWithTag("Pos2").GetComponent<Transform>();
        Pos3 = GameObject.FindGameObjectWithTag("Pos3").GetComponent<Transform>();
        Pos4 = GameObject.FindGameObjectWithTag("Pos4").GetComponent<Transform>();
        //Pos5 = GameObject.FindGameObjectWithTag("Pos5").GetComponent<Transform>();

        //Index will call saved position number from Players Position 
        index = PlayerPrefs.GetInt("save");
    }

    // Update is called once per frame
    void Update()
    {
        //If index is bigger than 2 then switch  index to 0 to load the First Camera Position
        if (index > 2)
        {
            index = 0;
        }

        if (index < 0)
        {
            index = 2;
        }
    }
    private void FixedUpdate()
    {
        this.transform.LookAt(Target.transform);
        float car_Move = Mathf.Abs(Vector3.Distance(this.transform.position, T.transform.position) * speed);
        this.transform.position = Vector3.MoveTowards(this.transform.position, T.transform.position, car_Move * Time.deltaTime);

        //If index equals 0, the target(T) will move to the Position 1
        if (index == 0)
        {
            T.transform.position = Pos1.position;
        }

        //If index equals 1, the target(T) will move to the Position 2
        if (index == 1)
        {
            T.transform.position = Pos2.position;
        }

        //If index equals 2, the target(T) will move to the Position 3
        if (index == 2)
        {
            T.transform.position = Pos3.position;
        }

        //If index equals 3, the target(T) will move to the Position 4
        if (index == 3)
        {
            T.transform.position = Pos4.position;
        }

        //If index equals 4, the target(T) will move to the Position 5
        if (index == 4)
        {
            //T.transform.position = Pos5.position;
        }
    }

    public void Next() //Switch camera position using UI Button
    {
        index++;
        PlayerPrefs.SetInt("save", index); //PlayerPrefs Set int will save the current index value
        PlayerPrefs.Save();
        Debug.Log("Next Camera");
    }
}
