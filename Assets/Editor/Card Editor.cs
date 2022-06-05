using UnityEditor;
using UnityEngine;

public class CardEditor : EditorWindow
{

    CardDataObject singleCard = new CardDataObject();
    CardDataSet data;
    int physical;
    int life;
    int armour;
    int fire;
    int cold;
    int lightning;
    int chaos;
    int wild;
    bool showForm = false;
    bool newCard = false;
    int editingInt;
    [MenuItem("Window/Card Editor")]
    public static void ShowWindow()
    {
        GetWindow<CardEditor>("Card Editor");
    }
    void OnGUI()
    {
        LoadFromJson();


        GUILayout.Label("Sort By", EditorStyles.boldLabel);

        // data.set.Sort((c1, c2) => c1.tier.CompareTo(c2.tier));
        // data.set.Sort((c1, c2) => c1.type.CompareTo(c2.type));
        data.set.Sort((c1, c2) => c1.name.CompareTo(c2.name));

        GUILayout.Label("Data", EditorStyles.boldLabel);

        if (showForm)
        {
            singleCard.name = EditorGUILayout.TextField("name", singleCard.name);
            singleCard.type = (Type)EditorGUILayout.EnumFlagsField("Type", singleCard.type);
            singleCard.tier = EditorGUILayout.IntField("Tier", singleCard.tier);
            singleCard.isUnique = EditorGUILayout.Toggle("Is Unique", singleCard.isUnique);
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


            if (GUILayout.Button("Save")) { SaveCard(); SaveIntoJson(); }
            if (GUILayout.Button("Cancel")) { Cancel(); }
        }
        else
        {
            for (int i = 0; i < data.set.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(data.set[i].name)) { editingInt = i; showForm = true; newCard = false; LoadCard(i); }
                GUILayout.Label(data.set[i].tier.ToString(), EditorStyles.label);
                GUILayout.Label(data.set[i].type.ToString(), EditorStyles.label);
                GUILayout.EndHorizontal();


            }
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
    }
    private void LoadCard(int i)
    {
        singleCard = data.set[i];
        ClearStats();
        StringToStats(singleCard.implicits);
    }
    private void SaveCard()
    {
        singleCard.implicits = StatsToString();
        if (newCard)
        {
            Debug.Log("added new card");
            data.set.Add(singleCard);
        }
        else
        {
            data.set[editingInt] = singleCard;
        }
        Cancel();
    }
    public void LoadFromJson()
    {
        string jsonData = System.IO.File.ReadAllText(Application.dataPath + "/Card/cardData/cardData.json");
        data = JsonUtility.FromJson<CardDataSet>(jsonData);
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
        implicitString.Remove(implicitString.Length - 1, 1);
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
}