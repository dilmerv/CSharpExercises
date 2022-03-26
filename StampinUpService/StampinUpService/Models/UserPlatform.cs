using System;
using System.ComponentModel.DataAnnotations;

namespace StampinUp.Service.Models
{
    public class UserPlatform
    {
        public int DeviceId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string DeviceName { get; set; }

        public DateTime DevicePurchaseDate { get; set; }

        public UserPlatform(int deviceid, string devicename, DateTime devicepurchasedate)
        {
            DeviceId = deviceid;
            DeviceName = devicename;
            DevicePurchaseDate = devicepurchasedate;
        }
    }
}
