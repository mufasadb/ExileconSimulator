using UnityEditor;
using UnityEngine;
using System.Linq;
public class CardEditor : EditorWindow
{

    CardDataObject singleCard = new CardDataObject();
    UniqueDataObject uniqueObject = new UniqueDataObject();
    CardDataSet data;
    UniqueDataSet uniqueData;
    int physical;
    int life;
    int armour;
    int fire;
    int cold;
    int lightning;
    int chaos;
    int wild;
    int explicitPhysical;
    int explicitLife;
    int explicitArmour;
    int explicitFire;
    int explicitCold;
    int explicitLightning;
    int explicitChaos;
    int explicitWild;
    bool showForm = false;
    bool newCard = false;
    int editingInt;
    int uniqueEditingInt;
    bool detailedReporting = false;

    Vector2 scrollPos;
    [MenuItem("Window/Card Editor")]
    public static void ShowWindow()
    {
        GetWindow<CardEditor>("Card Editor");
    }
    void OnGUI()
    {
        LoadFromJson();

        int[] totalRateArray = new int[8];
        int[] totalMapRateArray = new int[8];
        GUILayout.Label("Sort By", EditorStyles.boldLabel);

        // data.set.Sort((c1, c2) => c1.tier.CompareTo(c2.tier));
        // data.set.Sort((c1, c2) => c1.type.CompareTo(c2.type));
        data.set = data.set.OrderBy(c => c.tier).ThenBy(c => c.type).ToList();
        foreach (var card in data.set)
        {
            // if (card.tier < 0) Debug.Log(card.name);
            // if (card.tier > 4) Debug.Log(card.name);

            totalRateArray[card.tier] += card.rate;
            // if (card.tier == 1)
            // {
            //     Debug.Log(totalRateArray[card.tier]);
            // }
            totalMapRateArray[card.tier] += card.mapRate;
        }
        // data.set.Sort((c1, c2) => c1.name.CompareTo(c2.name));

        GUILayout.Label("Data", EditorStyles.boldLabel);

        if (showForm)
        {
            singleCard.name = EditorGUILayout.TextField("name", singleCard.name);
            singleCard.type = (Type)EditorGUILayout.EnumFlagsField("Type", singleCard.type);
            singleCard.tier = EditorGUILayout.IntField("Tier", singleCard.tier);
            singleCard.rate = EditorGUILayout.IntField("rate", singleCard.rate);
            singleCard.mapRate = EditorGUILayout.IntField("Map Rate", singleCard.mapRate);
            singleCard.extraDescription = EditorGUILayout.TextField("extraDescription", singleCard.extraDescription);
            singleCard.isUnique = EditorGUILayout.Toggle("Is Unique", singleCard.isUnique);
            if (singleCard.isUnique)
            {

            }
            GUILayout.Label("", EditorStyles.boldLabel);
            GUILayout.Label("Implicit String", EditorStyles.boldLabel);
            physical = EditorGUILayout.IntField("physical", physical);
            life = EditorGUILayout.IntField("life", life);
            armour = EditorGUILayout.IntField("armour", armour);
            fire = EditorGUILayout.IntField("fire", fire);
            cold = EditorGUILayout.IntField("cold", cold);
            lightning = EditorGUILayout.IntField("lightning", lightning);
            chaos = EditorGUILayout.IntField("chaos", chaos);
            wild = EditorGUILayout.IntField("wild", wild);
            if (singleCard.isUnique)
            {
                GUILayout.Label("", EditorStyles.boldLabel);
                GUILayout.Label("Explicit String", EditorStyles.boldLabel);
                explicitPhysical = EditorGUILayout.IntField("physical", explicitPhysical);
                explicitLife = EditorGUILayout.IntField("life", explicitLife);
                explicitArmour = EditorGUILayout.IntField("armour", explicitArmour);
                explicitFire = EditorGUILayout.IntField("fire", explicitFire);
                explicitCold = EditorGUILayout.IntField("cold", explicitCold);
                explicitLightning = EditorGUILayout.IntField("lightning", explicitLightning);
                explicitChaos = EditorGUILayout.IntField("chaos", explicitChaos);
                explicitWild = EditorGUILayout.IntField("wild", explicitWild);

                GUILayout.Label("", EditorStyles.boldLabel);
                uniqueObject.extraDraws = EditorGUILayout.IntField("Extra Draw", uniqueObject.extraDraws);
                uniqueObject.extraTakes = EditorGUILayout.IntField("Extra Take", uniqueObject.extraTakes);
            }


            if (GUILayout.Button("Save")) { SaveCard(); SaveIntoJson(); }
            if (GUILayout.Button("Cancel")) { Cancel(); }
        }
        else
        {
            if (GUILayout.Button("Detailed Reporting")) { detailedReporting = !detailedReporting; }
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name", EditorStyles.label);
            GUILayout.Label("Tier", EditorStyles.label, GUILayout.Width(75));
            GUILayout.Label("Rate", EditorStyles.label, GUILayout.Width(75));
            if (detailedReporting) GUILayout.Label("% of Tier", EditorStyles.label, GUILayout.Width(150));
            GUILayout.Label("Map Rate", EditorStyles.label, GUILayout.Width(75));
            if (detailedReporting) GUILayout.Label("Map % of Tier", EditorStyles.label, GUILayout.Width(150));
            GUILayout.Label("Type", EditorStyles.label, GUILayout.Width(150));
            GUILayout.EndHorizontal();
            scrollPos =
            GUILayout.BeginScrollView(scrollPos);
            for (int i = 0; i < data.set.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(data.set[i].name)) { editingInt = i; showForm = true; newCard = false; LoadCard(i); }
                GUILayout.Label(data.set[i].tier.ToString(), EditorStyles.label, GUILayout.Width(75));
                GUILayout.Label(data.set[i].rate.ToString(), EditorStyles.label, GUILayout.Width(75));
                if (detailedReporting) GUILayout.Label((data.set[i].rate != 0) ? (data.set[i].rate * 100 / ((totalRateArray[data.set[i].tier + 1] * 0.2) + totalRateArray[data.set[i].tier])).ToString() : 0.ToString(), EditorStyles.label, GUILayout.Width(150));

                GUILayout.Label(data.set[i].mapRate.ToString(), EditorStyles.label, GUILayout.Width(75));

                if (detailedReporting) GUILayout.Label((data.set[i].mapRate != 0) ? (data.set[i].mapRate * 100 / ((totalMapRateArray[data.set[i].tier + 1] * 0.2) + totalMapRateArray[data.set[i].tier])).ToString() : 0.ToString(), EditorStyles.label, GUILayout.Width(150));

                GUILayout.Label(data.set[i].type.ToString(), EditorStyles.label, GUILayout.Width(150));
                GUILayout.EndHorizontal();


            }
            GUILayout.EndScrollView();
            // if (GUILayout.Button("Load")) { LoadFromJson(); }
            if (GUILayout.Button("New")) { singleCard = new CardDataObject(); showForm = true; ClearStats(); newCard = true; }
        }
    }
    private void Cancel()
    {
        showForm = false; singleCard = null;
    }
    private void ClearStats()
    {
        physical = 0;
        life = 0;
        armour = 0;
        fire = 0;
        cold = 0;
        lightning = 0;
        chaos = 0;
        wild = 0;
        explicitPhysical = 0;
        explicitLife = 0;
        explicitArmour = 0;
        explicitFire = 0;
        explicitCold = 0;
        explicitLightning = 0;
        explicitChaos = 0;
        explicitWild = 0;
        uniqueObject = new UniqueDataObject();
    }
    private void LoadCard(int i)
    {
        singleCard = data.set[i];
        ClearStats();
        StringToStats(singleCard.implicits);
        if (singleCard.isUnique)
        {
            uniqueObject = uniqueData.set.Find(c => c.name == singleCard.name);
            ExplicitStringToStats(uniqueObject.explicits);
            // ExplicitStringToStats(uniqueData.set.Find(c => c.name == singleCard.name).explicits);
        }
    }
    private void SaveCard()
    {
        singleCard.implicits = StatsToString();
        uniqueObject.explicits = ExplicitStatsToString();
        uniqueObject.name = singleCard.name;
        if (singleCard.rate == 0) singleCard.rate = 1000;
        if (singleCard.tier == 0) singleCard.tier = 2;
        if (newCard)
        {
            if (singleCard.isUnique)
            {
                uniqueData.set.Add(uniqueObject);
            }
            data.set.Add(singleCard);
        }
        else
        {
            data.set[editingInt] = singleCard;
            // uniqueData.set[editingInt] = singleCard;
            if (singleCard.isUnique)
            {
                int index = uniqueData.set.FindIndex(c => c.name == singleCard.name);
                uniqueData.set[index] = uniqueObject;
            }
        }
        Cancel();
    }
    public void LoadFromJson()
    {
        string jsonData = System.IO.File.ReadAllText(Application.dataPath + "/Card/cardData/cardData.json");
        string uniqueJsonData = System.IO.File.ReadAllText(Application.dataPath + "/Card/cardData/uniqueData.json");
        data = JsonUtility.FromJson<CardDataSet>(jsonData);
        uniqueData = JsonUtility.FromJson<UniqueDataSet>(uniqueJsonData);
        // data.set.Sort((c1, c2) => c1.type.CompareTo(c2.type));
        // data.set.Sort((c1, c2) => c1.tier.CompareTo(c2.tier));
        // singleCard = JsonUtility.FromJson<CardDataObject>(jsonData);
        // oneLoadeD = true;
        // StringToStats(singleCard.implicits);
    }
    public void SaveIntoJson()
    {
        Debug.Log("is saving");
        string saveData = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.dataPath + "/Card/cardData/cardData.json", saveData);
        string uniqueSaveData = JsonUtility.ToJson(uniqueData);
        System.IO.File.WriteAllText(Application.dataPath + "/Card/cardData/uniqueData.json", uniqueSaveData);
        AssetDatabase.ImportAsset("Assets/Card/cardData/cardData.json");
        AssetDatabase.ImportAsset("Assets/Card/cardData/uniqueData.json");
    }
    private string StatsToString()
    {
        string implicitString = "";
        for (int i = 0; i < physical; i++) { implicitString += "Physical,"; };
        for (int i = 0; i < life; i++) { implicitString += "Life,"; };
        for (int i = 0; i < armour; i++) { implicitString += "Armour,"; };
        for (int i = 0; i < fire; i++) { implicitString += "Fire,"; };
        for (int i = 0; i < cold; i++) { implicitString += "Cold,"; };
        for (int i = 0; i < lightning; i++) { implicitString += "Lightning,"; };
        for (int i = 0; i < chaos; i++) { implicitString += "Chaos,"; };
        for (int i = 0; i < wild; i++) { implicitString += "Wild,"; };
        if (implicitString.Length > 0)
        {
            implicitString.Remove(implicitString.Length - 1, 1);
        }
        return implicitString;
    }
    private string ExplicitStatsToString()
    {
        string implicitString = "";
        for (int i = 0; i < explicitPhysical; i++) { implicitString += "Physical,"; };
        for (int i = 0; i < explicitLife; i++) { implicitString += "Life,"; };
        for (int i = 0; i < explicitArmour; i++) { implicitString += "Armour,"; };
        for (int i = 0; i < explicitFire; i++) { implicitString += "Fire,"; };
        for (int i = 0; i < explicitCold; i++) { implicitString += "Cold,"; };
        for (int i = 0; i < explicitLightning; i++) { implicitString += "Lightning,"; };
        for (int i = 0; i < explicitChaos; i++) { implicitString += "Chaos,"; };
        for (int i = 0; i < explicitWild; i++) { implicitString += "Wild,"; };
        if (implicitString.Length > 0)
        {
            implicitString.Remove(implicitString.Length - 1, 1);
        }
        return implicitString;
    }
    private void StringToStats(string implicitString)
    {
        string[] stringArray = implicitString.Split(",");
        for (int i = 0; i < stringArray.Length; i++)
        {
            if (stringArray[i] == "Fire")
            {
                fire++;
            }
            if (stringArray[i] == "Cold")
            {
                cold++;
            }
            if (stringArray[i] == "Lightning")
            {
                lightning++;
            }
            if (stringArray[i] == "Physical")
            {
                physical++;
            }
            if (stringArray[i] == "Life")
            {
                life++;
            }
            if (stringArray[i] == "Armour")
            {
                armour++;
            }
            if (stringArray[i] == "Chaos")
            {
                chaos++;
            }
            if (stringArray[i] == "Wild")
            {
                wild++;
            }
        }
    }
    private void ExplicitStringToStats(string explicitString)
    {
        string[] stringArray = explicitString.Split(",");
        for (int i = 0; i < stringArray.Length; i++)
        {
            if (stringArray[i] == "Fire")
            {
                explicitFire++;
            }
            if (stringArray[i] == "Cold")
            {
                explicitCold++;
            }
            if (stringArray[i] == "Lightning")
            {
                explicitLightning++;
            }
            if (stringArray[i] == "Physical")
            {
                explicitPhysical++;
            }
            if (stringArray[i] == "Life")
            {
                explicitLife++;
            }
            if (stringArray[i] == "Armour")
            {
                explicitArmour++;
            }
            if (stringArray[i] == "Chaos")
            {
                explicitChaos++;
            }
            if (stringArray[i] == "Wild")
            {
                explicitWild++;
            }
        }
    }
}