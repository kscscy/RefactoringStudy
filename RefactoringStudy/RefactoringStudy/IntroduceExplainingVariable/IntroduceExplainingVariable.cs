using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 직관적 임시변수 사용 (Introduce Explaining Variable)
/// 
/// 사용된 수식이 복잡할 땐 수식의 결과나 수식의 일부분을 용도에 부합하는 직관적 이름의 임시변수에 대입하자
/// 
/// 조건문에서 각 조건 절을 가져와서 직관적 이림의 임시변수를 사용해 그 조건의 의미를 설명하려 할 때 많이 사용한다.
/// 그 외에 긴 알고리즘에서 임시변수를 사용해서 계산의 각 단계를 설명할 수 있을 때에도 사용한다.
/// => 임시변수를 남용하면 코드를 보는 메소드만 복잡해지고 보는사람이 이해하기 힘들 수도 있다. 
/// => 그렇다면 메소드추출을 사용하려 노력한다. 임시변수는 하나의 메소드 안에서만 사용한다.
/// => 지역변수로 인해 메소드추출이 적용하기 힘들 때에는 어쩔 수 없이 직관적 임시변수를 사용한다.
/// </summary>

namespace RefactoringStudy.IntroduceExplainingVaraiable
{
    class IntroduceExplainingVaraiable
    {
        // PrintSampleService.cs 
        // ... 
        if(refundPrePaidPoint >= 0)
        {
            if(orderItem.PayState.Equals("SBPAYMENT") || orderItem.PayState.Equals("TEMPPAYMENT"))
            {
                orderItem.PayType = "POINT";
                orderItem.PayState = "PAYMENT";
            }
            orderItem.PrePaidPoint -= refundPrePaidPoint;
        }
        //...

        // 임시변수를 사용해보기
        bool isRefundPrePaidPoint = refundPrePaidPoint >= 0;
        bool isSbPayment = orderItem.PayState == "SBPAYMENT";
        bool isTempPayment = orderItem.PayState == "TEMPPAYMENT";

        if (isRefundPrePaidPoint)
        {
            if(isSbPayment || isTempPayment)
            {
                orderItem.PayType = "POINT";
                orderItem.PayState = "PAYMENT";
            }
            orderItem.PrePaidPoint -= refundPrePaidPoint;
        }

        // 메소드 추출
        public bool checkRefundPayState(string orderItem.PayState)
        {
            return orderItem.PayState == "SBPAYMENT" || orderItem.PayState == "TEMPPAYMENT";
        }
    }
}
