using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonParty : MonoBehaviour
{
    public List<SaplingMonStats> party = new List<SaplingMonStats>();

    public bool AddMon(SaplingMonStats t_newMon)
    {
        if (party.Count >= 6)
        {
            return false;
        }

        party.Add(t_newMon);

        return true;
    }

    public void showMon()
    {

    }
}
