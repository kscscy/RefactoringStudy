using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 데이터 값을 객체로 전환(Replace Data Value with Object)
/// 
/// 데이터 항목에 데이터나 기능을 더 추가해야 할 때는 데이터 항목을 객체로 만들자.
/// Extract Class의 특별한 경우이다. 클래스 내부의 관련성을 증가시킨다. 데이터와 관련된 행동들이 하나의 클래스 안에 들어있다.
/// 
/// </summary>

namespace RefactoringStudy._37_ReplaceDataValueWithObject
{
    // before
    class Order
    {
        public string Customer { get; set; }

        public Order(string customer)
        {
            Customer = customer;
        }

        private static int NumberOfOrdersFor(IEnumerable<Order> orders, string customer)
        {
            return orders.Count(o => o.Customer.Equals(customer, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    // after
    class Order2
    {
        public Customer Customer { get; set; }

        public Order2(Customer customer)
        {
            Customer = customer;
        }

        private static int NumberOfOrdersFor(IEnumerable<Order2> orders, string customer)
        {
            return orders.Count(o => o.Customer.Equals(customer));
        }
    }



    class Customer
    {
        public string Name { get; private set; }

        public Customer(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return Name.Equals(((Customer)obj).Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

}
