using System;
namespace blocket_lite.Models
{
    public class UsersModel
    {
        //miles,year,color

        public string? username { get; set; }
        public string? password { get; set; }
        public string? salt { get; set; }

        public List<ItemModel> liked { get; set; }
    }
}

