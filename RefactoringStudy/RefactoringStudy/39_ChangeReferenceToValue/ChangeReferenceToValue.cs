using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 참조를 값으로 전환 (Change Reference to Value)
/// 
/// 참조 객체가 작고 수정할 수 없고 관리하기 힘들 땐
/// 그 참조 객체를 값 객체로 만들자.
/// 
/// Change Value to Refernce 기법과 마찬가지로, 
/// 참조 객체와 값 객체 중 무엇을 사용할지 판단하기가 쉽지만은 않다.
/// 결정하더라도 나중에 바꿔야 할 때가 많다.
/// 
/// 참조 객체를 사용한 작업이 복잡해지는 순간이 참조를 값으로 바꿔야 할 시점이다.
/// 값 객체는 변경할 수 없어야 한다는 주요 특성이 있다.
/// '변경불가'의 뜻을 명확히 해야한다.
/// 
/// ex) Currency(통화)
/// 1. 전환할 객체가 변경불가인지 변경 가능인지 검사하자.
/// 2. Equals와 HashCode 메소드를 작성하자.
/// 3. 팩토리 메소드를 삭제하고, 생성자를 public으로 만들어야 좋을지 생각해보자.
/// </summary>
namespace RefactoringStudy._39_ChangeReferenceToValue
{
    //before
    class Currency
    {
        public string Name { get; private set; }

        private Currency(string name)
        {
            Name = name;
        }

        public static Currency Create(string name)
        {
            return new Currency(name);
        }
    }

    //after
    class Currency2
    {
        public string Name { get; private set; }

        public Currency2(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return Name.Equals(((Currency2)obj).Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    class Test
    {
        public bool TestFunc()
        {
            return new Currency2("JPY").Equals(new Currency2("JPY"));
        }
    }
}
