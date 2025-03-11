using System;
using System.Diagnostics;

namespace sauceLabs_PageObject
{
    public class FeatureRunner
    {
        public static void Main()
        {
            string[] testCategories =
            {
                "LoginPage", "ViewItem", "AddToCart", "Checkout",
                "Checkout2", "CheckoutOverview", "ConfirmationPage"
            };

            string filter = string.Join("|", testCategories);
            string command = $"dotnet test --filter \"TestCategory={filter}\""; // Run all in one command

            Console.WriteLine("Running all test categories in a single command...");
            RunCommand(command);

            Console.WriteLine("All tests completed.");
        }

        private static void RunCommand(string command)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = false
            };

            using (Process process = Process.Start(processInfo))
            {
                if (process == null)
                {
                    throw new Exception("Failed to start test process.");
                }

                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                Console.WriteLine(result);
            }
        }
    }
}




//using System;
//using System.Diagnostics;

//namespace sauceLabs_PageObject
//{
//    public class FeatureRunner
//    {
//        public static void RunOrderedTests()
//        {
//            string[] testCategories =
//            {
//                "LoginPage", "ViewItem", "AddToCart", "Checkout",
//                "Checkout2", "CheckoutOverview", "ConfirmationPage"
//            };

//            foreach (string category in testCategories)
//            {
//                Console.WriteLine($"Running tests for category: {category}");
//                RunCommand($"dotnet test --filter \"TestCategory={category}\"");
//            }

//            Console.WriteLine("All selected tests completed.");
//        }

//        private static void RunCommand(string command)
//        {
//            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
//            {
//                RedirectStandardOutput = true,
//                UseShellExecute = false,
//                CreateNoWindow = true
//            };

//            using (Process process = Process.Start(processInfo))
//            {
//                if (process == null)
//                {
//                    throw new Exception("Failed to start test process.");
//                }

//                string result = process.StandardOutput.ReadToEnd();
//                process.WaitForExit();

//                Console.WriteLine(result);
//            }
//        }
//    }
//}
