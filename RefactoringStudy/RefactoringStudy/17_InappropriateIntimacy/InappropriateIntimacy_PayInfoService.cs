using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringStudy._17_InappropriateIntimacy
{
    public class PayInfoService : IPayInfoService
    {
        #region IPayInfoService 멤버

        public IOrderService OrderService { set; private get; }
        //public IPayInfoDao PayInfoDao { set; private get; }
        //public IOrderDao OrderDao { set; private get; }
        //public IBankInfoDao BankInfoDao { set; private get; }
        //public IPointService PointService { set; private get; }
        //public IDeliveryInfoDao DeliveryInfoDao { set; private get; }
        //public IPaygentService PaygentService { get; set; }

        //public ValidationErrorParam ValidationCheck(OrderItem orderItem, int pointPaid, string payType, string bankType, string bankUserName, DeliveryInfoItem deliveryInfoItem)
        //{
        //    ValidationErrorParam param = new ValidationErrorParam();

        //    if ("BANK".Equals(payType))
        //    {
        //        if (!"1,2".Split(',').Contains(bankType)) param.Add(new ValidationErrorItem() { ErrorTarget = "", ErrorMessage = "振込先口座入力してください。" });
        //        if (string.IsNullOrEmpty(bankUserName)) param.Add(new ValidationErrorItem() { ErrorTarget = "bankUserName", ErrorMessage = "振込先口座入力してください。" });
        //    }

        //    return param;
        //}

        //public ValidationErrorParam ValidationCheckPayInfoInsert(OrderItem orderItem)
        //{
        //    ValidationErrorParam param = new ValidationErrorParam();
        //    PayInfoItem payInfoItem = GetPayInfoItem(orderItem.Id);
        //    if (payInfoItem.Id > 0)
        //    {
        //        param.Add(new ValidationErrorItem() { ErrorTarget = "", ErrorMessage = "処理できません - すでに処理が完了しています。" });
        //    }
        //    return param;
        //}

        public ValidationErrorParam ValidationCheckCanPayInfoChange(OrderItem orderItem, int pointPaid, string payType, string bankType, string bankUserName, DeliveryInfoItem deliveryInfoItem)
        {
            ValidationErrorParam param = new ValidationErrorParam();
            var oldPayInfoItem = GetPayInfoItem(orderItem.PayInfoId);
            var newPayInfoItem = GetPayInfoItem(orderItem, pointPaid, payType, bankType, bankUserName, TradeUtil.GetShippingCharge(OrderService.GetTaxAddedPrice(orderItem.Id), deliveryInfoItem));
            if (orderItem.PayType.StartsWith("TELEGRAM") && !orderItem.PayType.Equals(payType))
            {
                param.Add(new ValidationErrorItem() { ErrorTarget = "", ErrorMessage = "決済代行サービス(株式会社PAYGENT)ご利用頂いた場合、決済内容は変更頂けません。" });
            }
            bool payChanged = oldPayInfoItem.TotalCharge != newPayInfoItem.TotalCharge;
            if (payChanged && ((!orderItem.PayType.Equals("MAN") && orderItem.IsPaid()) || orderItem.PayType.StartsWith("TELEGRAM")))
            {
                param.Add(new ValidationErrorItem() { ErrorTarget = "", ErrorMessage = "決済代行サービス(株式会社PAYGENT)ご利用頂いた場合、決済金額等の修正は出来ません。\n※変更がある場合は、別途お問い合わせ下さい。" });
            }
            return param;
        }

        //[Transaction()]
        //public void SetPayInfo(OrderItem orderItem, int pointPaid, string payType, string bankType, string bankUserName, int shippingCharge)
        //{
        //    int payInfoId = orderItem.PayInfoId;
        //    PayInfoItem payInfoItem = GetPayInfoItem(orderItem, pointPaid, payType, bankType, bankUserName, shippingCharge);

        //    if (payInfoId == 0)
        //    {
        //        payInfoId = InsertPayInfo(orderItem, payInfoId, payInfoItem);
        //    }
        //    else
        //    {
        //        UpdatePayInfo(orderItem, payInfoId, payInfoItem);
        //    }
        //}

        //[Transaction()]
        //public void SetPayInfo(OrderItem orderItem, int pointPaid, string payType, string bankType, string bankUserName, DeliveryInfoItem deliveryInfoItem)
        //{
        //    int payInfoId = orderItem.PayInfoId;

        //    int shippingCharge = GetShippingCharge(orderItem, deliveryInfoItem);

        //    PayInfoItem payInfoItem = GetPayInfoItem(orderItem, pointPaid, payType, bankType, bankUserName, shippingCharge);
        //    if (payInfoId == 0)
        //    {
        //        payInfoId = InsertPayInfo(orderItem, payInfoId, payInfoItem);
        //    }
        //    else
        //    {
        //        UpdatePayInfo(orderItem, payInfoId, payInfoItem);
        //    }
        //}

        //[Transaction()]
        //public void SetPayInfoTelegram(OrderItem orderItem, int pointPaid, string payType, string bankType, string bankUserName, DeliveryInfoItem deliveryInfoItem, IDictionary<string, string> dic)
        //{
        //    int payInfoId = orderItem.PayInfoId;
        //    int shippingCharge = GetShippingCharge(orderItem, deliveryInfoItem);

        //    PayInfoItem payInfoItem = GetPayInfoItem(orderItem, pointPaid, payType, bankType, bankUserName, shippingCharge);
        //    payInfoId = InsertPayInfo(orderItem, payInfoId, payInfoItem);

        //    if (payType.StartsWith("TELEGRAM"))
        //    {
        //        TelegramProcess(payInfoItem, orderItem, dic);
        //    }
        //}

        //[Transaction()]
        //public void SetPayInfoTelegram_UPDATE(OrderItem orderItem, int pointPaid, string payType, string bankType, string bankUserName, DeliveryInfoItem deliveryInfoItem, IDictionary<string, string> dic)
        //{
        //    int payInfoId = orderItem.PayInfoId;
        //    string oldPayType = orderItem.PayType;
        //    int shippingCharge = GetShippingCharge(orderItem, deliveryInfoItem);

        //    PayInfoItem payInfoItem = GetPayInfoItem(orderItem, pointPaid, payType, bankType, bankUserName, shippingCharge);
        //    UpdatePayInfo(orderItem, payInfoId, payInfoItem);

        //    if (payType.StartsWith("TELEGRAM") && !oldPayType.Equals(payType))
        //    {
        //        TelegramProcess(payInfoItem, orderItem, dic);
        //        OrderDao.UpdatePayStatus(orderItem.Id, "UNPAID");
        //    }
        //    else if (payType.Equals("POINT") || payType.Equals("MAN"))
        //    {
        //        OrderDao.UpdatePayStatus(orderItem.Id, "PAID");
        //    }
        //    else
        //    {
        //        OrderDao.UpdatePayStatus(orderItem.Id, "UNPAID");
        //    }
        //}

        //private void TelegramProcess(PayInfoItem payInfoItem, OrderItem orderItem, IDictionary<string, string> dic)
        //{
        //    // 요청
        //    Paygent_TELEGRAM_KIND telegramType = (Paygent_TELEGRAM_KIND)Enum.Parse(typeof(Paygent_TELEGRAM_KIND), payInfoItem.PayType);
        //    var Result = this.PaygentService.ProcPaygentTelegramPaymentRequest(orderItem.Id, payInfoItem.CashPrice, telegramType, this.TelegramProcess_GetParamDic(orderItem, telegramType, dic));

        //    // 응답 처리
        //    if (Result.Result != PaygentResultStatus.SUCCESS)
        //    {
        //        switch (Result.Result)
        //        {
        //            case PaygentResultStatus.ERROR_PARAM:
        //                string propertyName = PaygentUtil.GetPropertyNameFromRESPONSE_HEADER_DETAIL(Result.Response.RESPONSE_HEADER_DETAIL);
        //                string propertyNameJP = PaygentUtil.GetPaygentRequestTelegramDescription(propertyName, telegramType);
        //                throw BizException.CreateMakumakuServiceException(propertyName, propertyNameJP + "を確認してください。(" + Result.Response.RESPONSE_HEADER_CODE + ")");
        //            case PaygentResultStatus.ERROR_CARD:
        //                throw BizException.CreateMakumakuServiceException(PaygentResultStatus.ERROR_PARAM.ToString(), Result.Response.RESPONSE_HEADER_DETAIL + " (" + Result.Response.RESPONSE_HEADER_CODE + ")");
        //            default:
        //                if (Result.ManagedException != null)
        //                {
        //                    throw BizException.CreateMakumakuServiceException(PaygentResultStatus.ERROR_PARAM.ToString(), "PG社から決済システムの応答がありません。\nしばらくお待ちになってから、もう一度行なってください。" + " (" + Result.Response.RESPONSE_HEADER_CODE + ")", Result.ManagedException);
        //                }
        //                else
        //                {
        //                    throw BizException.CreateMakumakuServiceException(PaygentResultStatus.ERROR_PARAM.ToString(), "PG社から決済システムの応答がありません。\nしばらくお待ちになってから、もう一度行なってください。" + " (" + Result.Response.RESPONSE_HEADER_CODE + ")");
        //                }
        //        }
        //    }
        //}

        //private IDictionary TelegramProcess_GetParamDic(OrderItem orderItem, Paygent_TELEGRAM_KIND telegramType, IDictionary<string, string> dic)
        //{
        //    DateTime executeTime = DateTime.Now;

        //    PaygentTelegramRequest req = null;
        //    string payment_detail = orderItem.Code;
        //    string payment_detail_kana = orderItem.Code;
        //    string payment_limit_date = executeTime.AddDays(20).ToString("yyyyMMdd");
        //    string customer_name = dic["customer_name"];
        //    string customer_family_name = dic["customer_family_name"];
        //    if (telegramType == Paygent_TELEGRAM_KIND.TELEGRAM_ATM_REQUEST)
        //    {
        //        req = new PaygentTelegramRequest_ATM()
        //        {
        //            customer_name = orderItem.UserName,
        //            customer_name_kana = orderItem.UserId,
        //            payment_detail = payment_detail,
        //            payment_detail_kana = payment_detail_kana,
        //            payment_limit_date = payment_limit_date
        //        };
        //    }
        //    else if (telegramType == Paygent_TELEGRAM_KIND.TELEGRAM_CARD_REQUEST)
        //    {
        //        req = new PaygentTelegramRequest_DEFAULT()
        //        {
        //            //card_conf_number = JangBoGo.Security.Cryptography.RSADecrypt(dic["card_conf_number"]),
        //            //card_number = JangBoGo.Security.Cryptography.RSADecrypt(dic["card_number"]).FullToHalf().Replace("-", ""),
        //            //card_valid_term = JangBoGo.Security.Cryptography.RSADecrypt(dic["card_valid_term"]),
        //            //payment_detail = payment_detail,
        //            //payment_detail_kana = payment_detail_kana,
        //            //payment_class = (Paygent_CARD_PAYMENT_CLASS)Enum.Parse(typeof(Paygent_CARD_PAYMENT_CLASS), dic["payment_class"]),
        //            //split_count = dic["split_count"]
        //        };
        //    }
            //else if (telegramType == Paygent_TELEGRAM_KIND.TELEGRAM_KONBINI_N_REQUEST)
            //{
            //    var cvsCompanyId = (Paygent_KONBINI_PAYMENT_CVS_COMPANY_ID)Enum.Parse(typeof(Paygent_KONBINI_PAYMENT_CVS_COMPANY_ID), dic["cvs_company_id"]);
            //    string cvsType = string.Empty;
            //    switch (cvsCompanyId)
            //    {
            //        case Paygent_KONBINI_PAYMENT_CVS_COMPANY_ID.SEICO:
            //            cvsType = "01";
            //            break;

            //        case Paygent_KONBINI_PAYMENT_CVS_COMPANY_ID.LAWSON:
            //        case Paygent_KONBINI_PAYMENT_CVS_COMPANY_ID.MINISTOP:
            //        case Paygent_KONBINI_PAYMENT_CVS_COMPANY_ID.CIRCLEK:
            //        case Paygent_KONBINI_PAYMENT_CVS_COMPANY_ID.DAILY_YAMAZAKI:
            //        case Paygent_KONBINI_PAYMENT_CVS_COMPANY_ID.SUNKUS:
            //            cvsType = "02";
            //            break;

            //        case Paygent_KONBINI_PAYMENT_CVS_COMPANY_ID.SEJ:
            //            cvsType = "03";
            //            break;

            //        case Paygent_KONBINI_PAYMENT_CVS_COMPANY_ID.FAMILY:
            //            cvsType = "04";
            //            break;
            //    }
            //    var item = new PaygentTelegramRequest_KONBINI_N()
            //    {
            //        cvs_type = cvsType,
            //        customer_name = customer_name,
            //        customer_family_name = customer_family_name,
            //        customer_tel = dic["customer_tel"],
            //        site_info = "Makumaku",
            //        payment_limit_date = payment_limit_date,
            //        cvs_company_id = cvsCompanyId,
            //        sales_type = dic["sales_type"],
            //        ticket_start_date = executeTime.ToString("yyyyMMdd"),
            //        ticket_end_date = payment_limit_date,
            //        service_type = Paygent_KONBINI_PAYMENT_SERVICE_TYPE.NONE,
            //        ticket_num = 1
            //    };
            //    item.customer_notice[0] = string.Empty;
            //    item.tNNmaster[0] = string.Empty;
            //    item.tNNtemplate[0] = string.Empty;
            //    item.tNN_fieldNN[0, 0] = orderItem.Code;
            //    item.tNN_fieldNN_align[0, 0] = "1";
            //    req = item;
            //}
            //else if (telegramType == Paygent_TELEGRAM_KIND.TELEGRAM_BANK_ASP_PAYMENT_REQUEST)
            //{
            //    req = new PaygentTelegramRequest_BANK_ASP()
            //    {
            //        claim_kanji = payment_detail,
            //        claim_kana = payment_detail_kana,
            //        customer_name = orderItem.UserName,
            //        customer_name_kana = orderItem.UserId,
            //        copy_right = "Makumaku",
            //        banner_url = "",
            //        free_memo = "Makumaku"
            //    };
            //}

            //var rtn = req.ToRequestDictionary();

            //foreach (var kv in dic)
            //{
            //    if (!rtn.Contains(kv.Key))
            //    {
            //        rtn[kv.Key] = kv.Value;
            //    }
            //}

        //    return rtn;
        //}

        private int GetShippingCharge(OrderItem orderItem, DeliveryInfoItem deliveryInfoItem)
        {
            if (orderItem.HighPrintId > 0) return 0;
            return TradeUtil.GetShippingCharge(OrderService.GetTaxAddedPrice(orderItem.Id), deliveryInfoItem);
        }

        private void UpdatePayInfo(OrderItem orderItem, int payInfoId, PayInfoItem payInfoItem)
        {
            PayInfoItem prevPayInfoItem = PayInfoDao.FindById(payInfoId);
            if (prevPayInfoItem.PointPaid > 0)
            {
                string title = "[" + orderItem.Code + "]支払方法変更による返金ポイント";
                PointService.AddPoint(title, prevPayInfoItem.PointPaid, orderItem.UserId);
            }

            payInfoItem.Id = payInfoId;

            if (!orderItem.OrderStatus.Equals("ORDER_CONFIRM"))
            {
                OrderDao.UpdateOrderStatus(orderItem.Id, "PAY_INFO");
            }

            if (payInfoItem.IsMan() || (payInfoItem.PayType.Equals("TELEGRAM_CARD_REQUEST") && orderItem.IsPaid()))
            {
                OrderDao.UpdatePayStatus(orderItem.Id, "PAID");
            }
            else if (payInfoItem.IsBank() && (prevPayInfoItem.IsMan() || prevPayInfoItem.IsBank() || (payInfoItem.PayType.Equals("TELEGRAM_CARD_REQUEST") && !orderItem.IsPaid())))
            {
                OrderDao.UpdatePayStatus(orderItem.Id, "UNPAID");
            }

            PayInfoDao.Update(payInfoItem);

            if (payInfoItem.PointPaid > 0)
            {
                SubtractPoint(orderItem, payInfoItem);
            }
            OrderService.UpdateOrderStatusToProduct(orderItem.Id);
        }

        //private int InsertPayInfo(OrderItem orderItem, int payInfoId, PayInfoItem payInfoItem)
        //{
        //    payInfoId = PayInfoDao.Insert(payInfoItem);
        //    orderItem.PayInfoId = payInfoId;
        //    OrderDao.UpdatePayInfo(orderItem);
        //    if (payInfoItem.PointPaid > 0)
        //    {
        //        SubtractPoint(orderItem, payInfoItem);
        //    }
        //    return payInfoId;
        //}

        //private void SubtractPoint(OrderItem orderItem, PayInfoItem payInfoItem)
        //{
        //    string title = "[" + orderItem.Code + "]ポイント支払い";
        //    PointService.SubtractPoint(title, payInfoItem.PointPaid, orderItem.UserId);
        //}

        internal PayInfoItem GetPayInfoItem(OrderItem orderItem, int pointPaid, string payType, string bankType, string bankUserName, int shippingCharge)
        {
            PayInfoItem item = new PayInfoItem();
            item.ProductCharge = OrderService.GetTaxAddedPrice(orderItem.Id);
            item.ShippingCharge = shippingCharge;
            item.PayType = payType;
            if (item.PayType.Equals("MAN")) item.ManPaidCharge = TradeUtil.ManPaidCharge(item.ProductCharge + item.ShippingCharge - pointPaid);
            item.PointPaid = pointPaid;
            item.TelegramCommision = TradeUtil.GetTelegramCommision(item.PayType, item.TotalChargeBanTelegramCommision);
            item.CashPrice = item.TotalCharge - pointPaid;
            if (item.PayType.Equals("BANK"))
            {
                item.BankType = bankType;
                string modifiedBankUserName;
                if (item.BankType.Equals("1"))
                {
                    modifiedBankUserName = TradeUtil.HalfToFull(bankUserName);
                }
                else
                {
                    modifiedBankUserName = TradeUtil.FullToHalf(bankUserName);
                }
                item.BankUserName = modifiedBankUserName;
            }
            item.UserId = orderItem.UserId;
            return item;
        }

        //지울꺼
        public PayInfoParam GetPayInfoParam(OrderItem orderItem, UserItem userItem, bool fromOrderConfirm)
        {
            DeliveryInfoItem deliveryInfoItem = DeliveryInfoDao.FindById(orderItem.DeliveryInfoId);

            PayInfoParam param = new PayInfoParam();
            param.OrderItem = orderItem;
            param.DeliveryInfoItem = deliveryInfoItem;
            param.UserItem = userItem;
            param.PayInfoItem = GetPayInfoItem(param.OrderItem.PayInfoId);
            if (param.DeliveryInfoItem.IsProxy == 1)
            {
                param.PayInfoItem.PayType = "BANK";
            }
            param.BankInfoSelectList = GetBankInfoSelectList(param.PayInfoItem.BankType);
            OrderService.SetPriceInPayInfoParam(orderItem.Id, param);

            param.FromOrderConfirm = fromOrderConfirm;
            return param;
        }

        public PayInfoParam GetPayInfoParamBeforeDeliveryInfoInsert(OrderItem orderItem, UserItem userItem, bool fromOrderConfirm)
        {
            DeliveryInfoItem deliveryInfoItem = DeliveryInfoDao.FindById(orderItem.DeliveryInfoId);

            PayInfoParam param = new PayInfoParam();
            param.OrderItem = orderItem;
            param.DeliveryInfoItem = deliveryInfoItem;
            param.UserItem = userItem;
            OrderService.SetPriceInPayInfoParam(orderItem.Id, param);
            param.PayInfoItem = GetPayInfoItem(param.OrderItem.PayInfoId);
            if (param.OrderItem.PayInfoId == 0)
            {
                if (orderItem.HighPrintId > 0)
                {
                    param.PayInfoItem.PayType = "BANK"; // 우선 출력 상품의 경우 대인불가
                    param.PayInfoItem.ShippingCharge = 0;
                    param.PayInfoItem.ManPaidCharge = 0;
                }
                else
                {
                    param.PayInfoItem.PayType = "MAN";
                    param.PayInfoItem.ShippingCharge = TradeUtil.GetShippingCharge(param.GoodsPrice, deliveryInfoItem);
                    param.PayInfoItem.ManPaidCharge = TradeUtil.ManPaidCharge(param.PayInfoItem.ProductCharge + param.PayInfoItem.ShippingCharge - param.PayInfoItem.PointPaid);
                }
                param.PayInfoItem.ProductCharge = param.TaxAddedPrice;
                param.PayInfoItem.CashPrice = param.PayInfoItem.ComputedCashPrice();
            }
            param.BankInfoSelectList = GetBankInfoSelectList(param.PayInfoItem.BankType);
            param.FromOrderConfirm = fromOrderConfirm;
            return param;
        }

        //대체
        public PayInfoParam GetPayInfoParam(OrderItem orderItem, UserItem userItem)
        {
            DeliveryInfoItem deliveryInfoItem = DeliveryInfoDao.FindById(orderItem.DeliveryInfoId);

            PayInfoParam param = new PayInfoParam();
            param.OrderItem = orderItem;
            param.DeliveryInfoItem = deliveryInfoItem;
            param.UserItem = userItem;
            param.PayInfoItem = GetPayInfoItem(param.OrderItem.PayInfoId);
            if (param.DeliveryInfoItem.IsProxy == 1)
            {
                param.PayInfoItem.PayType = "BANK";
            }
            param.BankInfoSelectList = GetBankInfoSelectList(param.PayInfoItem.BankType);
            param.TaxAddedPrice = OrderService.GetTaxAddedPrice(orderItem.Id);
            return param;
        }

        public PayInfoParam GetPayInfoParamBeforeDeliveryInfoInsert(OrderItem orderItem, UserItem userItem)
        {
            DeliveryInfoItem deliveryInfoItem = DeliveryInfoDao.FindById(orderItem.DeliveryInfoId);

            PayInfoParam param = new PayInfoParam();
            param.OrderItem = orderItem;
            param.DeliveryInfoItem = deliveryInfoItem;
            param.UserItem = userItem;
            OrderService.SetPriceInPayInfoParam(orderItem.Id, param);
            param.PayInfoItem = GetPayInfoItem(param.OrderItem.PayInfoId);
            //param.TaxAddedPrice = OrderService.GetTaxAddedPrice(orderItem.Id);
            if (param.OrderItem.PayInfoId == 0)
            {
                param.PayInfoItem.TotalPrice = param.TaxAddedPrice;
            }
            param.BankInfoSelectList = GetBankInfoSelectList(param.PayInfoItem.BankType);
            return param;
        }

        //private PayInfoItem GetPayInfoItem(int id)
        //{
        //    PayInfoItem payInfoItem = PayInfoDao.FindById(id);
        //    if (payInfoItem == null)
        //    {
        //        payInfoItem = new PayInfoItem();
        //    }
        //    return payInfoItem;
        //}

        //private SelectList GetBankInfoSelectList(string id)
        //{
        //    IList<BankInfoItem> bankInfoList = BankInfoDao.FindAll();
        //    return new SelectList(bankInfoList, "Id", "DisplayTitle", id);
        //}

        //public int ReducePayInfoWhenDepositUnChecked(PayInfoItem payInfoItem, int amount)
        //{
        //    if (IsOrderCancel(payInfoItem, amount))
        //    {
        //        int pointPaid = payInfoItem.PointPaid;
        //        payInfoItem.PointPaid = 0;
        //        payInfoItem.ProductCharge = 0;
        //        payInfoItem.ShippingCharge = 0;
        //        payInfoItem.ManPaidCharge = 0;
        //        payInfoItem.CashPrice = 0;
        //        payInfoItem.PayType = "POINT";
        //        return pointPaid;
        //    }

        //    int returnPoint = 0;
        //    int productCharge = payInfoItem.ProductCharge;
        //    int shippingCharge = payInfoItem.ShippingCharge;

        //    if (payInfoItem.IsBank() || payInfoItem.IsMan())
        //    {
        //        payInfoItem.ProductCharge -= amount;
        //        if (payInfoItem.HaveToAddShippingCharge())
        //        {
        //            payInfoItem.ShippingCharge = GetShippingCharge(payInfoItem);
        //        }

        //        if (payInfoItem.TotalCharge > payInfoItem.PointPaid)
        //        {
        //            returnPoint = 0;
        //        }
        //        else if (payInfoItem.TotalCharge == payInfoItem.PointPaid)
        //        {
        //            payInfoItem.PayType = "POINT";
        //            returnPoint = 0;
        //        }
        //        else
        //        {
        //            payInfoItem.PayType = "POINT";
        //            returnPoint = payInfoItem.PointPaid - payInfoItem.TotalCharge;
        //            payInfoItem.PointPaid -= returnPoint;
        //        }
        //        payInfoItem.CashPrice = payInfoItem.TotalCharge - payInfoItem.PointPaid;
        //        payInfoItem.ManPaidCharge = TradeUtil.ManPaidCharge(payInfoItem.CashPrice);
        //    }
        //    else
        //    {
        //        payInfoItem.ProductCharge -= amount;
        //        returnPoint = amount;
        //        if (payInfoItem.HaveToAddShippingCharge())
        //        {
        //            payInfoItem.ShippingCharge = GetShippingCharge(payInfoItem);
        //            returnPoint -= payInfoItem.ShippingCharge;
        //        }
        //        payInfoItem.PointPaid = payInfoItem.PointPaid - returnPoint;
        //    }
        //    return returnPoint;
        //}

        //private static bool IsOrderCancel(PayInfoItem payInfoItem, int amount)
        //{
        //    return amount == payInfoItem.TotalCharge;
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

        //[Transaction]
        //public void ChangeToBank(int id, string bankType, string bankUserName, int orderId)
        //{
        //    PayInfoItem payInfoItem = PayInfoDao.FindById(id);
        //    OrderItem orderItem = OrderDao.FindById(orderId);

        //    if (payInfoItem == null) return;
        //    if (!payInfoItem.IsMan()) return;
        //    payInfoItem.PayType = "BANK";
        //    payInfoItem.BankType = bankType;
        //    payInfoItem.BankUserName = bankUserName;
        //    payInfoItem.ManPaidCharge = 0;
        //    payInfoItem.CashPrice = payInfoItem.ComputedCashPrice();
        //    PayInfoDao.Update(payInfoItem);
        //    OrderDao.UpdatePayStatus(orderId, "UNPAID");
        //}

        ////지우자
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

        ////지우자
        //internal void UpdateChangeProductChargeMinus(PayInfoItem item, int price)
        //{
        //    if (item == null) return;
        //    item.ProductCharge -= price;

        //    item.CashPrice = item.ComputedCashPrice();
        //    PayInfoDao.Update(item);
        //}

        ////지우자
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

        //#endregion IPayInfoService 멤버

        //#region IPayInfoService 멤버

        //public void AddManPaidCharge(int id)
        //{
        //    PayInfoItem item = PayInfoDao.FindById(id);
        //    if (item == null) return;
        //    item.ManPaidCharge = TradeUtil.ManPaidCharge(item.ProductCharge + item.ShippingCharge - item.PointPaid);
        //    item.CashPrice = item.ComputedCashPrice();

        //    PayInfoDao.Update(item);
        //}

        //public void UpdateManPaidCharge(int id, int ManPaidCharg)
        //{
        //    PayInfoItem item = PayInfoDao.FindById(id);
        //    if (item == null) return;
        //    item.ManPaidCharge = ManPaidCharg;
        //    item.CashPrice = item.ComputedCashPrice();
        //    PayInfoDao.Update(item);
        //}

        //public void RemoveManPaidCharge(int id)
        //{
        //    PayInfoItem item = PayInfoDao.FindById(id);
        //    if (item == null) return;
        //    RemoveManPaidCharge(item);
        //}

        //internal void RemoveManPaidCharge(PayInfoItem item)
        //{
        //    if (item.ManPaidCharge == 0) return;
        //    item.ManPaidCharge = 0;
        //    item.CashPrice = item.ComputedCashPrice();
        //    PayInfoDao.Update(item);
        //}

        //public void AddShippingCharge(int id)
        //{
        //    PayInfoItem item = PayInfoDao.FindById(id);
        //    if (item == null) return;

        //    OrderItem orderItem = OrderDao.FindByPayInfoId(id);
        //    DeliveryInfoItem deliveryInfoItem = DeliveryInfoDao.FindById(orderItem.DeliveryInfoId);
        //    item.ShippingCharge = TradeUtil.GetShippingCharge(0, deliveryInfoItem);
        //    item.CashPrice = item.ComputedCashPrice();
        //    item.ManPaidCharge = TradeUtil.ManPaidCharge(item.CashPrice + item.ShippingCharge);
        //    PayInfoDao.Update(item);
        //}

        //public void UpdateShippingCharge(int id, int ShippingCharge)
        //{
        //    PayInfoItem item = PayInfoDao.FindById(id);
        //    if (item == null) return;
        //    OrderItem orderItem = OrderDao.FindByPayInfoId(id);
        //    DeliveryInfoItem deliveryInfoItem = DeliveryInfoDao.FindById(orderItem.DeliveryInfoId);
        //    item.ShippingCharge = ShippingCharge;
        //    item.CashPrice = item.ComputedCashPrice();
        //    item.ManPaidCharge = TradeUtil.ManPaidCharge(item.CashPrice + item.ShippingCharge);
        //    PayInfoDao.Update(item);
        //}

        //public int GetShippingCharge(PayInfoItem payInfoItem)
        //{
        //    OrderItem orderItem = OrderDao.FindByPayInfoId(payInfoItem.Id);
        //    DeliveryInfoItem deliveryInfoItem = DeliveryInfoDao.FindById(orderItem.DeliveryInfoId);
        //    return TradeUtil.GetShippingCharge(payInfoItem.ProductCharge, deliveryInfoItem);
        //}

        //public void RemoveShippingCharge(int id)
        //{
        //    PayInfoItem item = PayInfoDao.FindById(id);
        //    if (item == null) return;
        //    RemoveShippingCharge(item);
        //}

        //internal void RemoveShippingCharge(PayInfoItem item)
        //{
        //    if (item.ShippingCharge == 0) return;
        //    item.ShippingCharge = 0;
        //    item.ManPaidCharge = TradeUtil.ManPaidCharge(item.ProductCharge - item.PointPaid);
        //    item.CashPrice = item.ComputedCashPrice();

        //    //item.ManPaidCharge = TradeUtil.ManPaidCharge(item.CashPrice - item.ManPaidCharge);
        //    PayInfoDao.Update(item);
        //}

        //private void CalculatePriceAndRewordPointByPayInfo(PayInfoItem item)
        //{
        //    int orgPointPaid = item.PointPaid;

        //    if (item.CashPrice < 0)
        //    {
        //        item.PointPaid = item.PointPaid + item.CashPrice;
        //        item.CashPrice = 0;

        //        if (item.PointPaid < 0)
        //        {
        //            OrderItem orderItem = OrderDao.FindByPayInfoId(item.Id);
        //            PointService.AddPoint(orderItem.Code + " 注文内容変更による返金ポイント", (item.PointPaid * -1), item.UserId);
        //        }
        //    }
        //}

        //[Transaction]
        //public void PointRewardAboutUsePoint(OrderItem orderItem)
        //{
        //    string pointType = "POINTREWARD";
        //    DateTime cutDate = new DateTime(2014, 9, 16, 15, 0, 0);

        //    if (((DateTime)orderItem.OrderDate).IsEarlierThan(cutDate)) return;
        //    if (PointService.IsPointRewardedOrderItemByType(orderItem.Id, pointType)) return;

        //    PayInfoItem payInfoItem = PayInfoDao.FindById(orderItem.PayInfoId);
        //    if (payInfoItem.PointPaid <= 0) return;
        //    int rewardPoint = (int)(payInfoItem.PointPaid * 0.05);
        //    if (rewardPoint > 0) PointService.AddPoint(orderItem.Code + " キャッシュバックキャンペーン返還ポイント", rewardPoint, orderItem.UserId, pointType, orderItem.Id);
        //}

        //#endregion IPayInfoService 멤버

        //public void UpdateShippingChargeByUserDeliveryUpdate(int orderId)
        //{
        //    OrderItem orderItem = OrderDao.FindById(orderId);
        //    DeliveryInfoItem deliveryInfoItem = DeliveryInfoDao.FindById(orderItem.DeliveryInfoId);
        //    PayInfoItem payInfoItem = PayInfoDao.FindById(orderItem.PayInfoId);

        //    int oldShippingCharge = payInfoItem.ShippingCharge;
        //    payInfoItem.ShippingCharge = TradeUtil.GetShippingCharge(payInfoItem.ProductCharge, deliveryInfoItem);
        //    PayInfoDao.Update(payInfoItem);
        //}

        //[Transaction]
        //public void RewardByUsingCash(OrderItem orderItem)
        //{
        //    string pointType = "REWARDCASH";
        //    string[] possiblePayType = { "BANK", "TELEGRAM_KONBINI_N_REQUEST", "TELEGRAM_BANK_ASP_PAYMENT_REQUEST", "TELEGRAM_ATM_REQUEST" };
        //    DateTime cutDate = new DateTime(2016, 8, 15, 0, 0, 0);
        //    DateTime expiredDate = new DateTime(2016, 12, 12, 0, 0, 0);

        //    if (((DateTime)orderItem.OrderDate).IsEarlierThan(cutDate)) return;
        //    if (((DateTime)orderItem.OrderDate).IsLaterThan(expiredDate)) return;
        //    if (PointService.IsPointRewardedOrderItemByType(orderItem.Id, pointType)) return;

        //    PayInfoItem payInfoItem = PayInfoDao.FindById(orderItem.PayInfoId);
        //    if (payInfoItem.CashPrice <= 0) return;
        //    if (!possiblePayType.Contains(payInfoItem.PayType)) return;
        //    int rewardPoint = (int)(payInfoItem.CashPrice * 0.033);
        //    if (rewardPoint > 0) PointService.AddPoint(orderItem.Code + " 銀行決済・コンビニ決済ポイント還元", rewardPoint, orderItem.UserId, pointType, orderItem.Id);
        //}
    }
}
