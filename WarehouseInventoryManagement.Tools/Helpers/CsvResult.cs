using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WarehouseInventoryManagement.Tools.Helpers
{
    public sealed class CsvResult<T> : FileResult
    {
        private readonly IEnumerable<T> collection;

        public CsvResult(IEnumerable<T> collection)
            : base("text/csv")
        {
            this.collection = collection;
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            var outputStream = response.OutputStream;
            using (var mstream = new MemoryStream())
            {
                WriteObject(mstream);
                outputStream.Write(mstream.GetBuffer(), 0, (int)mstream.Length);
            }
        }

        private static void WriteValue(StreamWriter writer, string literal)
        {
            writer.Write(@"""");
            writer.Write(literal.Replace(@"""", @""""""));
            writer.Write(@""",");
        }

        private void WriteObject(Stream stream)
        {
            var writer = new StreamWriter(stream, System.Text.Encoding.Default);

            var modelType = typeof(T);
            var metadatas = ModelMetadataProviders.Current.GetMetadataForProperties(null, modelType).ToList();

            foreach (var t in metadatas.Where(x => x.Order > 0).OrderBy(x => x.Order))
            {
                WriteValue(writer, t.DisplayName ?? t.PropertyName);
            }

            writer.WriteLine();

            var en = collection.GetEnumerator();
            while (en.MoveNext())
            {
                var metadata = ModelMetadataProviders.Current.GetMetadataForType(() => en.Current, modelType);

                foreach (var prop in metadata.Properties.Where(x => x.Order > 0))
                {
                    WriteValue(writer, prop.SimpleDisplayText ?? string.Empty);
                }

                writer.WriteLine();
            }

            writer.Flush();
        }
    }
}