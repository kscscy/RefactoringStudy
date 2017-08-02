// 07.Feature Envy

// 어떤 값을 계산하기 위해 다른 객체의 get메소드를 호출하는 경우는 수도 없이 많다
// => Move Method를 사용한다.
// 메소드의 특정 부분만 이런 욕심으로 고통 받는데 이럴 때는 욕심이 많은 부분에 대해서
// => Extract Method 사용한 다음 Move Method를 사용한다.

// 한 메소드가 여러개의 클래스의 기능을 사용하는 경우가 있는데 이럴때는 어느 클래스 메소드로 옮겨야 하는가?
// 경험적인 방법은 어떤 클래스에 있는 데이터를 가장 많이 사용하는 가를 보고 메소드를 그 클래스로 옮기는 것이다



namespace RefactoringStudy._07_FeatureEnvy
{
    class CustomerController_Old : AbstractController
    {
        public ActionResult UserHistory(string userId, [DefaultValue("")] string defaultItem)
        {
            ViewData["defaultItem"] = defaultItem;
            UserHistory userHistory = UserService.GetUserHistory(userId);
            if (joinerItem.IsOrderMall() && !userHistory.UserItem.JoinSite.Equals(joinerItem.SiteUrl)) return View();

            userHistory.LivingOrderCount = CommonObjectDao.GetCountByListQuery(OrderQuery.FindLivingOrderByUserId(userId, CurrentSite.JoinerId));
            return View(userHistory);
        }

        public ActionResult LivingOrder(ListFormParam param, string userId)
        {
            ListViewInfoManager<AdprintOrderItem> manager = new ListViewInfoManager<AdprintOrderItem>();
            manager.ListFormParam = param;
            manager.ItemCount = CommonObjectDao.GetCountByListQuery(OrderQuery.FindLivingOrderByUserId(userId, CurrentSite.JoinerId));
            manager.ItemList = CommonObjectDao.FindList<AdprintOrderItem>(OrderQuery.FindLivingOrderByUserId(userId, CurrentSite.JoinerId));
            manager.CountOfListPage = 5;

            CommonListViewInfo<AdprintOrderItem> viewInfo = manager.GetListViewInfo();

            ViewData["pageSize"] = param.PageSize.ToString();
            ViewData["title"] = L.T("진행중주문");
            return PartialView("~/Areas/Page/Views/Customer/Partial/OrderInfoPartial.ascx", viewInfo);
        }

        public ActionResult CompleteOrder(ListFormParam param, string userId)
        {
            ListViewInfoManager<AdprintOrderItem> manager = new ListViewInfoManager<AdprintOrderItem>();
            manager.ListFormParam = param;
            manager.ItemCount = CommonObjectDao.GetCountByListQuery(OrderQuery.FindCompleteOrderByUserId(userId, CurrentSite.JoinerId));
            manager.ItemList = CommonObjectDao.FindList<AdprintOrderItem>(OrderQuery.FindCompleteOrderByUserId(userId, CurrentSite.JoinerId));
            manager.CountOfListPage = 5;

            CommonListViewInfo<AdprintOrderItem> viewInfo = manager.GetListViewInfo();

            ViewData["pageSize"] = param.PageSize.ToString();
            ViewData["title"] = L.T("완료된주문");
            return PartialView("~/Areas/Page/Views/Customer/Partial/OrderInfoPartial.ascx", viewInfo);
        }
        //...
    }


    // UserHistory 페이지를 호출하는 부분
    // 1. UserService.GetUserHistory 에서 LivingOrderCount 를 set하는 부분이 있는데 왜 다시 CustomerController에서 set할까
    // UserService 부분
    public class UserService : CommonObjectService, IUserService
    {
        //...
        public UserHistory GetUserHistory(string userId)
        {
            UserHistory userHistory = new UserHistory();
            userHistory.UserItem = UserDao.FindByUserId(userId);
            userHistory.UserItem.CustomerMemoList = CustomerMemoDao.FindByUserId(userId);
            userHistory.LivingOrderCount = AdprintOrderDao.LivingOrderCountByUserId(userId);
            userHistory.ClaimCount = QnaDao.ClaimQnaCountByUserId(userId);
            return userHistory;
        }
        //...
    }
    // LivingOrderCount가 CustomerController 다른 이유는 admin과 partner의 쿼리문이 다르기 때문에 CustomerController 다시 호출해서 사용하고 있었다.
    // GetUserHistory 에서는 CustomerMemoDao, AdprintOrderDao, QnaDao를 호출하며 UserHistory에 넣어주고 있다


    // 2. Order와 관련되어있는 partial페이지들을 위해서 COD로 감싸진 OrderQuery를 동일한 형태로 호출하고 있다.
    // 메모, 주문, 클레임의 카운트가 필요하다면 navcontroller로 옮긴다

    // /////////////// 수정방향
    // 1-1 CustomerController
    public class CustomerController : AbstractController
    {
        //...
        public ActionResult UserHistory(string userId)
        {
            UserHistory userHistory = UserService.GetUserHistory(userId);
            return View(userHistory);
        }
    }
    // 1-2 HistoryOrderController 를 생성해서 CustomerController 에서 분리
    public class HistoryOrderController : AbstractController
    {
        public ActionResult LivingOrder(ListFormParam param, string userId)
        {
            var viewInfo = SetCommonView(param, "LivingOrder");
            ViewData["title"] = "진행중주문";
            return PartialView("~/Areas/Page/Views/Customer/Partial/OrderInfoPartial.ascx", viewInfo);
        }

        public ActionResult CompleteOrder(ListFormParam param, string userId)
        {
            var viewInfo = SetCommonView(param, "CompleteOrder");
            ViewData["title"] = "완료된주문";
            return PartialView("~/Areas/Page/Views/Customer/Partial/OrderInfoPartial.ascx", viewInfo);
        }

        // ... 

        private CommonListViewInfo<AdprintOrderItem> SetCommonViewInfo(ListFormParam param, string type)
        {
            var manager = new ListViewInfoManager<AdprintOrderItem>();
            manager.ListFormParam = param;
            manager.ItemCount = CommonObjectDao.GetCountByListQuery(GetListQuery(type, CurrentSite.JoinerId));
            manager.ItemList = CommonObjectDao.FindList<AdprintOrderItem>(GetListQuery(type, CurrentSite.JoinerId));
            manager.CountOfListPage = 5;
            var viewInfo = manager.GetListViewInfo();
            return viewInfo;
        }

        private ListQuery<AdprintOrderItem> GetListQuery(string type, int joinerId)
        {
            switch (type)
            {
                case:
                    "LivingOrder"
                    return OrderQuery.FindLivingOrderByUserId(userId, CurrentSite.JoinerId);
                    break;
                case:
                    "CompleteOrder"
                    return OrderQuery.FindCompleteOrderByUserId(userId, CurrentSite.JoinerId);
                    break;
                    // ...
                    // type별로 swich-case문이 많아지는 것은 좋지 않아 보인다.
            }
        }
    }


    // 2. 좌측의 nav와 count가 필요하다면 NavController 에서
    public ActionResult UserHistory()
    {
        PartnerNavItem nav = UserHistoryItem();
        return PartialView("~/Areas/Page/Views/Nav/nav.ascx", nav);
    }

    public PartnerNavItem UserHistoryItem()
    {
        PartnerNavItem item = PartnerNavItem.Create("UserHistory");
        var livingOrderCount = CommonObjectDao.GetCountByListQuery(OrderQuery.FindLivingOrderByUserId(userId, CurrentSite.JoinerId));
        var completeOrderCount = CommonObjectDao.GetCountByListQuery(OrderQuery.FindComepleteOrderByUserId(userId, CurrentSite.JoinerId));
        var customerMemoCount = CommonObjectDao.GetCountByListQuery(OrderQuery.FindCustomerMemoListByUserId(userId, CurrentSite.JoinerId));
        var customerClaimCount = CommonObjectDao.GetCountByListQuery(UserQuery.ClaimQnaCountByUserId(userId, CurrentSite.JoinerId));

        item.AddChild("진행중주문" + "(" + livingOrderCount + ")", link: "/UserHistory/LivingOrder");
        item.AddChild("완료된주문" + "(" + completeOrderCount + ")", link: "/UserHistory/CompleteOrder");
        //...
        item.AddChild("고객메모" + "(" + customerMemoCount + ")", link: "/UserHistory/CustomerMemoList");
        item.AddChild("클레임정보" + "(" + customerClaimCount + ")", link: "/UserHistory/ClaimQnaCount");
        //...
    }
}
