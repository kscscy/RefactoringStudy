using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 마법 숫자를 기호 상수로 전환 (Replace Magic Number With Symbolic Constant)
/// 
/// 특수 의미를 지닌 리터럴 숫자가 있을 땐 
/// 의미를 살린 이름의 상수를 작성한 후 리터럴 숫자를 그 상수로 교체하자.
/// 
/// 여러곳에서 논리적으로 같은 숫자를 참조해야 할 때 특히 문제가 많다.
/// 숫자가 변경될 가능성이 있다면 끔찍한 일이다.
/// 변경하지 않더라도 코드에서 어떻게 처리되고 있는지 알아내기 힘들다.
/// 
/// 숫자가 분류 부호라면 부호를 클래스로 전환 기법을 적용하고
/// 숫자가 배열의 길이라면 length를 사용해서 루프를 실행하자
/// </summary>
namespace RefactoringStudy._44_ReplaceMagicNumberWithSymbolicConstant
{
    class ReplaceMagicNumberWithSymbolicConstant
    {
        // before
        double getCircleArea(int radius)
        {
            return radius * radius * 3.14;
        }

        // after
        public const double PI = 3.14;

        double getCircleArea2(int radius)
        {
            // Math.PI
            return Math.Pow(radius, radius) * PI;
        }
    }
}
