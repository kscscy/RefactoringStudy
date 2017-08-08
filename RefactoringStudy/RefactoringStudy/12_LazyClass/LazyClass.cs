/// <summary>
/// 11. 직무유기 클래스 (Lazy Class)
/// 리팩토링으로 인해 기능이 축소된 클래스
/// 또는 수정할 예정이었으나 수정하지 않아 쓸모없어진 클래스
/// 거의 쓸모없는 구성요소는 Inline Class(클래스의 기능을 다른 클래스로 이식)로
/// 함수가 거의 없는 서브클래스는 Collapse Hierarchy(서브클래스와의 병합) 로
/// 
/// </summary>
namespace RefactoringStudy._12_LazyClass
{
    // 1
    public class Postcode
    {
        public string postcode;

        public Postcode(string postcode)
        {
            this.postcode = postcode;
        }

        public string getPostcode()
        {
            return postcode;
        }

        public string getPostcodeArea()
        {
            return postcode.Split('-')[0];
        }
    }

    public class Address_Old
    {
        public string street;
        public string city;
        public Postcode postcode;

        public Address_Old(string street, string city, Postcode postcode)
        {
            this.street = street;
            this.city = city;
            this.postcode = postcode;
        }

        public string getAddress()
        {
            return postcode.getPostcode() + "\n" + city + "\n" + street; 
        }
    }

 //////////////////////////////////////////////////////////////////// 
    public class Address
    {
        public string street;
        public string city;
        public string postcode;

        public Address(string street, string city, string postcode)
        {
            this.street = street;
            this.city = city;
            this.postcode = postcode;
        }

        public string getAddress()
        {
            return postcode + "\n" + city + "\n" + street;
        }

        public string getPostcodeArea()
        {
            return postcode.Split('-')[0];
        }

        /////////
    }
    
    // 2
    public class ShippingAddress : Address
    {
        public ShippingAddress(string street, string city, string postcode) 
            : base(street, city, postcode)
        {
        }

        public double calcPrice()
        {
            // Address로 올린다
            return city == "jeju" ? 5000 : 2500;
        }
    }
}
