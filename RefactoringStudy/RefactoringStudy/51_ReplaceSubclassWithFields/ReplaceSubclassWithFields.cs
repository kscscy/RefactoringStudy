using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 하위클래스를 필드로 전환 (Replace Subclass With Fields)
/// 
/// 여러 하위클래스가 상수 데이터를 반환하는 메소드만 다룰 땐
/// 각 하위클래스의 메소드를 상위클래스 필드로 전환하고 하위클래스는 전부 삭제하자.
/// 
/// 기능을 추가하거나 기능을 조금씩 달리할 하위클래스를 작성하자.
/// 다형적인 기능의 한 형태는 상수메소드다. 상수메소드는 하드코딩된 값을 반환하는 메소드다.
/// 상수 메소드는 읽기 메소드에 각기 다른 값을 반환하는 하위클래스에 넣으면 유용하다.
/// 상위 클래스 안에 읽기 메소드를 정의하고 그 읽기 메소드를 하위 클래스에서 다양한 값으로 구현하자.
/// 
/// 상수 메소드가 유용하긴 해도, 하위클래스를 상수 메소드로만 구성한다고 해서 효용성이 커지는 것은 아니다.
/// 상위클래스 안에 필드를 넣고 그런 하위클래스는 완전히 삭제하면 된다.
/// 그렇게 하면 하위클래스의 불필요한 복잡함도 사라진다.
/// 
/// </summary>

namespace RefactoringStudy._51_ReplaceSubclassWithFields
{
    //
    abstract class Person
    {
        public abstract bool IsMale { get; }
        public abstract char Code { get; }
    }

    class Male : Person
    {
        public override bool IsMale
        {
            get
            {
                return true;
            }
        }
        public override char Code
        {
            get
            {
                return 'M';
            }
        }
    }

    class Female : Person
    {
        public override bool IsMale
        {
            get
            {
                return false;
            }
        }
        public override char Code
        {
            get
            {
                return 'F';
            }
        }
    }

    //after 
    // 6. abstract 제거
    /*abstract*/ class Person2
    {
        // 2. 상위클래스에 상수메소드별 필드 선언
        // 8. get set 수정
        //private bool _isMale;
        //private char _code;

        // 5. 상위클래스에 읽기 메소드를 넣어 필드를 사용하게 만들고 하위클래스의 메소드는 삭제
        //public abstract bool IsMale { get; }
        //public abstract char Code { get; }
        // 8. get set 수정
        public bool IsMale { get; private set; }
        public char Code { get; private set; }

       

        // 3. 상위클래스에 protected 타입 생성자 메소드 추가
        protected Person2(bool isMale, char code)
        {
            IsMale = isMale;
            Code = code;
        }

        // 1. 생성자를 팩토리 메소드로 전환
        public static Person2 CreateMale()
        {
            //6. 하위 클래스의 내용을 모두 삭제했으면 메소드 내용 직접삽입을 실시해서
            // 하위클래스 생성자 내용을 상위클래스 안에 직접 넣기
            //return new Male2();
            return new Person2(true, 'M');
        }

        public static Person2 CreateFemale()
        {
            return new Person2(false, 'F');
        }
    }

    //7. 하위클래스 삭제
    //class Male2 : Person2
    //{
    //    //5. 상위클래스에 읽기 메소드를 넣어 필드를 사용하게 만들고 하위클래스의 메소드는 삭제
    //    //public override bool IsMale
    //    //{
    //    //    get
    //    //    {
    //    //        return true;
    //    //    }
    //    //}
    //    //public override char Code
    //    //{
    //    //    get
    //    //    {
    //    //        return 'M';
    //    //    }
    //    //}

    //    // 4. 새로 작성한 생성자를 호출하는 생성자 메소드 추가
    //    public Male2() : base(true, 'M') { }
    //}

    //7. 하위클래스 삭제
    //class Female2 : Person2
    //{
    //    //5. 상위클래스에 읽기 메소드를 넣어 필드를 사용하게 만들고 하위클래스의 메소드는 삭제
    //    //public override bool IsMale
    //    //{
    //    //    get
    //    //    {
    //    //        return false;
    //    //    }
    //    //}
    //    //public override char Code
    //    //{
    //    //    get
    //    //    {
    //    //        return 'F';
    //    //    }
    //    //}

    //    // 4. 새로 작성한 생성자를 호출하는 생성자 메소드 추가
    //    public Female2() : base(false, 'F') { }
    //}
}
