using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 필드 캡슐화(Encapsulate Field)
/// 
/// public 필드가 있을 땐
/// 그 필드를 private로 만들고 필드용 읽기, 쓰기 메소드를 작성하자
/// 
/// 객체지향의 주요 원칙 중 하나는 캡슐화다. 캡슐화를 데이터은닉 이라고 부르기도 한다.
/// 데이터는 절대로 public 타입으로 선언하면 안된다.
/// 데이터가 있는 객체가 모르는 사이에 다른 객체가 데이터값을 읽고 변경할 수 있다.
/// 
/// 필드 캡슐화의 첫 단계는 데이터를 은닉하고 읽기/쓰기 메소드를 추가하는 것이다.
/// 읽기/쓰기 메소드만 있는 클래스는 객체의 장점을 활용하지 않은 클래스이다.
/// 필드 캡슐화를 적용한 후에는 새 메소드를 사용하는 메소드를 찾아 그 메소드를 묶어서 메소드 이동을 적용해준다.
/// 
/// </summary>


namespace RefactoringStudy._45_EncapsulateField
{
    class EncapsulateField
    {
        // before
        public string Name { get; set; }

        // after
        private string _name;
        public string Name2
        {
            get { return _name; }
            set { _name = value; }
        }

    }
}
