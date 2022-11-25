#pragma checksum "D:\ASU-AGFE\CSE 593 Applied Project\Project 0\Central Server Application\BEMS-IoT v 1.0.0\WebApplication1\Views\Home\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7dcc9305750e738d718ee14033093111a805f0d3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_About), @"mvc.1.0.view", @"/Views/Home/About.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7dcc9305750e738d718ee14033093111a805f0d3", @"/Views/Home/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"729efaa87342638aecfe1a972ce9f9f8dff55b4c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_About : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<!--

Project: BEMS IoT Enabled System V 1.0.0
File Name: About.cshtml
Development Environment: Microsoft Visual Studio v16.8.4 Community Version
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997

File Description:
The following page gives general information about BEMS IoT Enabled System.

-->

");
#nullable restore
#line 14 "D:\ASU-AGFE\CSE 593 Applied Project\Project 0\Central Server Application\BEMS-IoT v 1.0.0\WebApplication1\Views\Home\About.cshtml"
  
    ViewData["Title"] = "About";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7dcc9305750e738d718ee14033093111a805f0d33836", async() => {
                WriteLiteral(@"
    <!--CSS styling for the page-->
    <style>
        body {
            background-image: url('https://images.unsplash.com/photo-1585314062340-f1a5a7c9328d');
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-size: cover;
        }

        footer {
            background-color: white;
        }
    </style>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

<div class=""text-left"" style=""color:white;"">
    <h1 class=""display-4"">About</h1>
</div>

<!-- INTRO Text Field -->
<p style=""text-align: left; color:white;"">
    According to the World Economic Forum website buildings are the
    source of a 40% of global energy consumption and they are responsible
    as well for a 33% of the greenhouse gas emissions. [Tricoire, 2021] Therefore,
    addressing sustainable solutions for the buildings energy consumption and
    greenhouse gas emission is a crucial step in tackling the challenges of
    global climate change.
</p>

<!-- WHAT Text Field -->
<h3 style=""text-align: left;color:white;""><strong>What is BEMS IoT Based Solution?</strong></h3>

<p style=""color:white;"">
    Building Energy Management System defined in simple terms by UN Climate Technology Center &amp;
    Network &ldquo;is a sophisticated method to monitor and control the building's energy needs&rdquo;
    [CTCN, n.d.] this project will be building on the concept of BEMS and the ");
            WriteLiteral(@"paradigm of Internet of
    Things to build a more flexible and loosely coupled BEMS system.
</p>

<!-- WHY Text Field -->
<h3 style=""text-align: left;color:white;""><strong>Why BEMS IoT Based Solution?</strong></h3>
<p style=""color:white;"">
    Current BEMS systems are focused on the proof-of-concept implementations.
    The current implementations require high levels of specialty and they
    are subject to high costs in buildings and maintenance of the system.
    In order for BEMS to meet its purpose it should be reachable for the general
    public with lower costs and with minimum technological implementation challenges.
    BEMS systems are already implemented in many buildings so far but they are limited in
    their application to buildings and are limited in terms of extendibility,
    A more flexible systems based on more advanced solutions are necessary and
    thus the introduction of IoT technology.
</p>

<!-- HOW Text Field -->
<h3 style=""text-align: left;color:white;""><strong");
            WriteLiteral(@">How BEMS IoT Based Solution will be implemented?</strong></h3>
<p style=""color:white;"">
    In order for the BEMS system to be effective it should build upon
    technologies that are within reach of general public therefore using
    open-source technologies is a first step, the second step would be to build
    a solution that is easy to implement and replicate with costs that are
    feasible for mass replication of these systems.

    From an intellectual point of view this project is built upon the
    service-oriented computing paradigms that creates a system-of-systems
    made of loosely-coupled components making the systems easier to maintain
    and develop on larger scales.

    A foreseen impact of this project is to push the technology of BEMS
    a step further towards wide scale adoption of BEMS system in buildings
    assessing the ongoing efforts of addressing the global
    climate change challenges.
</p>

<!-- References Text Field -->
<blockquote>
    <p style=""text-a");
            WriteLiteral(@"lign: left;color:white;"">[1] Jean-Pascal Tricoire. 2021. &ldquo;Why buildings are the foundation of an energy-efficient future&rdquo;. 22 Feb 2021. Available online at: <a href=""https://www.weforum.org/agenda/2021/02/why-the-buildings-of-the-future-are-key-to-an-efficient-energy-ecosystem/"">https://www.weforum.org/agenda/2021/02/why-the-buildings-of-the-future-are-key-to-an-efficient-energy-ecosystem/</a></p>
    <p style=""text-align: left;color:white;"">[2] United Nations Climate Technology Center and Network. N.d. &ldquo;Building Energy Management Systems (BEMS)&rdquo;. N.d. Available online at: <a href=""https://www.ctc-n.org/technologies/building-energy-management-systems-bems"">https://www.ctc-n.org/technologies/building-energy-management-systems-bems</a></p>
</blockquote>
<p style=""text-align: center;color:white;"">&nbsp;</p>
<p style=""text-align: center;color:white;"">&nbsp;</p>

");
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
