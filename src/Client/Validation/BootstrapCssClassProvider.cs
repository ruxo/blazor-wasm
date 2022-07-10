using Microsoft.AspNetCore.Components.Forms;

namespace Wasm.Client.Validation;

public sealed class BootstrapCssClassProvider : FieldCssClassProvider
{
    public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier) {
        var isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();
        var isModified = editContext.IsModified(fieldIdentifier);
        return isValid ? isModified ? "is-valid" : "" : "is-invalid";
    }
}