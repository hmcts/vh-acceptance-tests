using AcceptanceTests.Model.FormData;

namespace AcceptanceTests.PageObject.Components.Forms
{
    public interface IFormComponent : IComponent
    {
        void FillFormDetails(IFormData formDataObject);
    }
}
