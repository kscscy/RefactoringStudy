using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 레코드를 데이터 클래스로 전환 (Replace Record With Data Class)
/// 
/// 전통적인 프로그래밍 환경에서 레코드 구조를 이용한 인터페이스를 제공해야 할 땐
/// 레코드 구조를 저장할 덤(dumb) 데이터 객체를 작성하자.
/// 
/// 객체지향 프로그램에서 레코드 구조를 사용하는 이유는 다양하다.
/// 구버젼 프로그램을 복사할 수도 있고, 구조화된 레코드를 기존의 프로그래밍 API나 데이터베이스 레코드와 소통하게 할 수도 있다.
/// 
/// 이러한 외부 요소를 처리할 인터페이스 역할을 하는 클래스를 작성하는 것이 좋다.
/// 클래스를 외부 레코드처럼 만드는 것은 간단하다.
/// 다른 필드와 메소드는 나중에 클래스로 옮기면 된다.
/// 
/// - 레코드를 표현할 클래스를 작성하자.
/// - 그 클래스에 각 데이터 항목에 대한 읽기/쓰기 메소드를 작성하고 private 필드를 선언하자.
/// 
/// </summary>

namespace RefactoringStudy._47_ReplaceRecordWithDataClass
{
    class ReplaceRecordWithDataClass
    {
    }
}
