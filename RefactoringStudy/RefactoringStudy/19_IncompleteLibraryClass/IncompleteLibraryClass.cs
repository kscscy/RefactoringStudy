using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 미흡한 라이브러리 클래스 (Incomplete Library Class)
/// 
/// 라이브러리 클래스를 만드는 사람이 모든 것을 알고 만든 것은 아니다. 자신이 직접 완성하지 않는 이상 설계를 파악하는 것은 불가능하다.
/// 원하는 기능을 수행하게 수정하는 것이 힘들며, 메소드 이동같은 방법도 사용할 수 없다.?
/// 
/// 라이브러리 클래스에 넣어야 할 메소드가 두개뿐이라면 외부클래스에 메소드 추가를
/// 부가기능이 많을 때에는 상속확장 클래스 기법을 사용한다.
/// 
/// Introduce Foreign Method : "이 클래스는 왜 이 기능이 없는 거야?"
/// - 원본 클래스를 수정할 수 없다면 그 메소드를 클라이언트 클래스 안에 작성해야 한다.
/// - 메소드를 클래스 안에서 한번만 사용하면 문제가되지 않지만 여러번 사용한다면 중복이된다.
/// - 다른 클래스에서 사용할 경우 유틸리티 클래스 래퍼를 만들어서 메소드를 사용하면 도움이 된다. => Introduce Local Extension
/// </summary>

namespace RefactoringStudy._19_IncompleteLibraryClass
{
    class IncompleteLibraryClass
    {
        void SendReport()
        {
            DateTime nextDay = previousEnd.AddDays(1);
            //...
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////
    class Report
    {
        void SendReport()
        {
            DateTime nextDay = NextDay(previousEnd);
            //...
        }

        private static DateTime NextDay(DateTime datetime)
        {
            return datetime.AddDays(1);
        }
    }
}
