using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 클래스의 양방향 연결을 단방향으로 전환하고, 공통으로 필요로 하는 부분이 있다면 클래스 추출을 실시한다.
/// 또는 대리자를 이용하여 중개역할을 하게 만든다.
/// 상위클래스에서 하위클래스를 빼내야 할 경우에는 상속을 위임으로 전환한다.
/// </summary>

namespace RefactoringStudy._17_InappropriateIntimacy
{
    public class OrderService : IOrderService
    {
        //[Autowire]
        //[AdoTemplateName(CommonObjectService.AdoTemplate)]
        //public IOrderDao OrderDao { set; private get; }

        //[Autowire]
        //[AdoTemplateName(CommonObjectService.AdoTemplate)]
        //public IPayInfoDao PayInfoDao { set; private get; }

        //[Autowire]
        //[AdoTemplateName(CommonObjectService.AdoTemplate)]
        //public IDeliveryInfoDao DeliveryInfoDao { set; private get; }

        //[Autowire]
        //[AdoTemplateName(CommonObjectService.AdoTemplate)]
        //public IPrintMadeOrderDao PrintMadeOrderDao { set; private get; }

        //[Autowire]
        //[AdoTemplateName(CommonObjectService.AdoTemplate)]
        //public IReadyMadeOrderDao ReadyMadeOrderDao { set; private get; }

        //[Autowire]
        //[AdoTemplateName(CommonObjectService.AdoTemplate)]
        //public IBankInfoDao BankInfoDao { set; private get; }

        //[Autowire]
        //public IOrderHistoryService OrderHistoryService { set; private get; }

        //[Autowire]
        //public IPrintMadeOrderService PrintMadeOrderService { set; private get; }

        //[Autowire]
        //public IReadyMadeOrderService ReadyMadeOrderService { set; private get; }

        //[Autowire("UpdateOrderExpectDate")]
        //public UpdateOrderExpectDate UpdateOrderExpectDate { set; private get; }

        //[Autowire("UpdateOrderStatusByPrintMadeFileTypeUpdate")]
        //public UpdateOrderStatusByPrintMadeFileTypeUpdate UpdateOrderStatusByPrintMadeFileTypeUpdate { set; private get; }

        //[Autowire]
        //[AdoTemplateName(CommonObjectService.AdoTemplate)]
        //public IOrderChatDao OrderChatDao { set; private get; }

        //[Autowire]
        //public IMailService MailService { set; private get; }

        [Autowire]
        public IPayInfoService PayInfoService { set; private get; }

        //[Autowire]
        //public IPointService PointService { set; private get; }

        //[Autowire]
        //public IIntranetOrderMemoDao IntranetOrderMemoDao { set; private get; }

        //[Autowire]
        //public IOrderChatService OrderChatService { set; private get; }

        //[Autowire]
        //public IPaygentService PaygentService { get; set; }

        //[Autowire]
        //[AdoTemplateName(CommonObjectService.AdoTemplate_JangBoGo)]
        //public IPaygentRequestDao PaygentRequestDao { get; set; }

        //[Autowire]
        //[AdoTemplateName(CommonObjectService.AdoTemplate_AdprintNewDB)]
        //public IEventUserDao EventUserDao { get; set; }

        //[Autowire]
        //[AdoTemplateName(CommonObjectService.AdoTemplate_AdprintNewDB)]
        //public IEventDao EventDao { get; set; }

        //private GoodsOrderInfo GetGoodsOrderInfoWithoutMadeList(int orderId)
        //{
        //    GoodsOrderInfo orderInfo = new GoodsOrderInfo();
        //    OrderItem orderItem = OrderDao.FindById(orderId);
        //    orderInfo.OrderItem = orderItem;
        //    orderInfo.PayInfoItem = PayInfoDao.FindById(orderItem.PayInfoId);
        //    orderInfo.DeliveryInfoItem = DeliveryInfoDao.FindById(orderItem.DeliveryInfoId);

        //    if (orderInfo.PayInfoItem != null)
        //    {
        //        if (orderInfo.PayInfoItem.PayType.Equals("BANK"))
        //        {
        //            orderInfo.BankInfoItem = BankInfoDao.FindById(Int32.Parse(orderInfo.PayInfoItem.BankType));
        //        }
        //    }
        //    return orderInfo;
        //}

        //public GoodsOrderInfo GetLivingGoodsOrderInfo(int orderId)
        //{
        //    GoodsOrderInfo orderInfo = GetGoodsOrderInfoWithoutMadeList(orderId);
        //    orderInfo.PrintMadeList = PrintMadeOrderDao.FindLivingByOrderId(orderId);
        //    orderInfo.ReadyMadeList = ReadyMadeOrderDao.FindLivingByOrderId(orderId);
        //    return orderInfo;
        //}

        //public GoodsOrderInfo GetValidGoodsOrderInfo(int orderId, bool includeCancel = false)
        //{
        //    GoodsOrderInfo orderInfo = GetGoodsOrderInfoWithoutMadeList(orderId);
        //    orderInfo.PrintMadeList = PrintMadeOrderDao.FindValidByOrderId(orderId, includeCancel);
        //    orderInfo.ReadyMadeList = ReadyMadeOrderDao.FindValidByOrderId(orderId, includeCancel);
        //    orderInfo.IntranetOrderMemoList = IntranetOrderMemoDao.FindByOrderId(orderId);
        //    orderInfo.OrderChatList = OrderChatDao.FindByOrderId(orderId);
        //    orderInfo.PaygentRequestItem = PaygentRequestDao.RetrievePaymentRequestLatestItemByOrderId(orderId, "MK");
        //    orderInfo.EventUserItem = EventUserDao.FindByMakumakuOrderCode(orderInfo.OrderItem.Code);
        //    if (orderInfo.EventUserItem != null)
        //    {
        //        orderInfo.EventItem = EventDao.FindById(orderInfo.EventUserItem.EventId);
        //    }
        //    orderInfo.HighPrintOrderItem = OrderDao.FIndByHighPrintId(orderInfo.OrderItem.Id);
        //    return orderInfo;
        //}

        //#region "Deposit Confirm, Cancel"

        //[Transaction()]
        //public void DepositConfirm(int id, string checkerId)
        //{
        //    OrderItem orderItem = OrderDao.FindById(id);
        //    if (orderItem.PayType.StartsWith("TELEGRAM") && orderItem.IsPaid()) return;

        //    OrderHistoryService.InsertOrderHistory(id);
        //    IList<PrintMadeOrderItem> printMadeList = PrintMadeOrderDao.FindByOrderId(id);
        //    foreach (PrintMadeOrderItem item in printMadeList)
        //    {
        //        OrderHistoryService.InsertPrintMadeHistory(item);
        //    }

        //    IList<ReadyMadeOrderItem> readyMadeList = ReadyMadeOrderDao.FindByOrderId(id);
        //    foreach (ReadyMadeOrderItem item in readyMadeList)
        //    {
        //        OrderHistoryService.InsertReadyMadeHistory(item);
        //    }

        //    OrderDao.UpdatePayStatus(id, "PAID");
        //    UpdateOrderStatusToProduct(id);

        //    PayInfoDao.DepositCheck(orderItem.PayInfoId, checkerId);

        //    string title = "[" + orderItem.Code + "]の入金確認が取れましたので次のステップに進みます。";
        //    MailService.SendOrderMailWithPhoneMail(orderItem, title);
        //}

        //public void SetHighPrintOrderToComplete(int id)
        //{
        //    var items = ReadyMadeOrderDao.FindByOrderId(id);
        //    if (!items.Any(t => "R199".Equals(t.ReadyMadeId))) return;

        //    OrderDao.UpdateOrderStatus(id, "COMPLETE");
        //    OrderDao.UpdateReadyMadeOrderWorkStatus(id, "COMPLETE");
        //}

        //public void SetHighPrintOrderToUnComplete(int id)
        //{
        //    var items = ReadyMadeOrderDao.FindByOrderId(id);
        //    if (!items.Any(t => "R199".Equals(t.ReadyMadeId))) return;

        //    OrderDao.UpdateOrderStatus(id, "ORDER_CONFIRM");
        //}

        //[Transaction()]
        //public void DepositCancel(int id)
        //{
        //    OrderHistoryService.RollbackOrderStatus(id);
        //    RollBackPrintMadeStatus(id);
        //    RollBackReadyMadeStatus(id);
        //    PayInfoDao.DepositCheckCancel(id);
        //    ResetCanProductDateToMinDate(id);

        //    OrderItem orderItem = OrderDao.FindById(id);
        //    PayInfoDao.DepositCheckCancel(orderItem.PayInfoId);
        //}

        //#endregion "Deposit Confirm, Cancel"

        //#region "AbroadDelivery Accept, Cancel"

        //[Transaction()]
        //public void AbroadDeliveryAccept(int id, string invoceNumber)
        //{
        //    OrderDao.UpdateOrderStatusToAbroadDeliveryAndInvoiceNumber(id, invoceNumber);

        //    UpdateAbroadDeliveryToPrintMadeOrder(id);
        //    UpdateAbroadDeliveryToReadyMadeOrder(id);
        //}

        //private void UpdateAbroadDeliveryToPrintMadeOrder(int orderId)
        //{
        //    IList<PrintMadeOrderItem> list = PrintMadeOrderDao.FindByOrderId(orderId);

        //    foreach (PrintMadeOrderItem item in list)
        //    {
        //        if ("CANCEL".Equals(item.WorkStatus)) continue;
        //        PrintMadeOrderDao.UpdateWorkStatusToAbroadDelivery(item.Id);
        //    }
        //}

        //private void UpdateAbroadDeliveryToReadyMadeOrder(int orderId)
        //{
        //    IList<ReadyMadeOrderItem> list = ReadyMadeOrderDao.FindByOrderId(orderId);

        //    foreach (ReadyMadeOrderItem item in list)
        //    {
        //        if ("CANCEL".Equals(item.WorkStatus)) continue;
        //        ReadyMadeOrderDao.UpdateWorkStatusToAbroadDelivery(item.Id);
        //    }
        //}

        //[Transaction()]
        //public void AbroadDeliveryCancel(int id)
        //{
        //    OrderDao.UpdateOrderStatusToDeliveryAccept(id);
        //    AbroadDeliveryCancelToPrintMadeOrder(id);
        //    AbroadDeliveryCancelToReadyMadeOrder(id);
        //}

        //private void AbroadDeliveryCancelToPrintMadeOrder(int orderId)
        //{
        //    IList<PrintMadeOrderItem> list = PrintMadeOrderDao.FindByOrderId(orderId);

        //    foreach (PrintMadeOrderItem item in list)
        //    {
        //        PrintMadeOrderDao.RollbackWorkStatusToDeliveryAccept(item.Id);
        //    }
        //}

        //private void AbroadDeliveryCancelToReadyMadeOrder(int orderId)
        //{
        //    IList<ReadyMadeOrderItem> list = ReadyMadeOrderDao.FindByOrderId(orderId);

        //    foreach (ReadyMadeOrderItem item in list)
        //    {
        //        ReadyMadeOrderDao.UpdateWorkStatusToDeliveryAccept(item.Id);
        //    }
        //}

        //#endregion "AbroadDelivery Accept, Cancel"

        //#region HighPrint - 우선출력

        [Transaction]
        public void HighPrintProc(int orderId, int addPrice, string procType)
        {
            DateTime now = DateTime.Now;
            var orderItem = OrderDao.FindById(orderId);
            var payInfo = PayInfoDao.FindById(orderItem.PayInfoId);
            string firstItemType = string.Empty;
            string newOrderCode = string.Empty;
            int firstItemId = 0;
            foreach (var item in PrintMadeOrderDao.FindByOrderId(orderItem.Id))
            {
                firstItemType = "PRINTMADE";
                firstItemId = item.Id;
                break;
            }
            foreach (var item in ReadyMadeOrderDao.FindByOrderId(orderItem.Id))
            {
                firstItemType = "READMADE";
                firstItemId = item.Id;
                break;
            }
            if ("ADDPRICE".Equals(procType))
            {
                if (payInfo.PayType.StartsWith("TELEGRAM") || (orderItem.IsPaid() && !payInfo.PayType.Equals("MAN")))
                {
                    throw BizException.CreateMakumakuServiceException("관리자 우선출력 오류", "PG사 결제나 입금완료주문의 경우\n금액 추가타입의 우선출력 요청은 할 수 없습니다.");
                }

                if ("PRINTMADE".Equals(firstItemType))
                {
                    var madeItem = PrintMadeOrderDao.FindById(firstItemId);
                    int orgPrice = madeItem.TaxAddedTotalPrice;
                    madeItem.ExtraPrice = addPrice;
                    PrintMadeOrderDao.Update(madeItem);
                    DeliveryInfoItem DeliveryInfoItem = DeliveryInfoDao.FindById(orderItem.DeliveryInfoId);
                    PayInfoService.UpdateChangeProductCharge(orderItem.PayInfoId, "MINUS", orgPrice, DeliveryInfoItem);
                    PayInfoService.UpdateChangeProductCharge(orderItem.PayInfoId, "PLUS", madeItem.TaxAddedTotalPrice, DeliveryInfoItem);
                }
                else if ("READYMADE".Equals(firstItemType))
                {
                    var madeItem = ReadyMadeOrderDao.FindById(firstItemId);
                    int orgPrice = madeItem.TaxAddedTotalPrice;
                    madeItem.ExtraPrice = addPrice;
                    ReadyMadeOrderDao.Update(madeItem);
                    DeliveryInfoItem DeliveryInfoItem = DeliveryInfoDao.FindById(orderItem.DeliveryInfoId);
                    PayInfoService.UpdateChangeProductCharge(orderItem.PayInfoId, "MINUS", orgPrice, DeliveryInfoItem);
                    PayInfoService.UpdateChangeProductCharge(orderItem.PayInfoId, "PLUS", madeItem.TaxAddedTotalPrice, DeliveryInfoItem);
                }
            }
            else if ("NEWGOODS".Equals(procType))
            {
                var newItemDoc = XMLUtil.CreateDocment();
                XMLUtil.SetValue(newItemDoc, "HIGHPRINT", orderItem.Code, "PARENTCODE");

                var newItem = new OrderItem();
                newItem.UserId = orderItem.UserId;
                newItem.OrderDate = now;
                newItem.OrderStatus = "DESIGN_INFO";
                newItem.PayStatus = "UNPAID";
                newItem.HighPrintId = orderItem.Id;
                newItem.Content = newItemDoc;
                var newOrderId = OrderDao.Insert(newItem);
                OrderDao.UpdateOrderStatus(newOrderId, "DESIGN_INFO");
                newItem = OrderDao.FindById(newOrderId);
                newOrderCode = newItem.Code;
                var rdItem = new ReadyMadeOrderItem();
                rdItem.ReadyMadeId = "R199";
                rdItem.Title = orderItem.Code + "追加/商品金額";
                rdItem.OrderId = newItem.Id;
                rdItem.Code = newItem.Code + "-S01";
                rdItem.OrderCode = newItem.Code;
                rdItem.OrderCount = 1;
                rdItem.TotalPrice = 0;
                rdItem.GoodsPrice = addPrice;

                ReadyMadeOrderDao.Insert(rdItem);
            }

            PrintMadeOrderDao.ChangePrintPriorityByOrderCode(orderItem.Code, "HIGH");

            DbParam param = new DbParam();
            var doc = XMLUtil.CreateDocment();
            XMLUtil.SetValue(doc, "PARENTHIGHPRINT", procType, "PROCTYPE");
            XMLUtil.SetValue(doc, "PARENTHIGHPRINT", addPrice, "ADDPRICE");
            if ("NEWGOODS".Equals(procType))
            {
                XMLUtil.SetValue(doc, "PARENTHIGHPRINT", newOrderCode, "NEWORDERCODE");
            }
            param.Add("content", doc);
            OrderDao.Update(param, orderItem.Id);
        }

        [Transaction]
        public void HighPrintCancel(int orderId)
        {
            var orderItem = OrderDao.FindById(orderId);
            string procType = XMLUtil.GetString(orderItem.Content, "PARENTHIGHPRINT", "PROCTYPE");
            int addPrice = XMLUtil.GetInt32(orderItem.Content, "PARENTHIGHPRINT", "ADDPRICE");
            if ("NEWGOODS".Equals(procType))
            {
                var highOrder = OrderDao.FIndByHighPrintId(orderItem.Id);
                PrintMadeOrderDao.ChangePrintPriorityByOrderCode(orderItem.Code, "");
                var payInfo = PayInfoDao.FindById(highOrder.Id);
                if (payInfo != null && payInfo.PayType.StartsWith("TELEGRAM"))
                {
                    CancleOrderWithTelegram(highOrder.Id);
                }
                else
                {
                    CancleOrder(highOrder.Id);
                }
            }
            if ("ADDPRICE".Equals(procType))
            {
                string firstItemType = string.Empty;
                int firstItemId = 0;
                foreach (var item in PrintMadeOrderDao.FindByOrderId(orderItem.Id))
                {
                    firstItemType = "PRINTMADE";
                    firstItemId = item.Id;
                    break;
                }
                foreach (var item in ReadyMadeOrderDao.FindByOrderId(orderItem.Id))
                {
                    firstItemType = "READMADE";
                    firstItemId = item.Id;
                    break;
                }

                if ("PRINTMADE".Equals(firstItemType))
                {
                    var madeItem = PrintMadeOrderDao.FindById(firstItemId);
                    int orgPrice = madeItem.TaxAddedTotalPrice;
                    madeItem.ExtraPrice -= addPrice;
                    PrintMadeOrderDao.Update(madeItem);
                    DeliveryInfoItem DeliveryInfoItem = DeliveryInfoDao.FindById(orderItem.DeliveryInfoId);
                    PayInfoService.UpdateChangeProductCharge(orderItem.PayInfoId, "MINUS", orgPrice, DeliveryInfoItem);
                    PayInfoService.UpdateChangeProductCharge(orderItem.PayInfoId, "PLUS", madeItem.TaxAddedTotalPrice, DeliveryInfoItem);
                }
                else if ("READYMADE".Equals(firstItemType))
                {
                    var madeItem = ReadyMadeOrderDao.FindById(firstItemId);
                    int orgPrice = madeItem.TaxAddedTotalPrice;
                    madeItem.ExtraPrice -= addPrice;
                    ReadyMadeOrderDao.Update(madeItem);
                    DeliveryInfoItem DeliveryInfoItem = DeliveryInfoDao.FindById(orderItem.DeliveryInfoId);
                    PayInfoService.UpdateChangeProductCharge(orderItem.PayInfoId, "MINUS", orgPrice, DeliveryInfoItem);
                    PayInfoService.UpdateChangeProductCharge(orderItem.PayInfoId, "PLUS", madeItem.TaxAddedTotalPrice, DeliveryInfoItem);
                }
            }

            DbParam param = new DbParam();
            param.Add("content", new XDocument());
            OrderDao.Update(param, orderItem.Id);
        }

        //public int ChangePrintPriorityByOrderCode(string orderCode, string priority)
        //{
        //    return PrintMadeOrderDao.ChangePrintPriorityByOrderCode(orderCode, priority);
        //}

        //#endregion HighPrint - 우선출력

        //private void RollBackPrintMadeStatus(int orderId)
        //{
        //    IList<PrintMadeOrderItem> printMadeList = PrintMadeOrderDao.FindByOrderId(orderId);
        //    foreach (PrintMadeOrderItem item in printMadeList)
        //    {
        //        OrderHistoryService.RollbackPrintMadeStatus(item.Id);
        //    }
        //}

        //private void RollBackReadyMadeStatus(int orderId)
        //{
        //    IList<ReadyMadeOrderItem> readyMadeList = ReadyMadeOrderDao.FindByOrderId(orderId);
        //    foreach (ReadyMadeOrderItem item in readyMadeList)
        //    {
        //        OrderHistoryService.RollbackReadyMadeStatus(item.Id);
        //    }
        //}

        //public int InsertOrder(string userId)
        //{
        //    OrderItem orderItem = new OrderItem();
        //    orderItem.UserId = userId;
        //    int orderId = OrderDao.Insert(orderItem);
        //    return orderId;
        //}

        //public string GetOrderCode(int orderId)
        //{
        //    OrderItem item = OrderDao.FindById(orderId);
        //    return item.Code;
        //}

        //public OrderItem CreateNewOrderItem(string userId)
        //{
        //    OrderItem orderItem = new OrderItem();
        //    orderItem.UserId = userId;
        //    int orderId = OrderDao.Insert(orderItem);
        //    return OrderDao.FindById(orderId);
        //}

        //public BasicOrderParam GetBasicOrderParam(int orderId)
        //{
        //    return GetBasicOrderParamByType(orderId, "LIVING");
        //}

        //public BasicOrderParam GetBasicOrderParamWithComplete(int orderId)
        //{
        //    return GetBasicOrderParamByType(orderId, "ALL");
        //}

        //private BasicOrderParam GetBasicOrderParamByType(int orderId, string type)
        //{
        //    IList<ReadyMadeOrderItem> readyMadeOrderList = GetReaderMadeOrderListByType(orderId, type);
        //    IList<PrintMadeOrderItem> printMadeOrderList = GetPrintMadeOrderListByType(orderId, type);
        //    OrderItem orderItem = OrderDao.FindById(orderId);

        //    BasicOrderParam basicOrderParam = new BasicOrderParam();
        //    basicOrderParam.PrintMadeOrderList = printMadeOrderList;
        //    basicOrderParam.ReadyMadeOrderList = readyMadeOrderList;
        //    basicOrderParam.OrderItem = orderItem;

        //    return basicOrderParam;
        //}

        //private IList<ReadyMadeOrderItem> GetReaderMadeOrderListByType(int orderId, string type)
        //{
        //    if (type.Equals("LIVING")) return ReadyMadeOrderDao.FindLivingByOrderId(orderId);
        //    return ReadyMadeOrderDao.FindCompleteListByOrderId(orderId);
        //}

        //private IList<PrintMadeOrderItem> GetPrintMadeOrderListByType(int orderId, string type)
        //{
        //    if (type.Equals("LIVING")) return PrintMadeOrderDao.FindLivingByOrderId(orderId);
        //    return PrintMadeOrderDao.FindCompleteListByOrderId(orderId);
        //}

        //public int GetTaxAddedPrice(int orderId)
        //{
        //    IList<ReadyMadeOrderItem> readyMadeOrderList = ReadyMadeOrderDao.FindValidByOrderId(orderId);
        //    IList<PrintMadeOrderItem> printMadeOrderList = PrintMadeOrderDao.FindValidByOrderId(orderId);
        //    int readyMadeOrderPrice = readyMadeOrderList.Sum(item => item.TaxAddedTotalPrice);
        //    int printMadeOrderPrice = printMadeOrderList.Sum(item => item.TaxAddedTotalPrice);

        //    return readyMadeOrderPrice + printMadeOrderPrice;
        //}

        //public int GetTotalPrice(int orderId)
        //{
        //    PayInfoParam param = new PayInfoParam();

        //    IList<ReadyMadeOrderItem> readyMadeOrderList = ReadyMadeOrderDao.FindValidByOrderId(orderId);
        //    IList<PrintMadeOrderItem> printMadeOrderList = PrintMadeOrderDao.FindValidByOrderId(orderId);
        //    int readyMadeOrderPrice = readyMadeOrderList.Sum(item => item.TotalPrice);
        //    int printMadeOrderPrice = printMadeOrderList.Sum(item => item.TotalPrice);

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

        //[Transaction()]
        //public void ResetCanProductDateToMinDate(int id)
        //{
        //    OrderDao.UpdateCanProductDateToMinDate(id);
        //    PrintMadeOrderDao.UpdateProductRequestDateToMinDate(id);
        //    ReadyMadeOrderDao.UpdateProductRequestDateToMinDate(id);
        //}

        //public bool CanProduct(int id)
        //{
        //    IList<PrintMadeOrderItem> printMadeOrderList = PrintMadeOrderDao.FindLivingByOrderId(id);
        //    foreach (PrintMadeOrderItem item in printMadeOrderList)
        //    {
        //        if (!item.FileChecked()) return false;
        //    }

        //    OrderItem orderItem = OrderDao.FindById(id);
        //    if (!orderItem.PayStatus.Equals("PAID")) return false;

        //    return true;
        //}

        //[Transaction()]
        //public bool UpdateOrderStatusToProduct(int id)
        //{
        //    if (!CanProduct(id)) return false;
        //    OrderDao.UpdateOrderStatusToProduct(id);
        //    PrintMadeOrderDao.UpdateWorkStatusToGoodsOrderRequestByOrderId(id);
        //    ReadyMadeOrderDao.UpdateWorkStatusToGoodsOrderRequestByOrderId(id);

        //    UpdateOrderExpectDate.DoAction(id);

        //    OrderItem orderItem = OrderDao.FindById(id);
        //    string title = orderItem.Code + "の商品をただ今より出力に入らせていただきます。";
        //    MailService.SendOrderMailWithPhoneMail(orderItem, title);
        //    return true;
        //}

        //public OrderItem FindByMadeOrder(int madeOrderId, string orderType)
        //{
        //    if (orderType.Equals("PRINTMADE"))
        //    {
        //        return GetOrderItemByPrintMadeOrderId(madeOrderId);
        //    }
        //    return GetOrderItemByReadyMadeOrderId(madeOrderId);
        //}

        //private OrderItem GetOrderItemByPrintMadeOrderId(int printMadeOrderId)
        //{
        //    PrintMadeOrderItem item = PrintMadeOrderDao.FindById(printMadeOrderId);
        //    return OrderDao.FindById(item.OrderId);
        //}

        //private OrderItem GetOrderItemByReadyMadeOrderId(int readyMadeOrderId)
        //{
        //    ReadyMadeOrderItem item = ReadyMadeOrderDao.FindById(readyMadeOrderId);
        //    return OrderDao.FindById(item.OrderId);
        //}

        //public void UpdateOrderStatusByPrintMadeFileType(int id)
        //{
        //    UpdateOrderStatusByPrintMadeFileTypeUpdate.DoAction(id);
        //}

        //[Transaction()]
        //public void CancleOrder(int id)
        //{
        //    string workStatus = "CANCEL";

        //    OrderDao.UpdateOrderStatus(id, workStatus);
        //    PrintMadeOrderDao.UpdateWorkStatusByOrderId(id, workStatus);
        //    ReadyMadeOrderDao.UpdateWorkStatusByOrderId(id, workStatus);

        //    OrderItem orderItem = OrderDao.FindById(id);
        //    if (orderItem.PayInfoId == 0) return;
        //    PayInfoItem payInfoItem = PayInfoDao.FindById(orderItem.PayInfoId);
        //    SetCancelOrderProc(orderItem.Code, orderItem, payInfoItem, payInfoItem.TotalCharge);
        //}

        //[Transaction()]
        //public void CancleOrderWithTelegram(int id)
        //{
        //    var paygentRequestItem = PaygentRequestDao.RetrievePaymentRequestLatestItemByOrderId(id, "MK");
        //    Paygent_TELEGRAM_KIND telegramKind = Paygent_TELEGRAM_KIND.TELEGRAM_CARD_REQUEST;

        //    if (!Enum.TryParse<Paygent_TELEGRAM_KIND>(paygentRequestItem.Telegram_kind_code, out telegramKind))
        //    {
        //        CancleOrder(id);
        //        return;
        //    }
        //    if (telegramKind != Paygent_TELEGRAM_KIND.TELEGRAM_CARD_REQUEST)
        //    {
        //        CancleOrder(id);
        //        return;
        //    }

        //    var telegram = new PaygentTelegramRequest_DEFAULT();

        //    // Paygent로 부터 현재 상태 조회
        //    PaygentResult Result = PaygentService.ProcPaygentTelegramRequestManual(paygentRequestItem, Paygent_TELEGRAM_KIND.TELEGRAM_GENERAL_PAYMENT_INFO_SEARCH, telegram.ToRequestDictionary());
        //    if (Result.Result != PaygentResultStatus.SUCCESS)
        //    {
        //        if ("13001".Equals(Result.Response?.RESPONSE_HEADER_CODE)) // 해당 결제 정보가 존재하지 않습니다.
        //        {
        //            // 전표처리 하지 않는다.
        //            CancleOrder(id);
        //            return;
        //        }
        //        else
        //        {
        //            throw BizException.CreateMakumakuServiceException("エラーが発生しました。", "PG社から決済システムの応答がありません。\nしばらくお待ちになってから、もう一度行なってください。\nこの問題が続く場合、お手数ですが顧客センターまでご連絡お願いいたします。\n顧客センター（050-8882-5225）" + (Result.Response?.RESPONSE_HEADER_CODE ?? "."));
        //        }
        //    }

        //    PaygentResult cancelResult = null;
        //    string pg_payment_status = (Result.Response?.RESPONSE_DICTIONARY_LIST.FirstOrDefault()?["payment_status"] ?? "").ToString();
        //    switch (paygentRequestItem.LatestPaymentStatus)
        //    {
        //        case "":
        //            throw BizException.CreateMakumakuServiceException("エラーが発生しました。", "決済代行会社からお支払い情報を確認しています。しばらくお待ち頂いてから、再度お試しください。");
        //        case "10":
        //        case "11":
        //            // 전표처리 하지 않는다.
        //            break;

        //        case "20":
        //            if (!pg_payment_status.Equals(paygentRequestItem.LatestPaymentStatus)) throw BizException.CreateMakumakuServiceException("ORDCAN0004", "決済代行会社からお支払い情報を確認しています。しばらくお待ち頂いてから、再度お試しください。");
        //            cancelResult = PaygentService.ProcPaygentTelegramRequestManual(paygentRequestItem, Paygent_TELEGRAM_KIND.TELEGRAM_CARD_CANCEL, telegram.ToRequestDictionary());
        //            break;

        //        case "40":
        //            if (!pg_payment_status.Equals(paygentRequestItem.LatestPaymentStatus)) throw BizException.CreateMakumakuServiceException("ORDCAN0004", "決済代行会社からお支払い情報を確認しています。しばらくお待ち頂いてから、再度お試しください。");
        //            cancelResult = PaygentService.ProcPaygentTelegramRequestManual(paygentRequestItem, Paygent_TELEGRAM_KIND.TELEGRAM_CARD_SALES_CANCEL, telegram.ToRequestDictionary());
        //            break;

        //        default:
        //            // 그 외, 취소할 수 없는 상태 상세 메시지를 얻기 위해서 PG사로 전표 취소요청후 메시지를 응답 받는다.
        //            cancelResult = PaygentService.ProcPaygentTelegramRequestManual(paygentRequestItem, Paygent_TELEGRAM_KIND.TELEGRAM_CARD_SALES_CANCEL, telegram.ToRequestDictionary());
        //            break;
        //    }

        //    if (cancelResult != null && cancelResult.Result != PaygentResultStatus.SUCCESS)
        //    {
        //        string addMsg = cancelResult.Result.ToString();
        //        if (cancelResult.ManagedException != null) { addMsg += cancelResult.ManagedException.Message; }
        //        if (cancelResult.Response != null) { addMsg += cancelResult.Response.RESPONSE_HEADER_CODE; }
        //        throw BizException.CreateMakumakuServiceException("エラーが発生しました。", "PG社から決済システムの応答がありません。\nしばらくお待ちになってから、もう一度行なってください。\nこの問題が続く場合、お手数ですが顧客センターまでご連絡お願いいたします。\n顧客センター（050-8882-5225）" + addMsg);
        //    }

        //    CancleOrder(id);
        //}

        //public void SetUserWriteInfo(int id)
        //{
        //    OrderItem item = OrderDao.FindById(id);
        //    if (item.IsCustomerWriteStatus()) return;

        //    item.UserWriteDate = DateTime.Now;
        //    item.CommentWriteStatus = "CUSTOMER";
        //    OrderDao.Update(item);
        //}

        //public void SetManagerWriteInfo(int id)
        //{
        //    OrderItem item = OrderDao.FindById(id);

        //    item.ManagerWriteDate = DateTime.Now;
        //    item.CommentWriteStatus = "MANAGER";
        //    item.UserReadStatus = "NEW";
        //    item.UserReadDate = SqlDateTime.MinValue;
        //    OrderDao.Update(item);
        //}

        //public void ClearManagerWriteInfo(int id)
        //{
        //    OrderItem item = OrderDao.FindById(id);

        //    item.ManagerWriteDate = DateTime.Now;
        //    item.CommentWriteStatus = "MANAGER";
        //    item.UserReadStatus = "READ";
        //    item.UserReadDate = SqlDateTime.MinValue;
        //    OrderDao.Update(item);
        //}

        //public ListParam<OrderItem> GetOrderCommentListParam(string fieldName, string searchValue, int page, int pageSize, string type)
        //{
        //    int startNum = (page - 1) * pageSize + 1;
        //    int endNum = page * pageSize;
        //    int itemCount = OrderDao.GetTotalCountByCommentWriteStatus(fieldName, searchValue, type);

        //    ListParam<OrderItem> param = new ListParam<OrderItem>();
        //    param.ItemList = OrderDao.FindByCommentWriteStatus(fieldName, searchValue, type, startNum, endNum);
        //    param.FieldSelectList = GetFieldSelectList(fieldName);
        //    param.SearchValue = searchValue;
        //    param.Type = type;

        //    IPager pager = new ButtonPager() { TotalCount = itemCount, CurrentPage = page, PageSize = pageSize, CountOfListPage = BoardUtil.countOfPageList };
        //    param.Pager = pager;
        //    return param;
        //}

        //private SelectList GetFieldSelectList(string fieldName)
        //{
        //    IList<ListItem> list = new List<ListItem>();
        //    list.Add(new ListItem() { Value = "code", Text = "주문번호" });
        //    list.Add(new ListItem() { Value = "userName", Text = "주문자명" });
        //    return new SelectList(list, "Value", "Text", fieldName);
        //}

        //public OrderItem InsertReproductOrderItem(int id)
        //{
        //    OrderItem prevOrderItem = OrderDao.FindById(id);
        //    OrderItem newOrderItem = GetReProductOrderItem(prevOrderItem);

        //    int newOrderId = OrderDao.Insert(newOrderItem);
        //    newOrderItem = OrderDao.FindById(newOrderId);
        //    return newOrderItem;
        //}

        //private OrderItem GetReProductOrderItem(OrderItem prevOrderItem)
        //{
        //    OrderItem item = new OrderItem();
        //    item.UserId = prevOrderItem.UserId;

        //    item.DeliveryInfoId = GetDeliveryInfoIdForReRroduct(prevOrderItem.DeliveryInfoId);
        //    item.PayInfoId = GetPayInfoIdForReProduct(prevOrderItem.PayInfoId);

        //    item.OrderDate = DateTime.Now;
        //    return item;
        //}

        //private int GetDeliveryInfoIdForReRroduct(int prevDeliveryInfoId)
        //{
        //    DeliveryInfoItem item = DeliveryInfoDao.FindById(prevDeliveryInfoId);
        //    item.Id = 0;
        //    return DeliveryInfoDao.Insert(item);
        //}

        //private int GetPayInfoIdForReProduct(int prevPayInfoId)
        //{
        //    PayInfoItem item = PayInfoDao.FindById(prevPayInfoId);
        //    item.Id = 0;
        //    item.ProductCharge = 0;
        //    item.ShippingCharge = 0;
        //    item.ManPaidCharge = 0;
        //    item.CashPrice = 0;
        //    item.PointPaid = 0;
        //    item.BankUserName = "";
        //    item.PayType = "MAN";
        //    item.BankType = "";

        //    return PayInfoDao.Insert(item);
        //}

        //[Transaction()]
        //public void DeletePrintMadeOrder(int id, int printMadeOrderId)
        //{
        //    PrintMadeOrderItem printMadeOrderItem = PrintMadeOrderDao.FindById(printMadeOrderId);
        //    OrderItem orderItem = OrderDao.FindById(id);
        //    PayInfoItem payInfoItem = PayInfoDao.FindById(orderItem.PayInfoId);

        //    int madeOrderPrice = printMadeOrderItem.TaxAddedTotalPrice;

        //    if (HasLivingMadeOrder(id))
        //    {
        //        SetCancelOrderProc(printMadeOrderItem.Code, orderItem, payInfoItem, madeOrderPrice);
        //    }

        //    PrintMadeOrderDao.UpdateWorkStatus(printMadeOrderId, "CANCEL");

        //    if (!HasLivingMadeOrder(id))
        //    {
        //        OrderDao.UpdateOrderStatus(id, "CANCEL");
        //    }
        //    else
        //    {
        //        UpdateOrderStatusToProduct(id);
        //    }
        //}

        //[Transaction()]
        //public void DeletePrintMadeOrderByManager(int id, int printMadeOrderId)
        //{
        //    GoodsOrderInfo orderInfo = GetLivingGoodsOrderInfo(id);
        //    if (orderInfo.HasSingleGoods()) return;

        //    PrintMadeOrderItem printMadeOrderItem = PrintMadeOrderDao.FindById(printMadeOrderId);
        //    OrderItem orderItem = OrderDao.FindById(id);
        //    PayInfoItem payInfoItem = PayInfoDao.FindById(orderItem.PayInfoId);

        //    int madeOrderPrice = printMadeOrderItem.TaxAddedTotalPrice;
        //    SetCancelOrderProc(printMadeOrderItem.Code, orderItem, payInfoItem, madeOrderPrice);
        //    PrintMadeOrderDao.UpdateWorkStatus(printMadeOrderId, "CANCEL");
        //}

        private void SetCancelOrderProc(string code, OrderItem orderItem, PayInfoItem payInfoItem, int reducePrice)
        {
            int returnPoint = 0;
            if (payInfoItem.IsBank() && orderItem.IsPaid())
            {
                returnPoint = PayInfoService.ReducePayInfoWhenDepositChecked(payInfoItem, reducePrice);
            }
            else if (payInfoItem.PayType.StartsWith("TELEGRAM"))
            {
                if (payInfoItem.PayType.Equals("TELEGRAM_CARD_REQUEST") && orderItem.IsPaid())
                {
                    returnPoint = payInfoItem.PointPaid;
                }
                else
                {
                    returnPoint = orderItem.IsPaid() ? payInfoItem.CashPrice : payInfoItem.PointPaid;
                }
            }
            else
            {
                returnPoint = PayInfoService.ReducePayInfoWhenDepositUnChecked(payInfoItem, reducePrice);
            }

            PayInfoDao.Update(payInfoItem);

            if (returnPoint > 0)
            {
                string title = "[" + code + "]注文取り消しによる返金ポイント";
                PointService.AddPoint(title, returnPoint, orderItem.UserId);
            }
        }

        //public bool HasLivingMadeOrder(int id)
        //{
        //    IList<PrintMadeOrderItem> printMadeList = PrintMadeOrderDao.FindLivingByOrderId(id);
        //    IList<ReadyMadeOrderItem> readyMadeList = ReadyMadeOrderDao.FindLivingByOrderId(id);

        //    return (printMadeList.Count > 0 || readyMadeList.Count > 0);
        //}

        //[Transaction()]
        //public void DeleteReadyMadeOrder(int id, int readyMadeOrderId)
        //{
        //    ReadyMadeOrderItem readyMadeOrderItem = ReadyMadeOrderDao.FindById(readyMadeOrderId);
        //    OrderItem orderItem = OrderDao.FindById(id);
        //    PayInfoItem payInfoItem = PayInfoDao.FindById(orderItem.PayInfoId);

        //    int madeOrderPrice = readyMadeOrderItem.TaxAddedTotalPrice;

        //    if (HasLivingMadeOrder(id))
        //    {
        //        SetCancelOrderProc(readyMadeOrderItem.Code, orderItem, payInfoItem, madeOrderPrice);
        //    }

        //    ReadyMadeOrderDao.UpdateWorkStatus(readyMadeOrderId, "CANCEL");
        //    if (!HasLivingMadeOrder(id))
        //    {
        //        OrderDao.UpdateOrderStatus(id, "CANCEL");
        //    }
        //    else
        //    {
        //        UpdateOrderStatusToProduct(id);
        //    }
        //}

        //[Transaction()]
        //public void DeleteReadyMadeOrderByManager(int id, int readyMadeOrderId)
        //{
        //    GoodsOrderInfo orderInfo = GetLivingGoodsOrderInfo(id);
        //    if (orderInfo.HasSingleGoods()) return;

        //    ReadyMadeOrderItem readyMadeOrderItem = ReadyMadeOrderDao.FindById(readyMadeOrderId);
        //    OrderItem orderItem = OrderDao.FindById(id);
        //    PayInfoItem payInfoItem = PayInfoDao.FindById(orderItem.PayInfoId);

        //    int madeOrderPrice = readyMadeOrderItem.TaxAddedTotalPrice;
        //    SetCancelOrderProc(readyMadeOrderItem.Code, orderItem, payInfoItem, madeOrderPrice);
        //    ReadyMadeOrderDao.UpdateWorkStatus(readyMadeOrderId, "CANCEL");
        //}

        //public IList<IMadeOrderItem> FindByExpectGoodsDeliveryDateByPartner(string fieldName, string searchValue, DateTime date, string workStatus, string partnerId, int startNum, int endNum, string orderBy = "maxUserExpectJapanDeliveryDate", string inkType = "", string orderCode = "")
        //{
        //    var list = new List<IMadeOrderItem>();

        //    IList<PrintMadeOrderItem> printMadeList = FindPrintMadeByExpectGoodsDeliveryDateByPartner(fieldName, searchValue, date, workStatus, partnerId, inkType, orderCode);
        //    list.AddRange(printMadeList);

        //    if (inkType == "")
        //    {
        //        IList<ReadyMadeOrderItem> readyMadeList = FindReadyMadeByExpectGoodsDeliveryDateByPartner(fieldName, searchValue, date, workStatus, partnerId, orderCode);
        //        list.AddRange(readyMadeList);
        //    }

        //    return ResultList(list, startNum, endNum, orderBy);
        //}

        //private IList<IMadeOrderItem> ResultList(IList<IMadeOrderItem> list, int startNum, int endNum, string orderBy)
        //{
        //    return list.OrderBy(item => "maxUserExpectJapanDeliveryDate".Equals(orderBy) ? item.MaxUserExpectJapanDeliveryDate : item.ProductRequestDate)
        //               .Where((item, index) => (index + 1) >= startNum && (index + 1) <= endNum)
        //               .ToList();
        //}

        //private IList<ReadyMadeOrderItem> FindReadyMadeByExpectGoodsDeliveryDateByPartner(string fieldName, string searchValue, DateTime date, string workStatus, string partnerId, string orderCode)
        //{
        //    int readyMadeCount = ReadyMadeOrderDao.GetTotalByExpectGoodsDeliveryDateByPartner(fieldName, searchValue, date, workStatus, partnerId);
        //    IList<ReadyMadeOrderItem> readyMadeList
        //        = ReadyMadeOrderDao.FindByExpectGoodsDeliveryDateByPartner(fieldName, searchValue, date, workStatus, partnerId, 1, readyMadeCount, orderCode);

        //    return readyMadeList ?? new List<ReadyMadeOrderItem>();
        //}

        //private IList<PrintMadeOrderItem> FindPrintMadeByExpectGoodsDeliveryDateByPartner(string fieldName, string searchValue, DateTime date, string workStatus, string partnerId, string inkType, string orderCode)
        //{
        //    int printMadeCount = PrintMadeOrderDao.GetTotalByExpectGoodsDeliveryDateByPartner(fieldName, searchValue, date, workStatus, partnerId, inkType);
        //    IList<PrintMadeOrderItem> printMadeList
        //        = PrintMadeOrderDao.FindByExpectGoodsDeliveryDateByPartner(fieldName, searchValue, date, workStatus, partnerId, 1, printMadeCount, inkType, orderCode);

        //    return printMadeList ?? new List<PrintMadeOrderItem>();
        //}

        //public int GetTotalByExpectGoodsDeliveryDateByPartner(string fieldName, string searchValue, DateTime date, string workStatus, string partnerId, string inkType = "", string orderCode = "")
        //{
        //    int printMadeCount = PrintMadeOrderDao.GetTotalByExpectGoodsDeliveryDateByPartner(fieldName, searchValue, date, workStatus, partnerId, inkType, orderCode);
        //    int readyMadeCount = 0;
        //    if (inkType == "")
        //    {
        //        readyMadeCount = ReadyMadeOrderDao.GetTotalByExpectGoodsDeliveryDateByPartner(fieldName, searchValue, date, workStatus, partnerId);
        //    }

        //    return printMadeCount + readyMadeCount;
        //}

        //public int ChangePrintMadeOrderPartnerId(int id, string partnerId)
        //{
        //    return PrintMadeOrderDao.ChangePartnerInfo(id, partnerId);
        //}

        //public int UpdateUserExpectJapanDeliveryDate(string orderCode, DateTime deliveryDateTime)
        //{
        //    int orderResult = OrderDao.UpdateUserExpectJapanDeliveryDate(orderCode, deliveryDateTime);
        //    int printResult = PrintMadeOrderDao.UpdateUserExpectJapanDeliveryDate(orderCode, deliveryDateTime);
        //    int readyResult = ReadyMadeOrderDao.UpdateUserExpectJapanDeliveryDate(orderCode, deliveryDateTime);

        //    return orderResult + printResult + readyResult;
        //}

        //public int UpdateMadeArea(int id)
        //{
        //    IList<string> madeAreaList = new List<string>();

        //    IList<PrintMadeOrderItem> printMadeList = PrintMadeOrderDao.FindByOrderId(id);
        //    IList<ReadyMadeOrderItem> readyMadeList = ReadyMadeOrderDao.FindByOrderId(id);

        //    string madeArea;

        //    foreach (PrintMadeOrderItem item in printMadeList)
        //    {
        //        madeArea = TradeUtil.GetMadeAreaByPartnerId(item.PartnerId);
        //        if (madeAreaList.IndexOf(madeArea) == -1) madeAreaList.Add(madeArea);
        //    }

        //    foreach (ReadyMadeOrderItem item in readyMadeList)
        //    {
        //        madeArea = TradeUtil.GetMadeAreaByPartnerId(item.PartnerId);
        //        if (madeAreaList.IndexOf(madeArea) == -1) madeAreaList.Add(madeArea);
        //    }

        //    OrderDao.UpdateMadeArea(id, GetMadeAreaByAreaList(madeAreaList));

        //    return madeAreaList.Count;
        //}

        //private string GetMadeAreaByAreaList(IList<string> madeAreaList)
        //{
        //    string returnValue = "";
        //    foreach (string value in madeAreaList)
        //    {
        //        if (!string.IsNullOrEmpty(returnValue)) returnValue += ",";
        //        returnValue += value;
        //    }

        //    return returnValue;
        //}

        //public PrintMadeOrderItem FindPrintMadeOrderItemById(int id)
        //{
        //    return PrintMadeOrderDao.FindById(id);
        //}

        //public ReadyMadeOrderItem FindReadyMadeOrderItemById(int id)
        //{
        //    return ReadyMadeOrderDao.FindById(id);
        //}

        //public int ChangeDeliveryPriorityByOrderId(int orderId, string priority)
        //{
        //    return OrderDao.ChangeDeliveryPriorityById(orderId, priority);
        //}

        //public int ChangePrintPriority(int id, string priority)
        //{
        //    return PrintMadeOrderDao.ChangePrintPriority(id, priority);
        //}

        //public int UpdateFileWorkStateForPrintMadeOrderItem(int printMadeOrderItemId, string fileWorkState)
        //{
        //    return PrintMadeOrderDao.Update(new DbParam().Add("fileWorkState", fileWorkState), printMadeOrderItemId);
        //}
    }
}
