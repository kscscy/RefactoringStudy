using System;
/// <summary>
/// 메시지 체인 (Message Chains)
/// 
/// A객체가 B객체를 다시 B객체가 C객체를 요청하는 식의 연쇄적 요청이 발생하는 문제점
/// 관계가 변경될 경우 클라이언트 객체들도 수정을 해야 한다.
/// 
/// foo.getBar().getFoo().getThis().getThat().doSomething();
/// 수많은 코드 행이 든 getThis 메서드나 임시변수 세트라고 봐도 된다.
/// 
/// 객체가 어느 대상에 사용되는지 알아내고 메서드 추출이나 메서드 이동을 수행한다.
/// 
/// 규모가 크고 복잡한 문제를 쪼개서 해결하는데 OOP는 큰 도움이 됩니다.  하지만, 무조건 쪼개는 것만이 능사는 아닙니다.  너무 쪼개다 보면 오히려 전체를 가늠하기 어려운 코드로 변질 될 수 있습니다.  이럴 때 공식같은 것이 있어서 "어디까지 쪼개야 된다"하고 계산 할 수 있다면 좋겠지만, 그것은 불가능에 가깝습니다.
/// 일단은 자신의 직관을 믿어야 합니다.  그것이 최선입니다.  그리고, 이후 리팩토링을 통해서 다듬어 가는 것이 정답입니다.  규모가 크고 복잡한 문제를 한 번에 해결하려는 것 자체가 욕심입니다.
/// </summary>
namespace RefactoringStudy._15_MessageChains
{
    class SqlTool
    {
        private string _values, _table, _condition, _result;

        public SqlTool Select(string values = "")
        {
            _values = string.Format("select {0}", values);
            return this;
        }

        public SqlTool Update(string table = "")
        {
            _table = table;
            return this;
        }

        public SqlTool Set(string name = "", string value = "")
        {
            _values = string.Format("{0} = {1}", name, value);
            return this;
        }

        public SqlTool From(string table = "")
        {
            _table = table;
            return this;
        }

        public SqlTool Where(string condition = "")
        {
            _condition = condition;
            return this;
        }

        public string Execute()
        {
            _result = string.Format("{0} {1}", _values, _table);

            if (!string.IsNullOrEmpty(_condition))
            {
                _result += string.Format("where {0}", _condition);
            }
            return _result;
        }

        public string SelectUser()
        {
            SqlTool tool = new SqlTool();
            return tool.Select("strUserId").From("tblUser").Where("email='abc@test'").Execute();
            // tool.Select().From().Execute(); => Ok

            // tool.Where().Select().From().Execute();
            // tool.Select().Select().From().Execute();

        }
    }

    class Test0 
    {
        public Test1 A()
        {
            //...
        }
    }

    class Test1 
    {
        public Test2 B()
        {
            //...
        }
    }

    class Test2 
    {
        
        public Test2 B()
        {
            //...
        }


        public Test2 C()
        {
            //...
        }

        public string Do()
        {
            // Do...
        }
    }

    public class TestClass
    {
        public void test()
        {
            Test0 test = new Test0();
            string t1 = test.A().B().Do();
            string t2 = test.A().B().B().Do();
        }
    }

    interface IStart
    {
        IMiddle A();
    }

    interface IMiddle
    {
        IFinal B();
    }

    interface IFinal
    {
        IFinal B();
        IFinal C();
        string Do();
    }

    class Test : IStart, IMiddle, IFinal
    {
        public IMiddle A()
        {
            return this;
        }

        public IFinal B()
        {
            return this;
        }

        public IFinal C()
        {
            return this;
        }

        public string Do()
        {
            return "";
        }
    }

    public class TestClass2
    {
        public void test()
        {
            IStart st = new Test();
            string t1 = st.A().B().Do();
            string t2 = st.A().B().C().Do();
            string t3 = st.A().B().B().B().C().C().C().Do();
        }
    }
}
