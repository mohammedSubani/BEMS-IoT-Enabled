#pragma checksum "D:\ASU-AGFE\CSE 593 Applied Project\Project 0\Central Server Application\BEMS-IoT v 1.0.0\WebApplication1\Views\Test\DashboardsTest.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a35c9dba14a1a8806e552e8bd956bd3ca3b36efa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Test_DashboardsTest), @"mvc.1.0.view", @"/Views/Test/DashboardsTest.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a35c9dba14a1a8806e552e8bd956bd3ca3b36efa", @"/Views/Test/DashboardsTest.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"729efaa87342638aecfe1a972ce9f9f8dff55b4c", @"/Views/_ViewImports.cshtml")]
    public class Views_Test_DashboardsTest : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<!--

Project: BEMS IoT Enabled System V 1.0.0
File Name: DashboardsTest.cshtml
Development Environment: Microsoft Visual Studio v16.8.4 Community Version
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997

File Description:
    The following page test calls to Cyclotron component, the test is sucessful if the tester
    is able to use the Cycltron editing tool through an iframe to Cyclotron Server.
-->
");
#nullable restore
#line 13 "D:\ASU-AGFE\CSE 593 Applied Project\Project 0\Central Server Application\BEMS-IoT v 1.0.0\WebApplication1\Views\Test\DashboardsTest.cshtml"
  
    ViewData["Title"] = "DashboardsTest";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>DashboardsTest</h1>

<!--Explaining the test for users-->
<p>
    The following test calls to Cyclotron component, the test is sucessful if the tester
    is able to use the Cycltron editing tool through an iframe to Cyclotron Server.

    If the user is not able to complete tasks through this page a flag of Cyclotron
    server down time is raised.
</p>

<!--iframe element pointing to Cyclotron main editing page-->
<iframe src=""http://bems-iot.ddnsgeek.com:8080""
        name=""targetframe""
        allowTransparency=""true""
        scrolling=""yes""
        frameborder=""0""
        height=""500""
        width=""100%"">
</iframe>");
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