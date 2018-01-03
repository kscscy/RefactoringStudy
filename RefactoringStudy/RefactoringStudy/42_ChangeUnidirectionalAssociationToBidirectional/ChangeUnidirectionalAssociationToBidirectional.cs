using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 클래스의 단방향 연결을 양방향으로 전환 (Change Unidirectional Association to Bidirectional)
/// 
/// 두 클래스가 서로의 기능을 사용해야 하는데 한 방향으로만 연결되어 있을 땐
/// 역 포인터(?)를 추가하고 두 클래스를 모두 업데이트할 수 있게 접근 한정자를 수정하자.
/// 
/// 한 클래스에서 다른 클래스를 참조하게 해 놓고 나중에 역으로 참조해야 할 경우가 생긴다면?
/// 포인터를 역방향으로 참조해야 한다.
/// 포인터는 단방향 연결이라서 이런 식의 역방향 참조는 불가능하다. 
/// 다른 경로를 찾아서 해결할 수 있을 때도 있다.
/// 그렇게 하면 계산에 비용이 들긴 해도 합리적이며, 이 기능을 사용하는 메소드를 참조되는 클래스에 넣을 수 있다.
/// 
/// 하지만 이게 쉽지 않을 떄도 있으므로 양방향 참조(역포인터)를 설정해야 한다.
/// 
/// - 양방향 연결은 단방향 연결보다 구현 및 유지관리가 훨씬 어렵다.
/// - 양방향 연결은 클래스를 상호 의존적으로 만든다. 단방향 연결을 사용하면 그 중 하나를 다른 연결과 독립적으로 사용할 수 있다.
/// 
/// 
/// </summary>

namespace RefactoringStudy._42_ChangeUnidirectionalAssociationToBidirectional
{
    // before
    class Order
    {
        public Customer Customer { get; set; }
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
        private HashSet<Customer2> _customers2 = new HashSet<Customer2>();
        public IEnumerable<Customer2> Customers2
        {
            get { return _customers2; }
        }

        public void AddCustomer(Customer2 customer2)
        {
            customer2.FriendOrders.Add(this);
            _customers2.Add(customer2);
        }

        public void RemoveCustomer(Customer2 cusomter2)
        {
            cusomter2.FriendOrders.Remove(this);
            _customers2.Remove(cusomter2);
        }

        //private Customer2 _customer2;
        //public Customer2 Customer2
        //{
        //    get { return _customer2; }
        //    set
        //    {
        //        _customer2.FriendOrders.Remove(this);
        //        _customer2 = value;
        //        _customer2.FriendOrders.Add(this);
        //    }
        //}
    }

    class Customer2
    {
        private HashSet<Order2> _orders = new HashSet<Order2>();

        public IEnumerable<Order2> Orders
        {
            get { return _orders; }
        }

        internal HashSet<Order2> FriendOrders
        {
            get { return _orders; }
        }

        public void AddOrder(Order2 order2)
        {
            order2.AddCustomer(this);
        }

        public void RemoveOrder(Order2 order2)
        {
            order2.RemoveCustomer(this);
        }
    }
}
