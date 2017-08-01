// 09. 강박적 기본 타입 사용 Primitive Obsession
// 원시타입 필드를 만드는 것은 새로운 클래스를 만드는 것 보다 쉽다?
// 돈관련 클래스나 문자열 클래스 등의 사소한 작업에 객체를 잘 사용하지 않으려는 경향이 있다.
// => 데이터 값을 객체로 전환한다. (Replace Data Vlaue with Object) 
// => 데이터와 관련된 동작을 클래스로 옮긴다.
// => 뭉쳐 다녀야 할 여러 개의 필드가 있다면 클래스 추출 

using System;

namespace RefactoringStudy._09_PrimitiveObsession
{

    public class ZipCodeItem_Old
    {
        int id = 0;
        string frontCode = "";
        string backCode = "";
        string province = "";
        string city = "";
        string village = "";
        int manPaymentBan = 0;
        int deliveryTimeBan = 0;
        int deliveryAmtAdd = 0;
        int intDeliverAmt = 0;

        //...
    }

    //////////////////////////////////////////////////////////////////////////////////////
    public class ZipCodeItem
    {
        public FrontCode FrontCode { get; set; }
    }

    public class FrontCode
    {
        private readonly string _value;

        public FrontCode(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

        public override string ToString()
        {
            return _value;
        }

        public static implicit operator string(FrontCode frontCode)
        {
            return frontCode.Value;
        }

        public static explicit operator FrontCode(string value)
        {
            return new FrontCode(value);
        }
    }

    //////
    public class Controller
    {
        private void Do()
        {
            ZipCodeItem zipCodeItem = new ZipCodeItem();

            // 생성자
            zipCodeItem.FrontCode = new FrontCode("000");

            // 명시적 형변환
            zipCodeItem.FrontCode = (FrontCode)"000";

            // 암시적 형변환
            string fCode = zipCodeItem.FrontCode;
        }
    }
}


