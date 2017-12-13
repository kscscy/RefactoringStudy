using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 필드 자체 캡슐화 (Self Encapsulate Field)
/// 
/// 필드에 직접 접근하던 중 그 필드로의 결합에 문제가 생길 땐,
/// 그 필드용 get/set 메소드를 작성해서 두 메소드를 통해서만 필드에 접근하게 만들자.
/// 
/// 변수 간접 접근 방식을 사용하면 하위 클래스가 메소드에 해당 정보를 가져오는 방식을 재정의 할 수 있으며, 
/// 데이터관리가 유연해진다는 장점이 있다.
/// 변수 직접 접근 방식의 장점은 코드를 알아보기가 쉽다는 것이다.
/// </summary>
namespace RefactoringStudy._36_SelfEncapsulateField
{
    // before
    class Range
    {
        private int low, high;

        bool Includes(int arg)
        {
            return arg >= low && arg <= high;
        }
    }

    // after
    class Range2
    {
        private int low, high;

        int Low
        {
            get { return low; }
        }
        int High
        {
            get { return high; }
        }

        bool Includes(int arg)
        {
            return arg >= Low && arg <= High;
        }
    }
}
