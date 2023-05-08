
using System.Text;

namespace DoKevEngine
{
    public class Config {

        /* ini 유효성 확인 및 선언 */
        IniFile ini = new IniFile();
        IniFile lang = new IniFile();


        /* ConfigSet
         * 설정 등록 */
        public void ConfigSet() {
            RichSupport rich = new RichSupport();
            try {
                ini.Load($"{AppDomain.CurrentDomain.BaseDirectory}/config.ini");
                lang.Load($"{AppDomain.CurrentDomain.BaseDirectory}/{Cfg("folder", "language")}/{Cfg("builder", "language")}.ini");

                rich.Log(Text("info", "builder"), Cfg("builder", "version"));
            } catch (Exception e) {
                rich.Log("Fatal Error", "An error occurred during initialization.", "fatal");
                rich.Log("\nFatal Error", e.Message, "fatal");
                Console.ReadKey(); return;
            }
        }


        /* Cfg
         * config의 설정 값을 반환합니다. */
        public string Cfg(string key, string value) {
            var innerDict = ini[key];
            return innerDict[value].ToString();
        }


        /* Text
         * 값에 따라 언어 설정에 맞는 텍스트를 반환합니다. */
        public string Text(string key, string value) {
            var innerDict = lang[key];
            var text = innerDict[value].ToString();
            return new StringBuilder(text).Replace("\\n", "\n").ToString();
        }

    }   /* Config Class */

}       /* DoKevEngine namespace */
