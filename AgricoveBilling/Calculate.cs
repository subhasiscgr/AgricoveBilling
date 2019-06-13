using System;
using System.Collections.Generic;
using System.Linq;

namespace AgricoveBilling
{


    class Calculate
    {
        
        public double SubTotal { get; set; }
        public double DiscountTotal { get; set; }
        public double TaxTotal { get; set; }
        public double GrossTotal { get; set; }
        public double BalanceDue { get; set; }
        public double Discount { get; set; }
        public double TaxRate { get; set; }
        public double TotalAmtPaid { get; set; }
        public string Invoice { get; set; }


        public void CalculateAll(List<Items>itemsList, double discount, double taxPercent, double totalAmtPaid)
        {
            Discount = discount;
            TaxRate = taxPercent;
            TotalAmtPaid = totalAmtPaid;
            SubTotal = Math.Round(CalculateSubTotal(itemsList));
            DiscountTotal = Math.Round(CalculateDiscount(SubTotal, discount));
            TaxTotal = Math.Round(CalculateTax(taxPercent, DiscountTotal),2);
            GrossTotal = CalculateGrossTotal(DiscountTotal, TaxTotal);
            BalanceDue = CalculateBalanceDue(totalAmtPaid, GrossTotal);
            
        }
        private double CalculateTax(double taxPercent, double amount)
        {
            double temp = (double)(taxPercent / 100);

            return (temp * amount); //total amount of tax in value
        }      
        private double CalculateSubTotal(List<Items> values)
        {
            double subtotal =  values.Sum(item => item.totalCost);
            return subtotal;   
            //return values.Sum(); //subtotal
        }
        private double CalculateDiscount(double subTotal, double discount)
        {
            return subTotal - discount; //subtotal less discount
        }
        private double CalculateGrossTotal(double subTotalLessDiscount, double taxAmount)
        {
            return subTotalLessDiscount + taxAmount; //adding the tax with the gross total less discount
        }
        private double CalculateBalanceDue(double totalAmtPaid, double grossTotal)
        {
            return grossTotal - totalAmtPaid; //total amount due
        }
    }
}
