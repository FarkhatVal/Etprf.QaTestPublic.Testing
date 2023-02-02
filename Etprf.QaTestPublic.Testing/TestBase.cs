using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Etprf.QaTestPublic.Testing.Etprf.QaTestPublic.Testing;

public class TestBase
{
    private protected const string Host1 = "http://qatest.etprf.ru/api/stage1";
    private protected const string Host2 = "http://qatest.etprf.ru/api/stage2";
    private protected static WebDriver Driver;
    private protected static WebDriverWait Wait;
    private static ChromeOptions _chromeOptions = new ChromeOptions();
    protected static string PathToIndexHtml =
        Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString())
            .ToString()) + "/Etprf.QaTestPublic/index.html";
    private static List<string>? ErrorMessage;

    [OneTimeSetUp]
    public void Setup()
    {
        _chromeOptions = new ChromeOptions();
        // Отключить "Браузером управляет автоматизированное ПО"
        _chromeOptions.AddAdditionalOption("useAutomationExtension", false);
        _chromeOptions.AddExcludedArgument("enable-automation");
        ErrorMessage = new List<string>(); 
    }
    [SetUp]
    public void BeforeTest()
    {
        Driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), _chromeOptions);
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
    }
    [TearDown]
    public void TearDown()
    {
        Driver.Close();
        Driver.Quit();
        Assert.That(ErrorMessage, Is.Empty, $"Значение finalResultValue не соответсвует ожидаемому 220 при значении: \n{ string.Join("\n", ErrorMessage)}");
    }
    
    protected static void CustomAssertAreEqual(string expected, string actual, string message)
    {
        if (expected != actual) {ErrorMessage.Add(message);}
    }
}