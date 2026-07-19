using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Utility
{
    public class PaginationSetting
    {
        int _pageSize = Int32.MaxValue, _pageNum = 1;
        int _totalRows;
        string _orderBy;
        bool _orderByAscending = false;
        static readonly string[] _sqlDangerousChars = new string[]
      {
            "--",
            "/*",
            "*/",
            "'",
            ";",
            "\r",
            "\n"
      };

        public PaginationSetting()
        {
        }

        public int TotalPage
        {
            get
            {
                if (_totalRows == 0)
                    return 1;

                return _totalRows % _pageSize == 0 ? (_totalRows / _pageSize) : (_totalRows / _pageSize + 1);
            }
        }

        public int TotalRows
        {
            get { return _totalRows; }
            set { _totalRows = value; }
        }

        public int PageNum
        {
            get { return _pageNum; }
            set { _pageNum = value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public string OrderBy
        {
            get { return _orderBy; }
            set
            {
                if (ContainsSQLDangerousString(value))
                    throw new ArgumentException("The specified order by property contains potentially dangerous characters.");

                _orderBy = value;
            }
        }

        public bool OrderByAscending
        {
            get { return _orderByAscending; }
            set { _orderByAscending = value; }
        }
        public static bool ContainsSQLDangerousString(string strToFilter)
        {
            if (string.IsNullOrEmpty(strToFilter))
                return false;

            foreach (string i in _sqlDangerousChars)
            {
                if (strToFilter.Contains(i))
                    return true;
            }

            return false;
        }
    }

}
