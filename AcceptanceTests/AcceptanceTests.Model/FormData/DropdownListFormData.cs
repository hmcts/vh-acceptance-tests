using System;
namespace AcceptanceTests.Model.FormData
{
    public class DropdownListFormData : IFormData
    {
        public string SelectedOption { get; set; }

        public IFormData GenerateFake()
        {
            throw new NotImplementedException();
        }
    }
}
