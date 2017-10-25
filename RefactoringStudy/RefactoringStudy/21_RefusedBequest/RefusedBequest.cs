using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 방치된 상속물 (Refused Bequest)
/// 
/// 상속받은 메소드나 데이터가 하위클래스에서 더 이상 쓰이지 않거나 필요없을 때
/// 
/// '기존에는' 문제의 원인이 잘못된 계층구조에서 나온다고 하였다. 
///  => 이 경우 새로운 대등 클래스를 작성하고 메소드, 필드 하향을 실시해서 상위 클래스는 공통코드만 들어있게한다.
/// 
/// 하위 클래스가 기능은 재사용하지만 상위클래스의 인터페이스를 지원하지 않을 때 문제가 된다. 
/// 상속구현을 거부하는 것은 상관없지만, 인터페이스를 거부하는 것은 심각한 문제다. 
/// 계층 구조를 건드려서는 안되고, "상속을 위임으로 전환" 기법을 적용해서 계층구조를 없애야 한다.
/// </summary>

namespace RefactoringStudy._21_RefusedBequest
{
    class Stack<T> : List<T>
    {
        public void Push(T element)
        {
            Insert(0, element);
        }

        public T Pop()
        {
            var result = this[0];
            RemoveAt(0);
            return result;
        }
    }

    ////////////////////////////////////////////////////////////////

    class Stack2<T>
    {
        private List<T> elements = new List<T>();

        public void Push(T element)
        {
            elements.Insert(0, element);
        }

        public T Pop()
        {
            var result = elements[0];
            elements.RemoveAt(0);
            return result;
        }
    }
}
