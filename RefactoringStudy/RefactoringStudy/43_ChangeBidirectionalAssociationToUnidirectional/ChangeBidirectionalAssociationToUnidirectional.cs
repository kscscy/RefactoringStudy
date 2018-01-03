using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 클래스의 양방향 연결을 단방향으로 전환 (Change Bidirectional Association To Unidirectional)
/// 
/// 두 클래스가 양방향으로 연결되어 있는데 한 클래스가 다른 클래스의 기능을 더 이상 사용하지 않게 됐을 땐
/// 불필요한 방향의 연결을 끊자.
/// 
/// 양방향 연결은 쓸모가 많지만 대가가 따른다.
/// 양방향 연결을 유지하고 객체가 적절히 생성되고 제거되는지 확인하는 복잡함이 더해진다.
/// 
/// 양방향 연결이 많으면 좀비 객체가 발생하기도 쉽다. 좀비 객체란 참조가 삭제 되지 않아 제거돼야 함에도
/// 남아서 떠도는 객체를 뜻한다.
/// 
/// 양방향 연결로 인해 두 클래스는 서로 종속된다. 한 클래스를 수정하면 다른 클래스도 변경된다.
/// 종속성이 많으면 시스템의 결합력이 강해져서 사소한 수정에도 예기치 못한 각종 문제가 발생한다.
/// 그러므로 양방향 연결은 꼭 필요할 때만 사용해야 한다.
/// 양방향 연결이 더 이상 쓸모없다고 판단되면 불필요한 연결을 차단하자.
/// 
/// </summary>

namespace RefactoringStudy._43_ChangeBidirectionalAssociationToUnidirectional
{
    // before
    class Order
    {
        private Customer _customer;
        public Customer Customer
        {
            get { return _customer; }
            set
            {
                _customer.FriendOrders.Remove(this);
                _customer = value;
                _customer.FriendOrders.Add(this);
            }
        }
    }

    class Customer
    {
        private HashSet<Order> _orders = new HashSet<Order>();

        public IEnumerable<Order> Orders
        {
            get { return _orders; }
        }

        internal HashSet<Order> FriendOrders
        {
            get { return _orders; }
        }
    }

    // after
    class Order2
    {
        public Customer2 Customer2
        {
            get
            {
                var customers = Customer2.FindCustomers(new CustomerSearch { Order2 = this });
                if (customers.Count() < 1)
                    throw new Exception("Not Found");
                if (customers.Count() > 1)
                    throw new Exception("Multiple Customers found for Order");
                return customers.Single();
            }
        }
    }

    class Customer2
    {
        private HashSet<Order2> _orders2 = new HashSet<Order2>();

        public HashSet<Order2> Orders2
        {
            get { return _orders2; }
        }

        public static IEnumerable<Customer2> GetAllCustomers()
        {
            return new List<Customer2>();
        }

        public static IEnumerable<Customer2> FindCustomers(CustomerSearch customerSearch)
        {
            return new List<Customer2>();
        }
    }

    class CustomerSearch
    {
        public Order2 Order2 { get; set; }
    }
}
