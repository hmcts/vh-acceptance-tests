namespace AcceptanceTests.Common.PageObject.Pages.Common
{
    public static class CommonLocator
    {
        public static string DropDownList(string element) => $"//select[@id='{element}']/option[position()>1]";
    }
}
