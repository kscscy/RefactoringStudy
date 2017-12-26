using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 배열을 객체로 전환 (Replace Array with Object)
/// 
/// 배열을 구성하는 특정 원소가 별의별 의미를 지닐 땐
/// 그 배열을 각 원소마다 필드가 하나씩 든 객체로 전환하자.
/// 
/// 배열은 비슷한 객체들의 컬렉션을 일정한 순서로 담는 용도로만 사용해야 한다.
/// 객체를 사용하면 필드명과 메소드명을 사용하여 정보를 전달할 수 있다.
/// </summary>

namespace RefactoringStudy._40_ReplaceArrayWithObject
{
    // before
    class Example
    {
        void Test()
        {
            string[] test = new string[3];
            test[1] = "adprint";
            test[2] = "01";
            test[3] = "adprint.jp";

            string[] test2 = new string[3];
            test2[1] = "tqoon";
            test2[2] = "02";
            test2[3] = "tqoon.jp";
        }
    }

    // after
    public class Company
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public string Url { get; set; }

    }

    public class Example2
    {
        void Test2()
        {
            Company AdprintObject = new Company();
            AdprintObject.Name = "adprint";
            AdprintObject.Code = 1;
            AdprintObject.Url = "adprint.jp";

            Company TqoonObject = new Company();
            TqoonObject.Name = "tqoon";
            TqoonObject.Code = 2;
            TqoonObject.Url = "tqoon.jp";

        }
    }
}
