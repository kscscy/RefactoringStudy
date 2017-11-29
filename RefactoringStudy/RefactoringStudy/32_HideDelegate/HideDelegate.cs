using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 대리 객체 은폐 (Hide Delegate)
/// 
/// 클라이언트가 객체의 대리 클래스를 호출할 땐, 대리 클래스를 감추는 메소드를 서버에 작성하자
/// 
/// 클라이언트가 서버 객체의 필드 중 하나에 정의된 메소드를 호출할 때 그 클라이언트는 이 대리 객체에 관해 알아야 한다.
/// 대리 객체가 변경될 때 클라이언트도 변경해야 할 가능성이 있기 때문이다.
/// 의존성을 없애기 위해서 대리 객체를 감추는 위임 메소드를 서버에 두면 된다. 변경은 서버에서만 이뤄지고 클라이언트에는 영향을 주지 않는다.
/// 
/// 
/// 
/// </summary>

namespace RefactoringStudy._32_HideDelegate
{
    // before
    class Employee
    {
        public Department WorkDepartment { get; set; }
    }

    class Department
    {
        public string ChargeCode { get; set; }
        public Employee Manager { get; set; }
    }

    class TestEmployee
    {
        void Test()
        {
            var tester = new Employee();
            var testManager = tester.WorkDepartment.Manager;
        }
    }

    // after
    class Employee2
    {
        private Department2 WorkDepartment { get; set; }
        public Employee2 Manager
        {
            get { return WorkDepartment.Manager; }
        }
    }

    class Department2
    {
        public string ChargeCode { get; set; }
        public Employee2 Manager { get; set; }
    }

    class TestEmployee2
    {
        void Test()
        {
            var tester = new Employee2();
            var testManager = tester.Manager;
        }
    }
}
