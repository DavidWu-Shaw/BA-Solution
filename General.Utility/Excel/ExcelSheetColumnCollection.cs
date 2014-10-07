using System;
using System.Collections;
using System.Collections.Generic;

namespace General.Utility.Excel
{
    [Serializable]
    public class ExcelSheetColumnCollection : CollectionBase
    {
        private Dictionary<string, ExcelSheetColumn> _dicColumns;
        private Dictionary<string, ExcelSheetColumn> DicColumns
        {
            get
            {
                if (_dicColumns == null)
                {
                    _dicColumns = new Dictionary<string, ExcelSheetColumn>();
                    foreach (ExcelSheetColumn column in List)
                    {
                        _dicColumns.Add(column.Name, column);
                    }
                }

                return _dicColumns;
            }
        }

        private Dictionary<string, ExcelSheetColumn> _dicColumnsByTarget;
        private Dictionary<string, ExcelSheetColumn> DicColumnsByRequest
        {
            get
            {
                if (_dicColumnsByTarget == null)
                {
                    _dicColumnsByTarget = new Dictionary<string, ExcelSheetColumn>();
                    foreach (ExcelSheetColumn column in List)
                    {
                        // Some columns might not be mapped
                        if (column.IsMapped)
                        {
                            _dicColumnsByTarget.Add(column.TargetName, column);
                        }
                    }
                }

                return _dicColumnsByTarget;
            }
        }

        public void Add(ExcelSheetColumn column)
        {
            List.Add(column);
        }

        public ExcelSheetColumn Add(string name)
        {
            ExcelSheetColumn column = new ExcelSheetColumn(name);
            Add(column);
            return column;
        }

        public ExcelSheetColumn this[string name]
        {
            get
            {
                return DicColumns[name];
            }
        }

        public bool Contains(string name)
        {
            return DicColumns.ContainsKey(name);
        }

        public int IndexOf(string columnName)
        {
            ExcelSheetColumn c = this[columnName];
            if (c != null)
            {
                return List.IndexOf(c);
            }
            else
            {
                return -1;
            }
        }

        public ExcelSheetColumn GetByTargetColumn(string targetColumn)
        {
            if (DicColumnsByRequest.ContainsKey(targetColumn))
            {
                return DicColumnsByRequest[targetColumn];
            }
            else
            {
                return null;
            }
        }

        public int GetIndexByTargetColumn(string targetColumn)
        {
            if (DicColumnsByRequest.ContainsKey(targetColumn))
            {
                ExcelSheetColumn c = DicColumnsByRequest[targetColumn];
                return List.IndexOf(c);
            }
            return -1;
        }
    }
}