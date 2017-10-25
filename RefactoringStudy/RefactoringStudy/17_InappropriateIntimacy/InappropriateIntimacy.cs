using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringStudy._17_InappropriateIntimacy
{
    class InappropriateIntimacy
    {
        // 1. PayInfoService => Orderservice의 안으로 이동

        //public void UpdateChangeProductCharge(int id, string plusOrMinus, int price, DeliveryInfoItem DeliveryInfoItem)
        //{
        //    PayInfoItem item = PayInfoDao.FindById(id);
        //    if (plusOrMinus.ToUpper().Equals("PLUS"))
        //    {
        //        UpdateChangeProductChargePlus(item, price, DeliveryInfoItem);
        //    }
        //    else
        //    {
        //        UpdateChangeProductChargeMinus(item, price);
        //    }
        //}

        //internal void UpdateChangeProductChargeMinus(PayInfoItem item, int price)
        //{
        //    if (item == null) return;
        //    item.ProductCharge -= price;

        //    item.CashPrice = item.ComputedCashPrice();
        //    PayInfoDao.Update(item);
        //}

        //internal void UpdateChangeProductChargePlus(PayInfoItem item, int price, DeliveryInfoItem DeliveryInfoItem)
        //{
        //    if (item == null) return;
        //    item.ProductCharge += price;

        //    if ("MAN".Equals(item.PayType))
        //    {
        //        item.ManPaidCharge = TradeUtil.ManPaidCharge(price);
        //    }

        //    item.ShippingCharge = TradeUtil.GetShippingCharge(price, DeliveryInfoItem);
        //    item.CashPrice = item.ComputedCashPrice();
        //    if (item.PointPaid > item.TotalCharge)
        //    {
        //        item.PayType = "POINT";
        //        int returnPoint = item.PointPaid - item.TotalCharge;
        //        item.PointPaid = item.TotalCharge;
        //        if (returnPoint > 0)
        //        {
        //            string title = "支払金額変更による返金ポイント";
        //            PointService.AddPoint(title, returnPoint, item.UserId);
        //        }
        //    }

        //    PayInfoDao.Update(item);
        //}

        //public int ReducePayInfoWhenDepositChecked(PayInfoItem payInfoItem, int amount)
        //{
        //    if (payInfoItem.CashPrice <= amount)
        //    {
        //        payInfoItem.PayType = "POINT";
        //        payInfoItem.PointPaid -= amount - payInfoItem.CashPrice;
        //        payInfoItem.CashPrice = 0;
        //    }
        //    else
        //    {
        //        payInfoItem.CashPrice -= amount;
        //    }
        //    payInfoItem.ProductCharge -= amount;
        //    return amount;
        //}

        //////////////////////////////////////////////////////////////////////////////////////////////////
        // 2. Orderservice => PayInfoService 안으로 이동

        //public int GetTaxAddedPrice(int orderId)
        //{
        //    IList<ReadyMadeOrderItem> readyMadeOrderList = ReadyMadeOrderDao.FindValidByOrderId(orderId);
        //    IList<PrintMadeOrderItem> printMadeOrderList = PrintMadeOrderDao.FindValidByOrderId(orderId);
        //    int readyMadeOrderPrice = readyMadeOrderList.Sum(item => item.TaxAddedTotalPrice);
        //    int printMadeOrderPrice = printMadeOrderList.Sum(item => item.TaxAddedTotalPrice);

        //    return readyMadeOrderPrice + printMadeOrderPrice;
        //}

        //public void SetPriceInPayInfoParam(int orderId, PayInfoParam param)
        //{
        //    IList<ReadyMadeOrderItem> readyMadeOrderList = ReadyMadeOrderDao.FindValidByOrderId(orderId);
        //    IList<PrintMadeOrderItem> printMadeOrderList = PrintMadeOrderDao.FindValidByOrderId(orderId);
        //    int readyMadeOrderPriceWithTax = readyMadeOrderList.Sum(item => item.TaxAddedTotalPrice);
        //    int printMadeOrderPriceWithTax = printMadeOrderList.Sum(item => item.TaxAddedTotalPrice);
        //    param.TaxAddedPrice = readyMadeOrderPriceWithTax + printMadeOrderPriceWithTax;

        //    int readyMadeOrderPrice = readyMadeOrderList.Sum(item => item.TaxAddedTotalPrice - item.Tax);
        //    int printMadeOrderPrice = printMadeOrderList.Sum(item => item.TaxAddedTotalPrice - item.Tax);
        //    param.GoodsPrice = readyMadeOrderPrice + printMadeOrderPrice;
        //}


        ///////////////////////////////////////////////////////////
        // 3. 공통으로 사용하는 UpdateOrderStatusToProduct

        public class UpdateOrderStatusToProductService
        {
            public IOrderDao OrderDao { set; private get; }
            public IPrintMadeOrderDao PrintMadeOrderDao { set; private get; }
            public IReadyMadeOrderDao ReadyMadeOrderDao { set; private get; }
            public UpdateOrderExpectDate UpdateOrderExpectDate { set; private get; }
            public IMailService MailService { set; private get; }

            public bool UpdateOrderStatusToProduct(int id)
            {
                if (!CanProduct(id)) return false;
                OrderDao.UpdateOrderStatusToProduct(id);
                PrintMadeOrderDao.UpdateWorkStatusToGoodsOrderRequestByOrderId(id);
                ReadyMadeOrderDao.UpdateWorkStatusToGoodsOrderRequestByOrderId(id);

                UpdateOrderExpectDate.DoAction(id);

                OrderItem orderItem = OrderDao.FindById(id);
                string title = orderItem.Code + "の商品をただ今より出力に入らせていただきます。";
                MailService.SendOrderMailWithPhoneMail(orderItem, title);
                return true;
            }

            private bool CanProduct(int id)
            {
                IList<PrintMadeOrderItem> printMadeOrderList = PrintMadeOrderDao.FindLivingByOrderId(id);
                foreach (PrintMadeOrderItem item in printMadeOrderList)
                {
                    if (!item.FileChecked()) return false;
                }

                OrderItem orderItem = OrderDao.FindById(id);
                if (!orderItem.PayStatus.Equals("PAID")) return false;

                return true;
            }
        }
    }
}
