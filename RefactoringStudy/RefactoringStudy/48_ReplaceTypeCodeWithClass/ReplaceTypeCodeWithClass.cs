using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 분류 부호를 클래스로 전환 (Replace Type Code With Class)
/// 
/// 기능에 영향을 미치는 숫자형 분류 부호가 든 클래스가 있을 땐 그 숫자를 새 클래스로 바꾸자
/// 
/// 분류 부호 이름을 상징적인 것으로 정하면 코드가 상당히 이해하기 쉬워진다.
/// 문제는 상징적 이름은 단지 별명에 불과하다는 점이다.
/// 컴파일러는 이름이 아닌 숫자를 보고 판단하기 때문에 별명에 불과하다.
/// 분류 부호를 파라미터로 받는 메소드는 숫자만을 인자로 받으며 이름을 전달할 수는 없다.
/// 그래서 코드를 이해하기 힘들어질 수 있다.
/// 
/// 숫자형 분류 부호를 클래스로 빼내면 컴파일러는 그 클래스 안에서 종류 판단을 수행할 수 있다.
/// 클래스 안에 팩토리 메소드를 작성하면 유효한 인스턴스만 생성되는 지와 그런 인스턴스가 적절한 객체로
/// 전달되는지를 정적으로 검사할 수 있다.
/// 
/// 분류부호를 클래스로 만드는 건 분류 부호가 순수한 데이터일 때만 실시해야 한다.
/// 분류부호 switch 문 안에 사용되어 다른 기능을 수행하거나 메소드를 호출할 땐 클래스로 전환하면 안된다.
/// switch문에는 임의로 클래스를 사용할 수 없으며 오직 정수 타입만을 사용하자.
/// 
/// </summary>

namespace RefactoringStudy._48_ReplaceTypeCodeWithClass
{
    // before
    class Person
    {
        public static int O = 0;
        public static int A = 1;
        public static int B = 2;
        public static int AB = 3;

        public int BloodGroup { get; private set; }

        public Person(int bloodGroup)
        {
            BloodGroup = bloodGroup;
        }
    }

    // after
    class Person2
    {
        public BloodGroup BloodGroup { get; private set; }

        public Person2(BloodGroup bloodGroup)
        {
            BloodGroup = bloodGroup;
        }
    }

    class BloodGroup
    {
        public static BloodGroup O = new BloodGroup(0);
        public static BloodGroup A = new BloodGroup(1);
        public static BloodGroup B = new BloodGroup(2);
        public static BloodGroup AB = new BloodGroup(3);
        
        private int Code { get;  set; }

        private BloodGroup(int code)
        {
            Code = code;
        }

        public override bool Equals(object obj)
        {
            return Code == ((BloodGroup)obj).Code;
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}
