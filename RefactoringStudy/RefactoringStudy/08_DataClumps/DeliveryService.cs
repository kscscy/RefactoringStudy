// 08. 데이터 뭉치(Data clumps)
// 동일한 3~4개의 데이터 항목이 여러 위치에 몰려있는경우 이를 객체로 만들어야 한다.
// 필드들을 대상으로 클래스를 추출

using System;

namespace RefactoringStudy._08_DataClumps
{
    class DeliveryService_Old : IDeliveryService
    {
        //...
        public CommonParam DeliveryTicketList(CommonParam param)
        {
            string searchType = param.GetString("searchType");
            string writeDateStart = param.GetString("writeDateStart");
            string writeDateEnd = param.GetString("writeDateEnd");
            string dbWriteDateStart = param.GetString("writeDateStart");
            string dbWriteDateEnd = param.GetString("writeDateEnd");
            string sendDateStart = param.GetString("sendDateStart");
            string sendDateEnd = param.GetString("sendDateEnd");
            string boxNumber = param.GetString("boxNumber");
            string orderNumber = param.GetString("orderNumber");
            string ticketNumber = param.GetString("ticketNumber");
            string sagawaWeight = param.GetString("sagawaWeight");
            int page = param.GetInt("page");
            int pageSize = param.GetInt("pageSize");

            //...
            var listParam = new ListParam<DeliveryTicketItem>();
            var rtnParam = new CommonParam();
            rtnParam.Add("listParam", listParam);
            rtnParam.Add("writeDateStart", writeDateStart);
            rtnParam.Add("writeDateEnd", writeDateEnd);
            rtnParam.Add("unitSum", unitSum);

            return rtnParam;
        }
    }

    // 1. DeliveryServiceItem 클래스 생성
    public class DeliveryServiceItem
    {
        public string searchType { get; set; }
        public string writeDateStart { get; set; }
        public string writeDateEnd { get; set; }
        public string dbWriteDateStart { get; set; }
        public string dbWriteDateEnd { get; set; }
        public string sendDateStart { get; set; }
        public string sendDateEnd { get; set; }
        public string boxNumber { get; set; }
        public string orderNumber { get; set; }
        public string ticketNumber { get; set; }
        public string sagawaWeight { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }

        public int startNum()
        {
            return (page - 1) * pageSize + 1;
        }
        public int endNum()
        {
            return page * pageSize;
        }
    }


    // 2. DeliveryService
    public class DeliveryService : IDeliveryService
    {
        //...
        public CommonParam DeliveryTicketList(CommonParam param)
        {
            DeliveryServiceItem deliveryServiceItem;
            //... 

            var listParam = new ListParam<DeliveryTicketItem>();
            var rtnParam = new CommonParam();
            rtnParam.Add("listParam", listParam);
            rtnParam.Add("writeDateStart", DateTime.Parse.(deliveryServiceItem.writeDateStart.Trim().GetDayStartValue());
            rtnParam.Add("writeDateEnd", DateTime.Parse.(deliveryServiceItem.writeDateEnd.Trim().GetDayStartValue());
            rtnParam.Add("unitSum", DeliveryInfoDao.GetDeliveryTicketUnitSum(whereConditons));

            return rtnParam;
        }
    }

}
