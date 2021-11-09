using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DriverApp.Pages
{
    public partial class Navigation
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initialize", null); 
            }
        }
    }
    
}