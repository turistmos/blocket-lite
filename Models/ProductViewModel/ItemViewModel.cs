using System.Collections.Generic;


<<<<<<< HEAD
namespace blocket_lite.Models.ProductViewModel;
public class ItemViewModel
{
    public List<ItemModel> ItemList {get;set;}
    public ItemModel Item {get; set;}
    public List<ItemModel> userItemList { get; set; }
    public List<ItemModel> likedItems{ get; set; }
    public List<ItemModel> userLikedItems { get; set; }
=======
namespace blocket_lite.Models.ProductViewModel
{
    public class ItemViewModel
    {
        public List<ItemModel> ItemList { get; set; }
        public ItemModel Item { get; set; }
        public List<ItemModel> userItemList { get; set; }
        public List<ItemModel> likedItems { get; set; }
        public List<ItemModel> userLikedItems { get; set; }
        public List<ItemModel> addedToCartList { get; set; }
    }
>>>>>>> c297ae0fcb752dcd822bf4c24eb09f4717043273
}