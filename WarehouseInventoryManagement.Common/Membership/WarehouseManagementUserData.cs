using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace WarehouseInventoryManagement.Common.Membership
{
    [DataContract]
    public class WarehouseManagementUserData
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string ImpersonatorUserName { get; set; }

        public static WarehouseManagementUserData Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            var serializer = new DataContractJsonSerializer(typeof(WarehouseManagementUserData));

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (WarehouseManagementUserData)serializer.ReadObject(stream);
            }
        }

        public static string Serialize(WarehouseManagementUserData userData)
        {
            var serializer = new DataContractJsonSerializer(typeof(WarehouseManagementUserData));

            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, userData);

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
