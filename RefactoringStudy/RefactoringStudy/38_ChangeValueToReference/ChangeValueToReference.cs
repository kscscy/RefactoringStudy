using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 값을 참조로 전환 (Change Value to Reference)
/// 
/// 클래스에 같은 인스턴스가 많이 들어 있어서 이것들을 하나의 객체로 바꿔야할 땐
/// 그 객체를 참조객체로 전환하자.
/// 
/// 참조객체는 고객이나 계좌 같은 것이다. 각 객체는 현실에서의 한 객체에 대응하므로,
/// 둘이 같은지 검사할 때는 객체 ID를 사용한다. 두 객체가 같은지 판단해야 하므로
/// equals나 hashCode(GetHashCode) 메소드를 재정의해야한다.
/// 
/// 1. 생성자를 팩토리 메소드로 전환
/// 2. 참조 객체로의 접근을 담당할 객체를 정한다.
/// 3. 객체를 미리 생성할지 사용하기 직전에 생성할지 정한다.
/// 4. 참조 객체를 반환하게 팩토리 메소드를 수정한다.
/// </summary>

namespace RefactoringStudy._38_ChangeValueToReference
{
    //before
    class Order
    {
        private Customer _customer;

        public string CustomerName
        {
            get { return _customer.Name; }
            set { _customer = new Customer(value); }
        }

        public Order(string customerName)
        {
            _customer = new Customer(customerName);
        }

        private static int NumberOfOrdersFor(IEnumerable<Order> orders, string customer)
        {
            return orders.Count(o => o.CustomerName.Equals(customer, StringComparison.InvariantCultureIgnoreCase));
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
            return Name.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    //after
    class Order2
    {
        private Customer2 _customer;

        public string CustomerName
        {
            get { return _customer.Name; }
            set { _customer = Customer2.GetExsitingCustomer(value); }
        }

        public Order2(string customerName)
        {
            _customer = Customer2.GetExsitingCustomer(customerName);
        }

        private static int NumberOfOrdersFor(IEnumerable<Order> orders, string customer)
        {
            return orders.Count(o => o.CustomerName.Equals(customer, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    class Customer2
    {
        private static HashSet<Customer2> ExistingCustomers = new HashSet<Customer2>();

        static Customer2()
        {
            ExistingCustomers.Add(new Customer2("adprint"));
            ExistingCustomers.Add(new Customer2("tqoon"));

        }

        public string Name { get; private set; }

        private Customer2(string name)
        {
            Name = name;
        }
        
        public static Customer2 GetExsitingCustomer(string name)
        {
            return ExistingCustomers.Single(s => s.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public override bool Equals(object obj)
        {
            return Name.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }


}
