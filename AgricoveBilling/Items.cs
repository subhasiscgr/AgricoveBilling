using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricoveBilling
{
    public class Items
    {
        [Key]
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public string ItemUnit { get; set; }

        private string _description;
        private double _qty;
        private double _price;

        //private Items tempobj;
        public Items()
        {

        }
        public Items( Items tempobj )
        {
            description = tempobj.description;
            qty = tempobj.qty;
            price = tempobj.price;
        }

        //private double _totalCost;

        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        public double qty
        {
            get { return _qty; }
            set { _qty = value; }
        }
        public double price
        {
            get { return _price; }
            set { _price = value; }
        }
        //public double totalCost1 => _price * _qty;
        public double totalCost
        {
            get { return _price * _qty; }
            //set { _totalCost = _price * _qty; }
            //get { return _totalCost; }
            //set { _totalCost = value; }
        }

    }
}
