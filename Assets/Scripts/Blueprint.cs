using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint
{
    public string ItemName;

    public string Requirement1;
    public string Requirement2;

    public int Requirement1Amount;
    public int Requirement2Amount;

    public int numberOfRequirement;

    public Blueprint(string name, int reqNum, string R1, int R1Num, string R2, int R2Num)
    {
        ItemName = name;

        numberOfRequirement = reqNum;

        Requirement1 = R1;
        Requirement2 = R2;

        Requirement1Amount = R1Num;
        Requirement2Amount = R2Num;
    }

    
}
