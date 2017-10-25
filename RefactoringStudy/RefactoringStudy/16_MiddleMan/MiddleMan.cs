/// <summary>
/// 과잉 중개 메서드 (Middle Man)
/// 
/// 어떤 클래스의 인터페이스가 그 안의 절반도 넘는 메서드가 기능을 다른 클래스에 위임하고 있다
/// 
/// 또는 메시지 체인을 많이 제거하면 발생할 수도 있다.
/// 
/// 새 기능이 대리자에 추가될때마다 위임하는 메소드를 생성해야 한다. 변경사항이 많아지면 귀찮은 일이 된다.
/// 
/// 내부 클래스 객체에서 대리자 클래스에 액세스 하기 위한 getter를 만든다.
/// 대리자 클래스의 메소드를 직접 호출하여 내부클래스의 메소드를 호출하는 것을 대신한다.
/// 
/// 메소드에 별 기능이 없다면 메소드의 내용을 호출 객체에 직접 삽입하여 메소드들의 내용을 호출객체에 직접 삽입
/// 부수적인 기능이 있다면 위임을 상속으로 전환하여 대리자메소드를 실제 객체의 하위 클래스로 전환
/// 
/// </summary>

namespace RefactoringStudy._16_MiddleMan
{
    public class Character
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }

        public Character() { }
        public Character(string name)
        {
            Name = name;
        }
    }

    public class CharacterBuilder
    {
        private Character _character;

        public CharacterBuilder Create(string name)
        {
            _character = new Character(name);
            return this;
        }

        public CharacterBuilder HP(int hp)
        {
            _character.Hp = hp;
            return this;

        }

        public CharacterBuilder MP(int mp)
        {
            _character.Mp = mp;
            return this;
        }


        public Character Value()
        {
            return _character;
        }
    }

    class program
    {
        void test()
        {
            CharacterBuilder builder = new CharacterBuilder();
            builder.Create("name1").HP(100).MP(100);

            Character myCharacter = builder.Value();
        }
    }

    //////////////////////////////////////

    public interface ICharacterBuilderStats
    {
        ICharacterBuilderStats HP(int hp);
        ICharacterBuilderStats MP(int mp);
    }

    public class CharacterBuilder2 : ICharacterBuilderStats
    {
        private Character _character;
        public CharacterBuilder2 Create(string name)
        {
            _character = new Character(name);
            return this;
        }
        public ICharacterBuilderStats HP(int hp)
        {
            _character.Hp = hp;
            return this;
        }

        public ICharacterBuilderStats MP(int mp)
        {
            _character.Mp = mp;
            return this;
        }
        public Character Value()
        {
            return _character;
        }
    }

    class program2
    {
        void test()
        {
            CharacterBuilder2 builder2 = new CharacterBuilder2();
            builder2.Create("name1").HP(100).MP(100);

            Character myCharacter = builder2.Value();
        }
    }
}
