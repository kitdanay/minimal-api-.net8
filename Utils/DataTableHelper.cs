using System.Data;

public static class DataTableHelper
{
    public static Dictionary<string, object> ConvertDataRowToDictionary(DataRow row)
    {
        if (row == null) throw new ArgumentNullException(nameof(row));

        return row.Table.Columns.Cast<DataColumn>()
                    .ToDictionary(
                        col => col.ColumnName,
                        col => row[col]
                    );
    }

    public static List<Dictionary<string, object>> ConvertDataTableToList(DataTable table)
    {
        if (table == null) throw new ArgumentNullException(nameof(table));

        return table.AsEnumerable()
                    .Select(row => ConvertDataRowToDictionary(row))
                    .ToList();
    }

}