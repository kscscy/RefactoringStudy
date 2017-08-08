using System;
/// <summary>
/// 11. 평행 상속 계층 ( Parallel Inheritance Hierachies)
/// “산재된 기능”의 특수한 케이스
/// 한 클래스의 하위 클래스를 만들 때마다 다른 클래스의 하위 클래스도 만들어야 하는 경우 
/// 중복된 코드 제거를 위해서는 한 상속 계층의 인스턴스가 다른 계층의 인스턴스를 참조하면 됨
/// </summary>
/// 
namespace RefactoringStudy._11_ParallelInheritanceHierarchies
{
    public interface 연구원
    {
        string getType();
        void setType(string type);
        int getSalary();
        void setSalary(int salary);
        업무표준 get업무표준();
        void set업무표준(업무표준 표준);
    }

    public interface 업무표준
    {
        string 할일();
        string 목적();
    }

    //
    public class 개발자_연구원 : 연구원
    {
        private string type;
        private int salary;
        private 업무표준 표준;

        public int getSalary()
        {
            return salary;
        }

        public string getType()
        {
            return type;
        }

        public 업무표준 get업무표준()
        {
            return 표준;
        }

        public void setSalary(int salary)
        {
            this.salary = salary;
        }

        public void setType(string type)
        {
            this.type = type;
        }

        public void set업무표준(업무표준 표준)
        {
            this.표준 = 표준;
        }
    }

    public class 개발자_업무표준 : 업무표준
    {
        public string 목적()
        {
            return "체계적인 개발";
        }

        public string 할일()
        {
            return "프로그래밍";
        }
    }

    //
    public class 기획자_연구원 : 연구원
    {
        private string type;
        private int salary;
        private 업무표준 표준;

        public int getSalary()
        {
            return salary;
        }

        public string getType()
        {
            return type;
        }

        public 업무표준 get업무표준()
        {
            return 표준;
        }

        public void setSalary(int salary)
        {
            this.salary = salary;
        }

        public void setType(string type)
        {
            this.type = type;
        }

        public void set업무표준(업무표준 표준)
        {
            this.표준 = 표준;
        }
    }

    public class 기획자_업무표준 : 업무표준
    {
        public string 목적()
        {
            return "체계적인 기획";
        }

        public string 할일()
        {
            return "기획";
        }
    }

    public class 관리자
    {
        static void Main()
        {
            연구원 A = new 개발자_연구원();
            A.setType("선임연구원");
            A.setSalary(100);
            A.set업무표준(new 개발자_업무표준());

            연구원 B = new 기획자_연구원();
            B.setType("수석연구원");
            B.setSalary(1000);
            B.set업무표준(new 기획자_업무표준());
        }
    }
}
