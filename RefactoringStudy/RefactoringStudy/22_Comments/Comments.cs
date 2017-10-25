using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 불필요한 주석(Comments)
/// 
/// 주석이 다 필요 없는 것은 아니다.
/// 엄청난 양의 주석은 코드의 특정 부분(풀어내지 못한, 구린내를 가리기 위한)을 감추고 있는 경우가 많다.
/// 
/// 주석을 넣어야겠다는 생각이 들 땐 먼저 코드를 리팩토링해서 주석을 없앨 수 있게 만들어보자
/// 주석은 무슨 작업을 해야 좋을지 모를 때만 넣는 것이 좋다.
/// 잊지 쉬운 사항을 주석으로 작성해 놓으면 다른 사람들이 수정하게 될 때 보고 쉽게 이해할 수 있다.
/// 어떤 코드 구간의 기능을 설명할 주석이 필요할 때에는 메소드 추출, 메소드 변경을 실시한다.
/// 
/// 시스템의 필수적인 상태에 관해 약간의 규칙을 설명해야 할 때
///  => Introduce Assertion : 코드의 한 부분이 프로그램의 상태에 대하여 어떤 것을 가정하고 있으면, assertion을 써서 가정을 명시되게(explicit) 만들어라.
/// TDD를 100% 수행한다면 꼭 필요하지 않을 것이라 생각한다.
/// 
/// </summary>
namespace RefactoringStudy._22_Comments
{
    class Comments
    {
        
    }
}
