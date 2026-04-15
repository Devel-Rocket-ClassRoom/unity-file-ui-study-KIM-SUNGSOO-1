public enum Languages
{
    Korean,
    English,
    Japanese,
}

public enum ItemTypes
{
    Weapon,
    Equip,
    Consumable
}

public enum CharacterTypes
{
    Warriar,
    Archer,
    Tanker
}
public static class Variables
{
    public static event System.Action OnLanguageChanged;
    private static Languages language = Languages.Korean;
    public static Languages Languages
    {
        get
        {
            return language;
        }
        set
        {
            if (language == value)
            {
                return;
            }
            language = value;
            DataTableManager.ChangeLanguage(language);
            OnLanguageChanged?.Invoke();
        }
    }

}

public static class DataTableIDs
{

    public static readonly string[] StringTableIds =
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp"
    };

    public static string String => StringTableIds[(int)Variables.Languages];

    public static readonly string Item = "ItemTable";

    public static readonly string character = "CharacterTables";
}
