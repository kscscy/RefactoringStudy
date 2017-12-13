using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 외래 클래스에 메소드 추가 (Introduce Foreign Method)
/// 
/// 사용 중인 서버 클래스에 메소드를 추가해야 하는데 그 클래스를 수정할 수 없을 땐,
/// 클라이언트 클래스 안에 서버 클래스의 인스턴스를 첫 번째 인자로 받는 메소드를 작성하자.
/// 
/// 기능이 필요하다. 그런데 원본 클래스를 수정할 수 없다면 그 메소드를 클라이언트 클래스 안에 작성해야 한다.
/// 메소드를 여러번 사용한다면 여기저기 중복 작성할 수도 있다. 
/// 이때 새로 만들 메소드를 외래 메소드로 만들면 원래 서버 메소드에 있어야할 메소드임을 분명히 나타낼 수 있다.
/// 
/// </summary>
namespace RefactoringStudy._34_IntroduceForeignMethod
{
    //before
    class Report
    {
        void sendReport()
        {
            DateTime nextDay = previousEnd.AddDays(1);
        }
    }

    class Report2
    {
        void sendReport()
        {
            DateTime nextDay = nextDay(previousEnd);
        }

        private static DateTime NextDay(DateTime date)
        {
            return date.AddDays(1);
        }
    }
}
