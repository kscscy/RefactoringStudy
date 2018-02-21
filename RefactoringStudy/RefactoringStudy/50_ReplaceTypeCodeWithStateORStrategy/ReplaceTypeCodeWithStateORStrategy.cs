using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 분류 부호를 상태/전략 패턴으로 전환 (Replace Type Code With State OR Strategy)
/// 
/// 분류 부호가 클래스의 기능에 영향을 주지만 하위 클래스로 전환할 수 없을 땐
/// 그 분류 부호를 상태 객체로 만들자
/// 
/// 이 기법은 분류 부호를 하위클래스로 전환과 비슷하지만
/// 분류 부호가 객체 수명주기 동안 변할 때나 다른 이유로 하위클래스로 만들 수 없을 때 사용한다.
/// 
/// 이 기법은 상태 패턴이나 전략 패턴중 하나를 사용한다.
/// 이 기법을 사용하면 많은 불필요한 추가 클래스가 생긴다.?
/// 
/// 상태 패턴과 전략 패턴을 공부하자.
/// 하나의 알고리즘을 단순화해야 할 때는 전략이 더 적절하며,
/// 상태별 데이터를 이동하고 객체를 변화하는 상태로 생각할 때는 상태 패턴이 더 적절하다.
/// 
/// 
/// </summary>

namespace RefactoringStudy._50_ReplaceTypeCodeWithStateORStrategy
{
    //
    class Employee
    {
        public const int ENGINEER = 0;
        public const int SALESMAN = 1;
        public const int MANAGER = 2;

        public int Type { get; set; }

        public int Salary { get; private set; }
        public int Comission { get; private set; }
        public int Bonus { get; private set; }

        public Employee(int type)
        {
            Type = type;
        }

        public int GetPayAmount()
        {
            switch(Type)
            {
                case ENGINEER:
                    return Salary;
                case SALESMAN:
                    return Salary + Comission;
                case MANAGER:
                    return Salary + Bonus;
            }
            throw new Exception("Unknown Type");
        }
    }

    //after
    class Employee2
    {
        //5. 정의 복사
        //public const int ENGINEER = 0;
        //public const int SALESMAN = 1;
        //public const int MANAGER = 2;

        //4. get set 수정 
        //private EmployeeType _employeeType;
        //public int Type
        //{
        //    //4.
        //    //get;
        //    //set;
        //    get { return _employeeType.Type; }
        //    set { _employeeType = EmployeeType.Create(value); }
        //}

        // 7. 분류부호 정의 삭제 후 클래스 참조로 변환
        private EmployeeType Type { get; set; }


        public int Salary { get; private set; }
        public int Comission { get; private set; }
        public int Bonus { get; private set; }

        public Employee2(/*int 7. */ EmployeeType type)
        {
            Type = type;
        }

        //8. 조건문을 재정의로 전환
        //public int GetPayAmount()
        //{
        //    // 7.
        //    //switch (Type)
        //    //switch (Type.Type)
        //    //{
        //    //    //6. 정의를 복사 후 수정
        //    //    //case ENGINEER:
        //    //    case EmployeeType.ENGINEER:
        //    //        return Salary;
        //    //    case EmployeeType.SALESMAN:
        //    //        return Salary + Comission;
        //    //    case EmployeeType.MANAGER:
        //    //        return Salary + Bonus;
        //    //}
        //    //throw new Exception("Unknown Type");
        //    return Type.GetPayAmount(this);
        //}
    }


    // 1. 상태 클래스 선언하기
    abstract class EmployeeType
    {
        // 5. 분류 부호 정의를 복사 > EmployeeType에 대한 팩토리 메소드 작성 후, Employee 클래스의 set 수정
        public const int ENGINEER = 0;
        public const int SALESMAN = 1;
        public const int MANAGER = 2;

        public abstract int Type { get; }
        
        // 3. switch 문 생성
        public static EmployeeType Create(int type)
        {
            switch (type)
            {
                //case Employee2.ENGINEER:
                //6. 정의를 복사 후 수정
                case ENGINEER:
                    return new Engineer();
                case SALESMAN:
                    return new SalesMan();
                case MANAGER:
                    return new Manager();
            }
            throw new Exception("Unknown Type");
        }

        //8.
        public abstract int GetPayAmount(Employee2 employee2);
        
    }


    // 2. 분류 부호별 하위클래스 작성하기

    class Engineer : EmployeeType
    {
        public override int Type
        {
            get
            {
                //6. 정의를 복사 후 수정
                //return Employee2.ENGINEER;
                return ENGINEER;
            }
        }

        public override int GetPayAmount(Employee2 employee2)
        {
            return employee2.Salary;
        }
    }

    class SalesMan : EmployeeType
    {
        public override int Type
        {
            get
            {
                 return SALESMAN;
            }
        }

        public override int GetPayAmount(Employee2 employee2)
        {
            return employee2.Salary + employee2.Comission;
        }
    }

    class Manager : EmployeeType
    {
        public override int Type
        {
            get
            {
                return MANAGER;
            }
        }

        public override int GetPayAmount(Employee2 employee2)
        {
            return employee2.Salary + employee2.Bonus;
        }
    }
}
