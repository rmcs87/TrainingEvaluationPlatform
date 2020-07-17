using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace TEP.Application.Assets
{
    public class ListCategoryModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var value = valueProviderResult.FirstValue;

            var model = JsonSerializer.Deserialize<List<int>>(value);
            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }
    }
}
