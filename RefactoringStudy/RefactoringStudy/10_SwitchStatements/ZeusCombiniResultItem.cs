// 10. Switch Statements
// switch문이 프로그램에 곳곳에 존재한다면 코드를 추가할때 전부 찾아서 수정해야 한다.
// => 재정의를 이용해서 이 문제를 해결한다.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringStudy._10_SwitchStatements
{
    public class ZeusCombiniResultItem_Old
    {
        private string Status = "";
        private string Pay_cvs = "";

        public string GetKonbiniImgSrc()
        {
            switch (Pay_cvs)
            {
                case "D001":
                    return "logo_cvsnr_mart01.gif";

                case "D002":
                    return "logo_cvsnr_mart02.gif";

                case "D030":
                    return "logo_cvsnr_mart03.gif";

                case "D015":
                    return "logo_cvsnr_mart04.gif";

                case "D040":
                    return "logo_cvsnr_mart05.gif";

                case "D045":
                    return "logo_cvsnr_mart06.gif";

                case "D060":
                    return "logo_cvsnr_mart07.gif";

                case "D050":
                    return "logo_cvsnr_mart08.gif";

                default:
                    return string.Empty;
            }
        }

        public string GetKonbiniDescription()
        {
            switch (Pay_cvs)
            {
                case "D001":
                    return "セブンイレブン";

                case "D002":
                    return "ローソン";

                case "D030":
                    return "ファミリーマート";

                case "D015":
                    return "セイコーマート";

                case "D040":
                    return "サークルK";

                case "D045":
                    return "サンクス";

                case "D060":
                    return "デイリーヤマザキ";

                case "D050":
                    return "ミニストップ";

                default:
                    return string.Empty;
            }
        }
        //...
    }
    
    // 1. Dic을 이용해보기
    class ZeusCombiniResultItem_DIC
    {
        // 
        public Dictionary<string, string> konbiniImgSrc = new Dictionary<string, string>()
        {
            { "D001" , "logo_cvsnr_mart01.gif" },
            { "D002" , "logo_cvsnr_mart02.gif" },
            { "D015" , "logo_cvsnr_mart03.gif" },
            { "D030" , "logo_cvsnr_mart04.gif" },
            { "D040" , "logo_cvsnr_mart05.gif" },
            { "D045" , "logo_cvsnr_mart06.gif" },
            { "D050" , "logo_cvsnr_mart07.gif" },
            { "D060" , "logo_cvsnr_mart08.gif" }
        };

        public string GetKonbiniImgSrc(string Pay_cvs)
        {
            if (konbiniImgSrc.ContainsKey(Pay_cvs))
            {
                return konbiniImgSrc[Pay_cvs];
            }

            return string.Empty;
        }
        //...
    }

    // 2. 다형성을 이용해보기
    public interface IKonbiniImgSrc
    {
        string getKonbiniImgSrc();
    }

    public class Konbini_D001 : IKonbiniImgSrc
    {
        public string getKonbiniImgSrc()
        {
            return "logo_cvsnr_mart01.gif";
        }
    }

    public class Konbini_D002 : IKonbiniImgSrc
    {
        public string getKonbiniImgSrc()
        {
            return "logo_cvsnr_mart02.gif";
        }
    }

    public class Konbini_D030 : IKonbiniImgSrc
    {
        public string getKonbiniImgSrc()
        {
            return "logo_cvsnr_mart03.gif";
        }
    }

    //...
}
