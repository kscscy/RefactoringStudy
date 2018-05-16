using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 조건문의 공통 실행 코드 빼내기 (Consolidate Duplicate Conditional Fragments)
/// 
/// 조건문의 모든 절에 같은 실행 코드가 있을 땐 같은 부분을 조건문 밖으로 빼자
/// 
/// 각 절이 공통적으로 실행할 기능과 서로 다르게 실행할 기능을 한눈에 알 수 있다.
/// 공통 코드가 조건문의 앞 절에 있을 땐 조건문 앞으로 빼자.
/// 공통 코드가 조건문의 끝 절에 있을 땐 조건문 뒤로 빼자.
/// 공통 코드 명령이 둘 이상일 땐 메소드로 만들자.
/// 
/// </summary>

namespace RefactoringStudy._54_ConsolidateDuplicateConditionalFragments
{
    class ConsolidateDuplicateConditionalFragments
    {
        private bool isSpecialDeal;
        public double total { get; set; }
        public int price { get; set; }
        public void Send()
        {

        }

        //before
        void Test()
        {
            if (isSpecialDeal)
            {
                total = price * 0.95;
                Send();
            }
            else
            {
                total = price * 0.98;
                Send();
            }
        }

        //after
        void Test2()
        {
            if (isSpecialDeal)
                total = price * 0.95;
            else
                total = price * 0.98;
            Send();
        }

        // Exception 처리에도 이 방식을 적용할 수 있다.
        // try - catch 구간 안의 예외 발생 명령 뒤에 공통 코드가 들어 있으면  그 코드를 finally 구간으로 옮기면 된다.
    }
}
