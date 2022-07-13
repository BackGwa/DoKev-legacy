namespace dkpl
{
    class Program{

        static void Main(string[] args){

            var engine = IronPython.Hosting.Python.CreateEngine();
            var scope = engine.CreateScope();
            string fp = System.Environment.CurrentDirectory + "\\export\\convert.py";
            string Filepath =  (@fp);
            int ec = 0;

            try
            {
                var source = engine.CreateScriptSourceFromFile(Filepath);
                source.Execute(scope);
            }

            catch(Exception ex){
                Console.WriteLine(ex.ToString());
                ec = ex.HResult;
            }

            Console.WriteLine("\n========================================\n프로세스가 종료되었습니다." + "[ 코드 :  " + ec + " ]" + "\n========================================");
            Console.ReadLine();

        }
    }
}