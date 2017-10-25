using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 데이터 클래스 Data Class
/// 
/// 데이터 클래스는 필드와 getter/setter 메소드만 들어있는 클래스다.
/// 그런 오로지 데이터 보관만 담당하며, 데이터 조작은 다른 클래스가 수행한다. 어쩌면 public 필드 였을수도 있다.
/// 
/// 필드 캡슐화 기법을 실시한다. 컬렉션 필드가 있으면 캡슐화 되어 있는지 확인하여 컬렉션 캡슐화 기법을 적용한다.
/// 변경되지 않아야 하는 필드는 메소드 제거를 사용한다.
/// 
/// getter/setter 메소드가 다른 클래스에 의해 사용되는 부분을 찾아서 메소드 이동을 실시하여 데이터 클래스로 옮긴다.
/// 메소드 전체를 옮길 수 없다면 메소드 추출을 실시한다. 나중에 getter/setter 메소드에 대해서는 메소드 은폐를 적용한다.
/// </summary>

namespace RefactoringStudy._20_DataClass
{
    class DataClass
    {
        public string name;
        // 캡슐화
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Name2 { get; set; }
    }

    class Collection1
    {
        // 1. Without auto-properties at all to be able to use field initializer:
        private List<string> _names = new List<string>();
        public List<string> Names
        {
            get { return _names; }
        }
    }

    class Collection2
    {
        // 2. Using auto-properties. This approach is ok if class has only one constructor:
        public List<string> Names { get; private set; }
        public Collection2()
        {
            Names = new List<string>();
        }
    }

    class Collection3
    {
        private readonly List<string> _names = new List<string>();
        public ICollection<string> Names
        {
            get { return new ReadOnlyCollection<string>(_names); }
        }
        public void Add_Name(string name)
        {
            _names.Add(name);
        }
        public void Remove_Names(string name)
        {
            _names.Remove(name);
        }
        public void Clear_Names()
        {
            _names.Clear();
        }

        ///<summary>
        /// 캡슐화된 컬렉션을 사용하는 사례
        /// 
        /// 1) A collection which can be changed.
        /// => 변경할 수 있는 컬렉션
        /// 
        /// 2) A collection that can be modified, but not swapped.
        /// => 수정할 수는 있지만 바뀌지 않는 컬렉션
        /// 
        /// 3) Expose a read-only copy of a collection.
        /// => ReadOnlyCollection 을 복사하여 노출시키기
        /// 
        /// 4) Collections that can be modified, but only in a certain way.
        /// => 수정할 수 있지만, 특정 방법을 이용(?) 하는 컬렉션
        /// 
        /// 5) Custom implementation of collection.
        /// => 컬렉션의 커스터마이징 구현
        /// 
        /// </summary>

    }

    //////////////////////////////////////////////////////////////////

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Province { get; set; }
        public List<Order> Orders { get; set; }

        public string GetFullName()
        {
            return LastName + ", " + FirstName;
        }
    }

    public class Order
    {
        public Order(Customer customer)
        {
            customer.Orders.Add(this);
        }
    }

    ////// 

    public class Customer2
    {
        private readonly IList<Order> _orders = new List<Order>();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Province { get; set; }
        public IEnumerable<Order> Orders { get { return _orders; } }

        public string GetFullName()
        {
            return LastName + ", " + FirstName;
        }
    }

    /////
    public class Customer3
    {
        private readonly IList<Order> _orders = new List<Order>();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Province { get; set; }
        public IEnumerable<Order> Orders { get { return _orders; } }

        internal void AddOrder(Order order)
        {
            _orders.Add(order);
        }
    }
}
