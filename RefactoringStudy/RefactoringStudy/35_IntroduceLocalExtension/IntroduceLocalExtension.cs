using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 국소적 상속확장 클래스 사용(Introduce Local Extension)
/// 
/// 사용 중인 서버 클래스에 여러 개의 메소드를 추가해야 하는데 클래스를 수정할 수 없을 땐,
/// 새 클래스를 작성하고 그 안에 필요한 여러 개의 메소드를 작성하자.
/// 이 상속확장 클래스를 원본 클래스의 하위 클래스나 래퍼 클래스로 만들자.
/// 
/// 추가해야 하는 메소드가 한 두개 일때에는 Introduce Foreign Method 기법을 사용하면 된다.
/// 하지만 필요한 메소드 수가 세 개 이상이면 적당한 곳에 모아둬야 한다.
/// 
/// 국소적 상속확장 클래스는 별도의 클래스지만 상속확장하는 클래스의 하위 타입이다. 
/// 원본 클래스의 모든 기능도 사용하면서 추가 기능도 들어있다. 
/// 원본 클래스를 사용할 것이 아니라 국소적 상속확장 클래스를 인스턴스화해서 사용하자.
/// 
/// 
/// </summary>
namespace RefactoringStudy._35_IntroduceLocalExtension
{
    class IntroduceLocalExtension
    {
    }
}
