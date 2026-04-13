using UnityEngine;
using CsvHelper;
using System.IO;
using System.Globalization;
public class CSVData
{
    public string Id { get; set; }
    public string String {  get; set; }

}
public class csvTest1 : MonoBehaviour
{
    public TextAsset textAsset;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //string csv = textAsset.text;
            TextAsset textAsset = Resources.Load<TextAsset>("DataTables/StringTableKr");
            string csv = textAsset.text;
            using(var reader = new StringReader(csv))
            using(var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<CSVData>();

                foreach (var record in records)
                {
                    Debug.Log($"{record.Id}: {record.String}");
                }
            }
        }
    }
}
