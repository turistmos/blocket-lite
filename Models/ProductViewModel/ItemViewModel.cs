using System.Collections.Generic;


namespace blocket_lite.Models.ProductViewModel
{
    public class ItemViewModel
    {
        public List<ItemModel> ItemList { get; set; }
        public ItemModel Item { get; set; }
        public List<ItemModel> userItemList { get; set; }
        public List<ItemModel> likedItems { get; set; }
        public List<ItemModel> userLikedItems { get; set; }
    }
}