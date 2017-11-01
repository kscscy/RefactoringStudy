using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 메서드를 메서드 객체로 전환 (Replace Method With Method Object)
/// 
/// 지역변수 때문에 메소드 추출을 적용할 수 없는 긴 메소드가 있을 땐
/// 그 메소드 자체를 객체로 전환해서 모든 지역변수를 객체의 필드로 만든다.
/// 그런 다음 그 메소드를 객체 안의 여러 메소드로 쪼개면 된다.
/// 
/// 메소드 분해를 어렵게 만드는 것은 지역변수다. 지역변수가 많으면 메소드를 쪼개기 힘들 수 있다.
/// 
/// </summary>

namespace RefactoringStudy._26_ReplaceMethodWithMethodObject
{
    public class Order
    {
        //...
        public double Price()
        {
            double primaryBasePrice;
            double secondaryBasePrice;
            double tertiaryBasePrice;
            // long computation.
            //...
            return 0;
        }
    }
    ///////////////////////////////////////////////////////////////


    public class Order2
    {
        //...
        public double Price()
        {
            return new PriceCalculator(this).Compute();
        }
    }

    public class PriceCalculator
    {
        private double primaryBasePrice;
        private double secondaryBasePrice;
        private double tertiaryBasePrice;

        public PriceCalculator(Order2 order)
        {
            // copy relevant information from order object.
            //...
        }

        public double Compute()
        {
            // long computation.
            //...
            return 0;
        }
    }
}
