using UnityEditor;
using UnityEngine;

public class StaffEditor : EditorWindow
{

    StaffDataObject singleStaffMember;
    StaffDataSet data;
    int defencePhysical;
    int defenceLife;
    int defenceArmour;
    int defenceFire;
    int defenceCold;
    int defenceLightning;
    int defenceChaos;
    int defenceWild;
    int attackPhysical;
    int attackLife;
    int attackArmour;
    int attackFire;
    int attackCold;
    int attackLightning;
    int attackChaos;
    int attackWild;
    bool showForm = false;
    bool newCard = false;
    int editingInt;
    [MenuItem("Window/Staff Editor")]
    public static void ShowWindow()
    {
        GetWindow<StaffEditor>("Staff Editor");
    }
    void OnGUI()
    {
        LoadFromJson();


        // GUILayout.Label("Sort By", EditorStyles.boldLabel);

        // data.set.Sort((c1, c2) => c1.tier.CompareTo(c2.tier));
        // data.set.Sort((c1, c2) => c1.type.CompareTo(c2.type));
        data.set.Sort((c1, c2) => c1.name.CompareTo(c2.name));

        GUILayout.Label("Data", EditorStyles.boldLabel);

        if (showForm)
        {
            singleStaffMember.name = EditorGUILayout.TextField("name", singleStaffMember.name);
            // singleStaffMember.type = (Type)EditorGUILayout.EnumFlagsField("Type", singleStaffMember.type);
            singleStaffMember.tier = EditorGUILayout.IntField("Tier", singleStaffMember.tier);
            GUILayout.Label("", EditorStyles.boldLabel);
            GUILayout.Label("Attack Data", EditorStyles.boldLabel);
            attackPhysical = EditorGUILayout.IntField("physical attack", attackPhysical);
            attackLife = EditorGUILayout.IntField("life attack", attackLife);
            attackArmour = EditorGUILayout.IntField("armour attack", attackArmour);
            attackFire = EditorGUILayout.IntField("fire attack", attackFire);
            attackCold = EditorGUILayout.IntField("cold attack", attackCold);
            attackLightning = EditorGUILayout.IntField("lightning attack", attackLightning);
            attackChaos = EditorGUILayout.IntField("chaos attack", attackChaos);
            attackWild = EditorGUILayout.IntField("wild attack", attackWild);
            GUILayout.Label("", EditorStyles.boldLabel);
            GUILayout.Label("Defence Data", EditorStyles.boldLabel);
            defencePhysical = EditorGUILayout.IntField("physical defence", defencePhysical);
            defenceLife = EditorGUILayout.IntField("life defence", defenceLife);
            defenceArmour = EditorGUILayout.IntField("armour defence", defenceArmour);
            defenceFire = EditorGUILayout.IntField("fire defence", defenceFire);
            defenceCold = EditorGUILayout.IntField("cold defence", defenceCold);
            defenceLightning = EditorGUILayout.IntField("lightning defence", defenceLightning);
            defenceChaos = EditorGUILayout.IntField("chaos defence", defenceChaos);
            defenceWild = EditorGUILayout.IntField("wild defence", defenceWild);


            if (GUILayout.Button("Save")) { SaveCard(); SaveIntoJson(); }
            if (GUILayout.Button("Cancel")) { Cancel(); }
            if (GUILayout.Button("Update In Monster Set")) { SaveInMonsterSet(); }
        }
        else
        {
            for (int i = 0; i < data.set.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(data.set[i].name)) { editingInt = i; showForm = true; newCard = false; LoadCard(i); }
                GUILayout.Label(data.set[i].tier.ToString(), EditorStyles.label);
                // GUILayout.Label(data.set[i].type.ToString(), EditorStyles.label);
                GUILayout.EndHorizontal();


            }
            // if (GUILayout.Button("Load")) { LoadFromJson(); }
            if (GUILayout.Button("New")) { singleStaffMember = new StaffDataObject(); showForm = true; ClearStats(); newCard = true; }
        }
    }
    private void Cancel()
    {
        showForm = false; singleStaffMember = null;
    }
    private void ClearStats()
    {
        defencePhysical = 0;
        defenceLife = 0;
        defenceArmour = 0;
        defenceFire = 0;
        defenceCold = 0;
        defenceLightning = 0;
        defenceChaos = 0;
        defenceWild = 0;
        attackPhysical = 0;
        attackLife = 0;
        attackArmour = 0;
        attackFire = 0;
        attackCold = 0;
        attackLightning = 0;
        attackChaos = 0;
        attackWild = 0;
    }
    private void LoadCard(int i)
    {
        singleStaffMember = data.set[i];
        ClearStats();
        StringToStats(singleStaffMember.attack, true);
        StringToStats(singleStaffMember.defence, false);
    }
    private void SaveInMonsterSet()
    {
        // TODO : Add to monster sete
        Debug.LogError("not implimented yet");
    }
    private void SaveCard()
    {
        singleStaffMember.attack = StatsToString(false);
        singleStaffMember.defence = StatsToString(true);
        if (singleStaffMember.clipCount == 0) { singleStaffMember.clipCount = 1; }
        if (singleStaffMember.clipMethod == "") { singleStaffMember.clipMethod = "Broken"; }
        if (newCard)
        {
            Debug.Log("added new card");
            data.set.Add(singleStaffMember);
        }
        else
        {
            data.set[editingInt] = singleStaffMember;
        }
        Cancel();
    }
    public void LoadFromJson()
    {
        string jsonData = System.IO.File.ReadAllText(Application.dataPath + "/staff/staffData/staffData.json");
        data = JsonUtility.FromJson<StaffDataSet>(jsonData);
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
        System.IO.File.WriteAllText(Application.dataPath + "/staff/staffData/staffData.json", saveData);
    }
    private void SaveIntoMonsterJSON()
    {

    }
    private string StatsToString(bool isAttack)
    {
        string implicitString = "";
        if (isAttack)
        {
            for (int i = 0; i < attackPhysical; i++) { implicitString += "Physical,"; };
            for (int i = 0; i < attackLife; i++) { implicitString += "Life,"; };
            for (int i = 0; i < attackArmour; i++) { implicitString += "Armour,"; };
            for (int i = 0; i < attackFire; i++) { implicitString += "Fire,"; };
            for (int i = 0; i < attackCold; i++) { implicitString += "Cold,"; };
            for (int i = 0; i < attackLightning; i++) { implicitString += "Lightning,"; };
            for (int i = 0; i < attackChaos; i++) { implicitString += "Chaos,"; };
            for (int i = 0; i < attackWild; i++) { implicitString += "Wild,"; };
        }
        else
        {
            for (int i = 0; i < defencePhysical; i++) { implicitString += "Physical,"; };
            for (int i = 0; i < defenceLife; i++) { implicitString += "Life,"; };
            for (int i = 0; i < defenceArmour; i++) { implicitString += "Armour,"; };
            for (int i = 0; i < defenceFire; i++) { implicitString += "Fire,"; };
            for (int i = 0; i < defenceCold; i++) { implicitString += "Cold,"; };
            for (int i = 0; i < defenceLightning; i++) { implicitString += "Lightning,"; };
            for (int i = 0; i < defenceChaos; i++) { implicitString += "Chaos,"; };
            for (int i = 0; i < defenceWild; i++) { implicitString += "Wild,"; };
        }
        implicitString.Remove(implicitString.Length - 1, 1);
        return implicitString;
    }
    private void StringToStats(string implicitString, bool isAttack)
    {
        string[] stringArray = implicitString.Split(",");
        if (isAttack)
        {

            for (int i = 0; i < stringArray.Length; i++)
            {
                if (stringArray[i] == "Fire") { attackFire++; }
                if (stringArray[i] == "Cold") { attackCold++; }
                if (stringArray[i] == "Lightning") { attackLightning++; }
                if (stringArray[i] == "Physical") { attackPhysical++; }
                if (stringArray[i] == "Life") { attackLife++; }
                if (stringArray[i] == "Armour") { attackArmour++; }
                if (stringArray[i] == "Chaos") { attackChaos++; }
                if (stringArray[i] == "Wild") { attackWild++; }
            }
        }
        else
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (stringArray[i] == "Fire") { defenceFire++; }
                if (stringArray[i] == "Cold") { defenceCold++; }
                if (stringArray[i] == "Lightning") { defenceLightning++; }
                if (stringArray[i] == "Physical") { defencePhysical++; }
                if (stringArray[i] == "Life") { defenceLife++; }
                if (stringArray[i] == "Armour") { defenceArmour++; }
                if (stringArray[i] == "Chaos") { defenceChaos++; }
                if (stringArray[i] == "Wild") { defenceWild++; }
            }
        }
    }
}