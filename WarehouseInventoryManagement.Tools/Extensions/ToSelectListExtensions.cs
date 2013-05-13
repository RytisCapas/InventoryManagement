using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WarehouseInventoryManagement.DataEntities.Enums.Extensions;

namespace WarehouseInventoryManagement.Tools.Extensions
{
    public static class ToSelectListExtension
    {
        public static SelectList ToSelectList<T>(this IEnumerable<T> source, string dataValueField, string dataTextField, object selectedValue)
        {
            var list = new SelectList(source, dataValueField, dataTextField, selectedValue ?? -1);

            return list;
        }

        public static SelectList ToSelectList<T>(this IQueryable<T> query, string dataValueField, string dataTextField, object selectedValue)
        {
            var list = new SelectList(query, dataValueField, dataTextField, selectedValue ?? -1);

            return list;
        }

        public static List<SelectListItem> ToSelectList(this IEnumerable<int> enumerable, Func<int, string> text, Func<int, string> value)
        {
            var items = enumerable.Select(f => new SelectListItem { Text = text(f), Value = value(f) }).ToList();

            return items;
        }

        public static List<SelectListItem> ToSelectList<TEnum>(this TEnum selected, bool useNameForValue = true, bool defaultSelectedFirst = false)
        {
            var enumValues = Enum.GetValues(typeof(TEnum));

            var items = new List<SelectListItem>();

            foreach (TEnum enumValue in enumValues)
            {
                var fieldInfo = typeof(TEnum).GetField(enumValue.ToString());

                var attribs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

                var text = (attribs != null && attribs.Length > 0) ? attribs[0].StringValue : null;

                text = text ?? enumValue.ToString();

                var value = useNameForValue ? enumValue.ToString() : Convert.ToInt32(enumValue).ToString(CultureInfo.InvariantCulture);

                var item = new SelectListItem
                {
                    Value = value,
                    Text = text,
                    Selected = ((int)(object)enumValue & (int)(object)selected) != 0
                };

                items.Add(item);
            }

            return items;
        }

        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> text, Func<T, string> value)
        {
            var items = enumerable.Select(f => new SelectListItem { Text = text(f), Value = value(f) }).ToList();

            return items;
        }

        public static List<SelectListItem> ToSelectListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, bool convertValueToo = false)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            TypeConverter converter = TypeDescriptor.GetConverter(enumType);

            IEnumerable<SelectListItem> items =
                from value in values
                select new SelectListItem
                {
                    Text = converter.ConvertToString(value),
                    Value = convertValueToo ? converter.ConvertToString(value) : value.ToString(),
                    Selected = value.Equals(metadata.Model)
                };

            return items.ToList();
        }

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            var realModelType = modelMetadata.ModelType;

            var underlyingType = Nullable.GetUnderlyingType(realModelType);

            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }

            return realModelType;
        }
    }
}
