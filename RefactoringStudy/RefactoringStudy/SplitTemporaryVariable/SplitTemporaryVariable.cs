using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 임시변수 분리 (Split Temporary Variable)
/// 
/// 루프변수나 값 누적용 임시변수가 아닌 임시변수에 여러번 값이 대입될 땐 각 대입마다 다른 임시변수를 사용하자
/// 긴 코드의 계산 결과를 나중에 간편히 참조할 수 있게 저장하는 용도로 사용하는 임시변수에는 값이 한 번만 대입되어야한다.
/// 두 번 이상 대입된다는 건 그 변수가 메소드 안에 여러 용도로 사용된다는 증거이다.
/// 여러 용도로 사용되는 변수는 각 용도별로 다른 변수를 사용하게 분리해야 한다.
/// </summary>
namespace RefactoringStudy.SplitTemporaryVariable
{
    class SplitTemporaryVariable
    {
        public void PrintItem()
        {
            int result = 0;
            foreach (item in selectList)
            {
                if (item.selected)
                {
                    result += 1;
                }
            }

            Console.WriteLine("totl selected : " + result);
            result = 0;

            foreach (item in PriceList)
            {
                result += item.Price;
            }
            Console.WriteLine("totl price : " + result);
        }
    }
}
