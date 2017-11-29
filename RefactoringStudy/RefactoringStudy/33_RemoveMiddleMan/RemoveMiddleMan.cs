using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 과잉 중개 메소드 제거 (Remove Middle Man)
/// 
/// 클래스에 자잘한 위임이 너무 많을 땐 대리 객체를 클라이언트가 직접 호출하게 하자.
/// 
/// Hide Delegate를 사용할 때의 단점은 클라이언트가 대리 개체의 새 기능을 사용해야 할 때마다 서버에 간단한 위임 메소드를 추가해야 한다는 점이다.
/// 서버 클래스는 그저 중개자에 불과하므로, 이때는 클라이언트가 대리 객체를 직접 호출하게 해야한다.
/// 
/// 은폐의 적절한 정도를 알기란 어렵다. 시스템이 변경되면 은폐 정도의 기준도 변하고 필요할 때마다 보수하자.
/// 
/// </summary>

namespace RefactoringStudy._33_RemoveMiddleMan
{
    // before
    class Employee
    {
        private Department WorkDepartment { get; set; }

        public Employee Manager
        {
            get { return WorkDepartment.Manager; }
        }

        public string ChargeCode
        {
            get { return WorkDepartment.ChargeCode; }
        }
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
            var testManager = tester.Manager;
        }
    }

    // after
    class Employee2
    {
        public Department2 WorkDepartment { get; set; }
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
            var testManager = tester.WorkDepartment.Manager;
        }
    }
}
