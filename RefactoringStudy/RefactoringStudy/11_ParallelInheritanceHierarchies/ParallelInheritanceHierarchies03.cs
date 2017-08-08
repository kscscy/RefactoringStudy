using System;
/// <summary>
/// 단 하나의 계층 구조만 유지하기
/// 장점 : 유지보수가 쉽다
/// 단점 : SRP 원칙에 어긋난다.
/// </summary>
/// 
namespace RefactoringStudy._11_ParallelInheritanceHierarchies
{
    public interface 연구원업무표준
    {
        string getType();
        void setType(string type);
        int getSalary();
        void setSalary(int salary);
        string 할일();
        string 목적();
    }

    //
    public class 개발자_연구원 : 연구원업무표준
    {
        private string type;
        private int salary;

        public int getSalary()
        {
            return salary;
        }

        public string getType()
        {
            return type;
        }
                
        public void setSalary(int salary)
        {
            this.salary = salary;
        }

        public void setType(string type)
        {
            this.type = type;
        }

        public string 할일()
        {
            return "프로그래밍";
        }

        public string 목적()
        {
            return "체계적인 개발";
        }
    }
        

    //
    public class 기획자_연구원 : 연구원업무표준
    {
        private string type;
        private int salary;

        public int getSalary()
        {
            return salary;
        }

        public string getType()
        {
            return type;
        }

        public void setSalary(int salary)
        {
            this.salary = salary;
        }

        public void setType(string type)
        {
            this.type = type;
        }

        public string 할일()
        {
            return "기획";
        }

        public string 목적()
        {
            return "체계적인 기획";
        }
    }

    public class 관리자
    {
        static void Main()
        {
            연구원 A = new 개발자_연구원();
            A.setType("선임연구원");
            A.setSalary(100);
           
            연구원 B = new 기획자_연구원();
            B.setType("수석연구원");
            B.setSalary(1000);
        }
    }
}
