using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 알고리즘 전환(Substitute Algorithm)
/// 
/// 알고리즘을 더 분명한 것으로 교체해야 할 땐, 해당 메소드의 내용을 새 알고리즘으로 바꾸자.
/// 기능을 수행하기 위한 간단한 방법이 있다면 복잡한 방법을 간단한 방법으로 교체해야 한다.
/// 리팩토링으로 복잡한 코드를 간단한 부분으로 쪼갤 수 있지만, 알고리즘을 전부 삭제하고 간단한 알고리즘으로 교체해야 하는 상황에 부딪힐 때도 있다.
/// 작업을 다르게 처리해야 해서 알고리즘을 변경할 때에도 변경하기 쉬운 알고리즘으로 교체하는 것이 간편하다.
/// 
/// 이렇게 하려면 메소드를 최대한 잘게 쪼개야 한다. 
/// 길고 복잡한 알고리즘은 수정하기 어려우므로, 간단한 알고리즘으로 교체해야만 수정하기 편해진다.
/// </summary>
namespace RefactoringStudy._27_SubstituteAlgorithm
{
    class SubstituteAlgorithm
    {
        public string FoundPerson(string[] people)
        {
            foreach(var person  in people)
            {
                if (person == "Don")
                    return person;
                if (person == "John")
                    return person;
                if (person == "Kent")
                    return person;
            }
            return string.Empty;
        }

        //////////////////////////////////////////////////////////////////////////

        public string FoundPerson2(string[] people)
        {
            return NewFoundPerson(people);
        }

        private string NewFoundPerson(string[] people)
        {
            var candidates = new List<string> { "Don", "John", "Kent" };
            foreach(var person in people)
            {
                if (candidates.Contains(person))
                    return person;
            }
            return string.Empty;
        }

        //////////////////////////////////////////////////////////////////////////

        public string FoundPerson3(string[] people)
        {
            var candidates = new List<string> { "Don", "John", "Kent" };
            foreach (var person in people)
            {
                if (candidates.Contains(person))
                    return person;
            }
            return string.Empty;
        }
    }
}
