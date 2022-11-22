using DBMSCore.Dtos;
using DBMSCore.Models;

namespace DBMSCore.Interfaces;

public interface IDbms
{
    Database CreateDatabase(string name);
    Database GetDatabase();
    void SaveDatabase(string path);
    Database UploadDatabase(string path);
    TableDto AddTable(string name);
    void DeleteTable(int tableIndex);
    RowDto AddRow(int tableIndex, List<string> values);
    void DeleteRow(int tableIndex, int rowIndex);
    RowDto EditRow(int tableIndex, int rowIndex, List<string> newValues);
    ColumnDto AddColumn(int tableIndex, string name, string type);
    void DeleteColumn(int tableIndex, int columnIndex);
    TableDto SortByColumn(int tableIndex, int sortColumnIndex);
    byte[] DownloadPng(int tableIndex, int columnIndex, int rowIndex);
    TableDto GetTable(int tableIndex);
    bool IsDatabaseCreated();
}