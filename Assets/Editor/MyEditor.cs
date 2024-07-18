using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Excel;
using System.Data;
using System.IO;
using UnityEngine.UI;
public static class MyEditor
{
    [MenuItem("我的工具/excel转csv")]
    public static void ExportExcelToTxt()
    {
        string gamePath = Application.dataPath;
        string excelPath = gamePath + "/_Excel";
        Debug.Log(excelPath);
        if (!Directory.Exists(excelPath))
        {
            Directory.CreateDirectory(excelPath);
            return;
        }

        string[] files = Directory.GetFiles(excelPath, "*.xlsx");

        for (int i = 0; i < files.Length; i++)
        {
            files[i] = files[i].Replace('\\', '/');

            using (FileStream fs = File.Open(files[i], FileMode.Open, FileAccess.Read))
            {
                var excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);

                DataSet dataSet = excelDataReader.AsDataSet();

                if (dataSet == null)
                {
                    continue;
                }

                DataTable table = dataSet.Tables[0];

                readTableToTxt(files[i], "Resources/Data", table);
            }
        }

        AssetDatabase.Refresh();
    }

    private static void readTableToTxt(string filePath, string outPathStr, DataTable table)
    {
        string fileName = Path.GetFileNameWithoutExtension(filePath);

        string path = Application.dataPath + "/" + outPathStr + "/" + fileName + ".txt";

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                for (int row = 0; row < table.Rows.Count; row++)
                {
                    DataRow dataRow = table.Rows[row];

                    string str = "";
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        string val = dataRow[col].ToString();

                        if (col != table.Columns.Count - 1)
                        {
                            str = str + val + ",";
                        }
                        else
                        {
                            str = str + val;
                        }
                    }

                    sw.Write(str);

                    if (row != table.Rows.Count - 1)
                    {
                        sw.WriteLine();
                    }
                }
            }
        }
    }

    //[MenuItem("我的工具/csv导入Addressable")]
    //private static void ExportCsVToAddressable()
    //{
    //    string[] files = Directory.GetFiles(Application.dataPath + "/Addressable/Data", "*.txt");
    //    AddressableAssetSettings setting = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>("Assets/AddressableAssetsData/AddressableAssetSettings.asset");
    //    AddressableAssetGroup group = setting.FindGroup("Data");
    //    for (int i = 0; i < files.Length; i++)
    //    {
    //        string fullName = files[i].Replace('\\', '/');
    //        string fileName = Path.GetFileNameWithoutExtension(fullName);
    //        string guid = AssetDatabase.AssetPathToGUID($"Assets/Addressable/Data/{fileName}.txt");
    //        AddressableAssetEntry entry = setting.CreateOrMoveEntry(guid, group);
    //        entry.SetAddress(fileName);
    //        entry.SetLabel("", false);
    //    }  
    //    AssetDatabase.Refresh();
    //}

    //[MenuItem("我的工具/UI导入Addressable")]
    //private static void ExportUIToAddressable()
    //{
    //    string[] files = Directory.GetFiles(Application.dataPath + "/Addressable/UI", "*.prefab");
    //    AddressableAssetSettings setting = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>("Assets/AddressableAssetsData/AddressableAssetSettings.asset");
    //    AddressableAssetGroup group = setting.FindGroup("UI");
    //    for (int i = 0; i < files.Length; i++)
    //    {
    //        string fullName = files[i].Replace('\\', '/');
    //        string fileName = Path.GetFileNameWithoutExtension(fullName);
    //        string guid = AssetDatabase.AssetPathToGUID($"Assets/Addressable/UI/{fileName}.prefab");
    //        AddressableAssetEntry entry = setting.CreateOrMoveEntry(guid, group);
    //        entry.SetAddress(fileName);
    //        entry.SetLabel("", false);
    //    }
    //    AssetDatabase.Refresh();
    //}


    //[MenuItem("我的工具/Icon导入Addressable")]
    //private static void ExportIconToAddressable()
    //{
    //    string[] files = Directory.GetFiles(Application.dataPath + "/Addressable/Icon", "*.png");
    //    AddressableAssetSettings setting = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>("Assets/AddressableAssetsData/AddressableAssetSettings.asset");
    //    AddressableAssetGroup group = setting.FindGroup("Icon");
    //    for (int i = 0; i < files.Length; i++)
    //    {
    //        string fullName = files[i].Replace('\\', '/');
    //        string fileName = Path.GetFileNameWithoutExtension(fullName);
    //        string guid = AssetDatabase.AssetPathToGUID($"Assets/Addressable/Icon/{fileName}.png");
    //        AddressableAssetEntry entry = setting.CreateOrMoveEntry(guid, group);
    //        entry.SetAddress(fileName);
    //        entry.SetLabel("", false);
    //    }
    //    AssetDatabase.Refresh();
    //}

    //[MenuItem("我的工具/Model导入Addressable")]
    //private static void ExportModelToAddressable()
    //{
    //    string[] files = Directory.GetFiles(Application.dataPath + "/Addressable/Model", "*.prefab");
    //    AddressableAssetSettings setting = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>("Assets/AddressableAssetsData/AddressableAssetSettings.asset");
    //    AddressableAssetGroup group = setting.FindGroup("Model");
    //    for (int i = 0; i < files.Length; i++)
    //    {
    //        string fullName = files[i].Replace('\\', '/');
    //        string fileName = Path.GetFileNameWithoutExtension(fullName);
    //        string guid = AssetDatabase.AssetPathToGUID($"Assets/Addressable/Model/{fileName}.prefab");
    //        AddressableAssetEntry entry = setting.CreateOrMoveEntry(guid, group);
    //        entry.SetAddress(fileName);
    //        entry.SetLabel("", false);
    //    }
    //    AssetDatabase.Refresh();
    //}
}
