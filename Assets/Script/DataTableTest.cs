using UnityEngine;

public class DataTableTest : MonoBehaviour
{
    public string NameStringTableKr = "StringTableKr";
    public string NameStringTableEn = "StringTableEn";
    public string NameStringTableJp = "StringTableJp";

    public void OnClickStringTableKr()
    {
        Debug.Log(DataTableManager.StringTable.Get("YOU DIE"));
        
    }
    public void OnClickStringTableEn()
    {
        var table = new StringTable();
        table.Load(NameStringTableEn);
        Debug.Log(table.Get("YOU DIE"));
        Debug.Log(table.Get("AAA"));

    }
    public void OnClickStringTableJp()
    {
        var table = new StringTable();
        table.Load(NameStringTableJp);
        Debug.Log(table.Get("YOU DIE"));
        Debug.Log(table.Get("CALL"));
    }

    
}
