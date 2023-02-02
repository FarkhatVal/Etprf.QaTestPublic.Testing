using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Etprf.QaTestPublic.Testing.Etprf.QaTestPublic.Testing;

public class IndexPage : TestBase
{
    private static WebDriver _driver;
    private static WebDriverWait _wait;
    private readonly string _url = PathToIndexHtml;
    
    public IndexPage(WebDriver driver, WebDriverWait wait)
    {
        driver.Url = _url;
        _driver = driver;
        _wait = wait;
    }
    
    public IndexPage Open()
    {
        _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl(_url);
        return this;
    }

    public  (string dataValue, string specialDataValue) GetAndPostDataValue()
    {
        string importantDataValue = _driver.FindElement(By.XPath("//div[@id='important']")).GetAttribute("data-value");
        _driver.FindElement(By.XPath("//input[@id='importantInput']")).SendKeys(importantDataValue);
        _driver.FindElement(By.XPath("//button[@id='submitBtn']")).Click();
        Thread.Sleep(500);
        string specialDataValue = _wait.Until(e => e.FindElement(By.XPath("//div[@id='special']"))).GetAttribute("data-value");
        return (importantDataValue, specialDataValue);
    }
}