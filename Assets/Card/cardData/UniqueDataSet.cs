using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UniqueDataSet
{
    public List<UniqueDataObject> set = new List<UniqueDataObject>();
    public UniqueDataObject GetExplicitStringByUniqueName(string name)
    {
        return set.Find(c => c.name == name);
    }
}
