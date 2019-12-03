using AcceptanceTests.Common.Model.FormData;

namespace AcceptanceTests.Common.PageObject.Components.Forms
{
    public interface IFormComponent : IComponent
    {
        void FillFormDetails(IFormData formDataObject);
    }
}
