using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 분류 부호를 하위 클래스로 전환 (Replace Type Code With Subclasses)
/// 
/// 클래스 기능에 영향을 주는 변경불가 분류 부호가 있을 땐 분류 부호를 하위 클래스로 만들자
/// 
/// 분류 부호가 클래스 기능에 영향을 미치는 현상은 case 문 같은 조건문이 있을 때 주로 나타난다.
/// 그런 조건문은 swich문 아니면 if-else문이다.
/// 어느 조건문이든 분류 부호의 값을 검사해서 그 값에 따라 다른 코드를 실행한다.
/// 
/// 이런 조건문은 조건문을 재정의로 전환을 실시해서 재정의로 바꿔야 한다.
/// 이 기법이 효과를 보려면 분류 부호를 다형화된 기능이 든 상속 구조로 고쳐야 한다.
/// 그런 상속 구조는 하나의 클래스와 각 분류 부호에 대한 하위클래스로 구성된다.
/// 
/// 분류부호가 든 클래스 코드를 이용해 각 분류 부호별 하위클래스를 작성하자. 
/// 이 기법을 적용할 수 없는 몇 가지 경우는 다음과 같다.
/// 
/// - 분류 부호의 값이 객체 생성 후 변할 때
/// - 다른 이유로 분류 부호를 이미 하위클래스로 만들었을 때
/// - 분류 부호를 상태/전략 패턴으로 전환을 실시해야 할 때
/// 
/// 
/// </summary>

namespace RefactoringStudy._49_ReplaceTypeCodeWithSubclasses
{
    class Employee
    {
        public const int ENGINEER = 0;
        public const int SALESMAN = 1;
        public const int MANAGER = 2;

        public int Type { get; private set; }

        public Employee(int type)
        {
            Type = type;
        }
    }

    // after
    abstract class Employee2
    {
        public const int ENGINEER = 0;
        public const int SALESMAN = 1;
        public const int MANAGER = 2;

        public abstract int Type { get; /*private set;*/ }

        //private Employee2(int type)
        //{
        //    Type = type;
        //}

        //protected Employee2() { }

        public static Employee2 Create(int type)
        {
            //if (type == ENGINEER) return new Engineer();
            switch (type)
            {
                case ENGINEER:
                    return new Engineer();
                case SALESMAN:
                    return new Salesman();
                case MANAGER:
                    return new Manager();
            }
            //return new Employee2(type);
            throw new Exception("Unknown Type");
        }
    }

    class Engineer : Employee2
    {
        public override /*new*/ int Type
        {
            get { return Employee2.ENGINEER; }
        }
    }
    class Salesman : Employee2
    {
        public override int Type
        {
            get { return Employee2.SALESMAN; }
        }
    }
    class Manager : Employee2
    {
        public override int Type
        {
            get { return Employee2.MANAGER; }
        }
    }
}
