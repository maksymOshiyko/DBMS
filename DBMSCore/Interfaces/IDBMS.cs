using DBMSCore.Models;

namespace DBMSCore.Interfaces;

public interface IDBMS
{
    Database CreateDatabase(string name);
    Database GetDatabase();
    byte[] DownloadDatabase();
    void SaveDatabase(string path);
    Database UploadDatabase(byte[] file);
    Table AddTable(string name);
    void DeleteTable(int tableIndex);
    Row AddRow(int tableIndex, List<string> values);
    void DeleteRow(int tableIndex, int rowIndex);
    Row EditRow(int tableIndex, int rowIndex, List<string> newValues);
    Column AddColumn(int tableIndex, string name, string type);
    void DeleteColumn(int tableIndex, int columnIndex);
    Table SortByColumn(int tableIndex, int sortColumnIndex);
}