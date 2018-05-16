using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 제어 플래그 제거 (Remove Control Flag)
/// 
/// 논리 연산식의 제어 플래그 역할을 하는 변수가 있을 땐 그 변수를 break 문이나 return문으로 바꾸자
/// 
/// 논리문을 빠져나오게 하는 제어 플래그 값을 찾자.
/// 제어 플래그 값을 대입하는 코드를 break문이나 continue문으로 바꾸자
/// 
/// break 문이나 continue 문이 없는 언어에서는 다음과 같이 하자
/// - 로직을 메소드로 빼자.
/// - 논리문을 빠져나오게 하는 제어 플래그 값을 찾자.
/// - 빠져나오게 하는 값을 return 문으로 바꾸자.
/// 
/// </summary>

namespace RefactoringStudy._55_RemoveControlFlag
{
    class RemoveControlFlag
    {
        void SendAlert()
        {

        }

        //before
        void Test1(IEnumerable<string> people)
        {
            var foundPerson = false;
            foreach (var person in people)
            {
                if (!foundPerson)
                {
                    if ("Don" == person)
                    {
                        SendAlert();
                        foundPerson = true;
                    }
                    if ("John" == person)
                    {
                        SendAlert();
                        foundPerson = true;
                    }
                }
            }
        }

        //after - 제어 플래그를 break문으로
        void Test1_1(IEnumerable<string> people)
        {
            // 2. 제어 플래그 제거
            //var foundPerson = false;
            foreach (var person in people)
            {
                // 2.
                //if (!foundPerson)

                if ("Don" == person)
                {
                    SendAlert();
                    //foundPerson = true;
                    break; // 1. break
                }
                if ("John" == person)
                {
                    SendAlert();
                    //foundPerson = true;
                    break;
                }

            }
        }

        //before
        void Test2(IEnumerable<string> people)
        {
            var foundPerson = string.Empty;
            foreach (var person in people)
            {
                if (foundPerson == "")
                {
                    if ("Don" == person)
                    {
                        SendAlert();
                        foundPerson = person;
                    }
                    if ("John" == person)
                    {
                        SendAlert();
                        foundPerson = person;
                    }
                }
            }
            Console.WriteLine(foundPerson);
        }

        //after - 제어 플래그를 reteurn문으로
        void Test2_1(IEnumerable<string> people)
        {
            //string foundPerson = NewMethod(people);
            //Console.WriteLine(foundPerson);
            Console.WriteLine(NewMethod(people));
        }

        // 1. 메소드 추출
        private string NewMethod(IEnumerable<string> people)
        {
            // 3.제어 플래그 제거
            //var foundPerson = string.Empty;
            foreach (var person in people)
            {
                //if (foundPerson == "")
                if ("Don" == person)
                {
                    SendAlert();
                    //foundPerson = person;
                    return person; // 2. return문으로 교체
                }
                if ("John" == person)
                {
                    SendAlert();
                    //foundPerson = person;
                    return person;
                }
            }

            return string.Empty; //3.
        }
    }
}
