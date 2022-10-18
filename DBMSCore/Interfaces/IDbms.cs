using DBMSCore.Models;

namespace DBMSCore.Interfaces;

public interface IDbms
{
    Database CreateDatabase(string name);
    Database GetDatabase();
    void SaveDatabase(string path);
    Database UploadDatabase(string path);
    Table AddTable(string name);
    void DeleteTable(int tableIndex);
    Row AddRow(int tableIndex, List<string> values);
    void DeleteRow(int tableIndex, int rowIndex);
    Row EditRow(int tableIndex, int rowIndex, List<string> newValues);
    Column AddColumn(int tableIndex, string name, string type);
    void DeleteColumn(int tableIndex, int columnIndex);
    Table SortByColumn(int tableIndex, int sortColumnIndex);
    byte[] DownloadPng(int tableIndex, int columnIndex, int rowIndex);
}