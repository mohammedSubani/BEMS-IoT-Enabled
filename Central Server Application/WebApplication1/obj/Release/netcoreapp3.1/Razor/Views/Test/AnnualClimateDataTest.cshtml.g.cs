#pragma checksum "D:\ASU-AGFE\CSE 593 Applied Project\Project 0\Central Server Application\BEMS-IoT v 1.0.0\WebApplication1\Views\Test\AnnualClimateDataTest.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2fab3b91e38a503297d3a11cf73c05333c6af29f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Test_AnnualClimateDataTest), @"mvc.1.0.view", @"/Views/Test/AnnualClimateDataTest.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\ASU-AGFE\CSE 593 Applied Project\Project 0\Central Server Application\BEMS-IoT v 1.0.0\WebApplication1\Views\_ViewImports.cshtml"
using WebApplication1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\ASU-AGFE\CSE 593 Applied Project\Project 0\Central Server Application\BEMS-IoT v 1.0.0\WebApplication1\Views\_ViewImports.cshtml"
using WebApplication1.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2fab3b91e38a503297d3a11cf73c05333c6af29f", @"/Views/Test/AnnualClimateDataTest.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"729efaa87342638aecfe1a972ce9f9f8dff55b4c", @"/Views/_ViewImports.cshtml")]
    public class Views_Test_AnnualClimateDataTest : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<!--

Project: BEMS IoT Enabled System V 1.0.0
File Name: AnnualClimateDataTest.cshtml
Development Environment: Microsoft Visual Studio v16.8.4 Community Version
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997

File Description:
The following page contains the test checking whether the AnnualClimateData retrieves monthly
    point data for the locationwhere the BEMS is implemented that is namely latitude: 31.9702593 
    and longitude:35.9568498 , The test happens on page load.
-->


");
#nullable restore
#line 16 "D:\ASU-AGFE\CSE 593 Applied Project\Project 0\Central Server Application\BEMS-IoT v 1.0.0\WebApplication1\Views\Test\AnnualClimateDataTest.cshtml"
  
    ViewData["Title"] = "AnnualClimateDataTest";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<!--Explaining the test for users-->
<h1>AnnualClimateDataTest</h1>

<p>
    This test checks whether the AnnualClimateData retrieves monthly point data for the location
    where the BEMS is implemented that is namely latitude: 31.9702593 and longitude:35.9568498.

    In case of sucessful retrieval of data the a raw string of the data is recieved with values for
    aggregate relative humiditiy. The data retrieved are post-processed in Cycltron for presentation.

    In case of unsucessful call a message notifying the test failuare is shown.
</p>

<!--Test results, the weather is called from controller on page load.-->
<h3>Test Result ?????????????????????????????????????????????????????????????????????????????????????????????????????????</h3>
<p><strong><span style=""color: #ff0000;"">");
#nullable restore
#line 35 "D:\ASU-AGFE\CSE 593 Applied Project\Project 0\Central Server Application\BEMS-IoT v 1.0.0\WebApplication1\Views\Test\AnnualClimateDataTest.cshtml"
                                    Write(ViewBag.AnnaulDataMessage);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></strong></p>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
