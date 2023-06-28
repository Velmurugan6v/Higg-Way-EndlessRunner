using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoWork : MonoBehaviour
{
    public string[] studensName;
    // Start is called before the first frame update
    void Start()
    {
        PrintStudentName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintOddorEvenNumber(int lengh)
    {
        for (int i = 0; i < lengh; i++)
        {
            //Code for Odd number
            if (i % 2 != 0)
            {
                print(i + " is Odd number");
            }

            //Code for Even number
            if (i % 2 == 0)
            {
                print(i + " is Even number");
            }
        }
    }

    public void PrintStudentName()
    {
        /*foreach (string  studentName in studensName)
        {
            
        }*/
        for (int i = 0; i < studensName.Length; i++)
        {
            print(i + ". " + studensName[i]);
        }
    }
}
