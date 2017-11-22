using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 필드 이동 (Move Field)
/// 
/// 어떤 필드가 자신이 속한 클래스보다 다른 클래스에서 더 많이 사용될 때는
/// 대상 클래스 안에 새 필드를 선언하고 그 필드 참조 부분을 전부 새 필드 참조로 수정하자.
/// 
/// 한 클래스에서 다른 클래스로 상태와 기능을 옮기는 것은 리팩토링의 기본.
/// 인터페이스에 따라 메소드를 옮기는 방법을 사용할 수도 있다. 그러나 메소드의 현재 위치가 적절하다고 판단되면 필드를 옮긴다.
/// 
/// 필드가 public이면 필드 캡슐화 기법을 실시하자.
/// 대상 클래스 안에 get/set 메소드와 함께 필드를 작성하자.
/// 원본 객체에서 대상 객체를 참조할 방법을 정하자.
/// 원본 클래스에서 필드를 삭제하자.
/// 원본 필드를 참조하는 모든 부분을 대상 클래스에 있는 적절한 메소드를 참조하게 수정하자.
/// </summary>

namespace RefactoringStudy._29_MoveField
{
    class MoveField
    {
    }
}
