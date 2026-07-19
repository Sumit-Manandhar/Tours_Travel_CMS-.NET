using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace vHolidays.Utility
{
    public static class PaginationHelper
    {
        public const int PagerSegmentSize = 5;
        public static IHtmlContent RenderOrder(this IHtmlHelper helper, string column, PaginationSetting paging)
        {
            return RenderOrder(helper, column, paging.OrderBy, paging.OrderByAscending);
        }
        public static IHtmlContent RenderOrder(this IHtmlHelper helper, string column, string orderBy, bool orderByAscending)
        {
            if (string.Equals(column, orderBy, System.StringComparison.OrdinalIgnoreCase))
            {
                var iconClass = orderByAscending ? "asc" : "desc";
                var html = $"<i class='fa fa-sort-{iconClass}'></i>";
                return new HtmlString(html);
            }
            else
            {
                return HtmlString.Empty;
            }
        }
        public static IHtmlContent RenderPager2(this IHtmlHelper helper, int pageNum, int pageCount)
        {
            var buildLi = new Func<string, string, string, string, TagBuilder>(
                (liClass, aClass, page, text) =>
                {
                    var li = new TagBuilder("li");
                    li.AddCssClass(liClass);
                    var a = new TagBuilder("a");
                    a.InnerHtml.Append(text);
                    a.MergeAttribute("data-page", page);
                    a.MergeAttribute("href", "javascript:void(0);");
                    a.AddCssClass(aClass);
                    li.InnerHtml.AppendHtml(a);
                    return li;
                });

            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination mt-4");

            // First
            ul.InnerHtml.AppendHtml(buildLi("page-item first" + (pageNum == 1 ? " disabled" : string.Empty), "page-link", "first", "«"));

            // Prev
            ul.InnerHtml.AppendHtml(buildLi("page-item prev" + (pageNum - 1 <= 0 ? " disabled" : string.Empty), "page-link", "prev", "<"));

            // Indexes
            int pageSegment = pageNum % PagerSegmentSize == 0 ? pageNum / PagerSegmentSize : pageNum / PagerSegmentSize + 1;
            int lower = GetLowerPageCount(pageNum, pageCount); // pageNum <= 2 ? 1 : pageNum - 2; //(pageSegment - 1) * PagerSegmentSize + 1;
            int upper = GetUpperPageCount(pageNum, pageCount); //pageNum <= 2 ? PagerSegmentSize : pageNum + 2; // pageSegment * PagerSegmentSize;

            for (int i = lower; i <= upper && i <= pageCount; i++)
            {
                ul.InnerHtml.AppendHtml(buildLi("page-item page-" + i + (pageNum == i ? " active" : string.Empty), "page-link", i.ToString(), i.ToString()));
            }

            // Next
            ul.InnerHtml.AppendHtml(buildLi("page-item next" + (pageNum + 1 > pageCount ? " disabled" : string.Empty), "page-link", "next", ">"));

            // Last
            ul.InnerHtml.AppendHtml(buildLi("page-item last" + (pageNum == pageCount ? " disabled" : string.Empty), "page-link", "last", "»"));

            return ul;
        }

        public static int GetLowerPageCount(int pageNum, int pageCount)
        {
            //int pageSegment = pageNum % PagerSegmentSize == 0 ? pageNum / PagerSegmentSize : pageNum / PagerSegmentSize + 1;
            int lower = 1;
            if (pageNum <= 2)
                return lower;
            if (pageNum > pageCount - 2)
                return pageCount - 4 <= 0 ? lower : pageCount - 4; //(pageSegment - 1) * PagerSegmentSize + 1;
            return pageNum - 2;
        }

        public static int GetUpperPageCount(int pageNum, int pageCount)
        {
            //int pageSegment = pageNum % PagerSegmentSize == 0 ? pageNum / PagerSegmentSize : pageNum / PagerSegmentSize + 1;
            int higher = 5;
            if (pageNum <= 2)
                return higher;
            if (pageNum > pageCount - 2)
                return pageCount; //pageSegment * PagerSegmentSize;
            return pageNum + 2;
        }

        public static IHtmlContent RenderPager(this IHtmlHelper helper, int pageNum, int pageCount)
        {
            var buildLi = new Func<string, string, string, TagBuilder>(
                (liClass, page, text) =>
                {
                    var li = new TagBuilder("li");
                    li.AddCssClass(liClass);
                    var a = new TagBuilder("a");
                    a.InnerHtml.Append(text);
                    a.Attributes.Add("data-page", page);
                    a.Attributes.Add("href", "javascript:void(0);");
                    a.AddCssClass("button page-link");
                    a.AddCssClass(liClass);
                    li.InnerHtml.AppendHtml(a);
                    return li;
                });

            var navBuilder = new TagBuilder("nav");
            navBuilder.AddCssClass("Page navigation");

            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");

            // First
            ul.InnerHtml.AppendHtml(buildLi("page-item first" + (pageNum == 1 ? " disabled" : string.Empty), "first", "«"));

            // Prev
            ul.InnerHtml.AppendHtml(buildLi("page-item prev" + (pageNum - 1 <= 0 ? " disabled" : string.Empty), "prev", "<"));

            // Indexes
            int pageSegment = pageNum % PagerSegmentSize == 0 ? pageNum / PagerSegmentSize : pageNum / PagerSegmentSize + 1;
            int lower = (pageSegment - 1) * PagerSegmentSize + 1;
            int upper = pageSegment * PagerSegmentSize;

            for (int i = lower; i <= upper && i <= pageCount; i++)
            {
                ul.InnerHtml.AppendHtml(buildLi("page-" + i + (pageNum == i ? " active" : string.Empty), i.ToString(), i.ToString()));
            }

            // Next
            ul.InnerHtml.AppendHtml(buildLi("next" + (pageNum + 1 > pageCount ? " disabled" : string.Empty), "next", ">"));

            // Last
            ul.InnerHtml.AppendHtml(buildLi("last" + (pageNum == pageCount ? " disabled" : string.Empty), "last", "»"));

            navBuilder.InnerHtml.AppendHtml(ul);

            return new HtmlString(navBuilder.ToString());
        }

        public static IHtmlContent RenderPageSize(this IHtmlHelper htmlHelper, string name, int selectedValue)
        {
            var selectTag = new TagBuilder("select");
            selectTag.MergeAttribute("name", name);
            selectTag.Attributes.Add("class", "form-control input-sm");
            var divTag = new TagBuilder("div");
            divTag.MergeAttribute("class", "form-group page-size-selector col-md-2 mb-2 ms-auto");


            foreach (var item in htmlHelper.PageSizes(selectedValue))
            {
                var optionTag = new TagBuilder("option");
                optionTag.MergeAttribute("value", item.Value);
                optionTag.InnerHtml.SetHtmlContent(item.Text);
                if (int.Parse(item.Value) == selectedValue)
                {
                    optionTag.MergeAttribute("selected", "Selected");
                }
                selectTag.InnerHtml.AppendHtml(optionTag);
            }
            divTag.InnerHtml.AppendHtml(selectTag);
            return divTag;
        }

        public static IHtmlContent RenderPageSizeSelect(this IHtmlHelper helper, string name, int pageSize)
        {
            var select = new TagBuilder("select");
            select.Attributes.Add("name", name);
            select.Attributes.Add("class", "form-control input-sm");

            string pageSizeStr = pageSize.ToString();
            var options = new StringBuilder();
            foreach (var i in helper.PageSizes(pageSize))
            {
                var option = new TagBuilder("option");
                option.Attributes.Add("value", i.Value);
                if (pageSizeStr == i.Value)
                    option.Attributes.Add("selected", "selected");
                option.InnerHtml.Append(i.Text);
                options.AppendLine(option.ToString());
            }
            var test = HttpUtility.UrlEncode(options.ToString());

            select.InnerHtml.AppendHtml(test);
            string html = string.Format("<div class='form-group page-size-selector'>{0}</div>", select.ToString());
            return new HtmlString(html);
        }
        public static IHtmlContent RenderPageInfo(this IHtmlHelper helper, int pageNum, int pageSize, int totalRows)
        {
            string info = "<div class='pager-info mt-4 ms-auto'>Showing {0} to {1} of {2} records</div>";

            if (totalRows > 0)
            {
                int lowerRow = (pageNum - 1) * pageSize + 1;
                int upperRow = pageNum * pageSize;
                if (upperRow > totalRows)
                    upperRow = totalRows;
                info = string.Format(info, lowerRow, upperRow, totalRows);
                return new HtmlString(info);
            }
            else
            {
                return HtmlString.Empty;
            }
        }

        public static IEnumerable<SelectListItem> PageSizes(this IHtmlHelper helper, int? val)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="10 Rows/Page", Value="10",Selected=val==10 },
                new SelectListItem{ Text="20 Rows/Page", Value="20", Selected=val==20 },
                new SelectListItem{ Text="50 Rows/Page", Value="50",Selected=val==50 },
                new SelectListItem{ Text="100 Rows/Page", Value="100",Selected=val==100 },
                new SelectListItem{ Text="View All", Value =int.MaxValue.ToString(),Selected=val == int.MaxValue }
            };
            return list;
        }

    }

}
