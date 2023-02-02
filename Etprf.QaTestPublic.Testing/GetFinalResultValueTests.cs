namespace Etprf.QaTestPublic.Testing.Etprf.QaTestPublic.Testing;

public class Tests : TestBase
{
    [Test]
    public static async Task CheckGetFinalResultValueFromEtprfQaTestPublicIndexPage()
    {
        (string importantDataValue, string specialDataValue) = new IndexPage(Driver, Wait).Open().GetAndPostDataValue();
        var secretHeaderValue =
            await HTTPClient.GetSecretHeaderValue(Host1, $"?important={importantDataValue}", importantDataValue);
        if (secretHeaderValue != null)
        {
            string finalResultValue = await  HTTPClient.GetFinalResult(Host2, secretHeaderValue, specialDataValue);
            Assert.That(finalResultValue, Is.EqualTo("220"),
                "Полученное значение Final Result не соответствует ожидаемому значению 220");
            Console.WriteLine(
                $"dataValue= {importantDataValue}, specialDataValue = {specialDataValue}, secretHeaderValue = " +
                $"{secretHeaderValue}. \n finalResultValue= {finalResultValue}");
        }
    }

    //При направлении GET-запроса по адресу http://qatest.etprf.ru/api/stage1?important=3 возвращается ответ
    //со значением заголовка Secret = 2022.
    //Также если направлять GET-запрос по адресу http://qatest.etprf.ru/api/stage1 с заголовком important = 3
    //возвращается ответ со значением заголовка Secret каждый раз разный, однако в при дальнейшем использовании
    //каждого из значений заголовка Secret значение Final Result возвращается также220.
    //Чтобы подтвердить данное утверждение был написан следующий тест.
    
    [Test]
    [TestCase(10)]
    public static async Task CheckGetFinalResultValueWithHeaderImportant(int a)
    {
        (string importantDataValue, string specialDataValue) = new IndexPage(Driver, Wait).Open().GetAndPostDataValue();
        for (int i = 0; i <= a; i++)
        {
            string customHeader = "important";
            var secretHeaderValue =
                await HTTPClient.GetSecretHeaderValueWithHeaderImportant(Host1, customHeader, importantDataValue);
            if (secretHeaderValue == null) continue;
            string finalResultValue = await HTTPClient.GetFinalResult(Host2, secretHeaderValue, specialDataValue);
            CustomAssertAreEqual("220", finalResultValue,
                $"secretHeaderValue = {secretHeaderValue} finalResultValue = {finalResultValue}");
            Console.WriteLine(
                $"При значении secretHeaderValue = {secretHeaderValue} результат finalResultValue = {finalResultValue}"); 
        }
    }
}