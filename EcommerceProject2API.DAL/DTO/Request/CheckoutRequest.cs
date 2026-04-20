using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.DTO.Request
{
    public enum PaymentMethodEnum
    {
        Cash=1,Visa=2
    }
    public class CheckoutRequest
    {
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PhoneNumber{ get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]//هاي عشان يرضى بأنه اكتب كلمة فيزا او كاش من الفرونت بدل الارقام
        public PaymentMethodEnum PaymentMethod { get; set; }

    }
}
