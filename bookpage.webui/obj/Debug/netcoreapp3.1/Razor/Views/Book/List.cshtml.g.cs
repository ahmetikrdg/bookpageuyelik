#pragma checksum "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bdcf14ad33127cb5f8d0a7e29e2ed7c91763c476"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Book_List), @"mvc.1.0.view", @"/Views/Book/List.cshtml")]
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
#line 1 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\_ViewImports.cshtml"
using bookpage.entity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\_ViewImports.cshtml"
using bookpage.webui.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\_ViewImports.cshtml"
using bookpage.webui.Controllers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\_ViewImports.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\_ViewImports.cshtml"
using bookpage.webui.Extensions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bdcf14ad33127cb5f8d0a7e29e2ed7c91763c476", @"/Views/Book/List.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b970b28584b0dc4f7d95e99f5bd0e68cd2ab8573", @"/Views/_ViewImports.cshtml")]
    public class Views_Book_List : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ProductListViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
  //buranın layoultla alakası yok
    var popularClass=Model.Products.Count>2?"popular":""; 
    var products=Model.Products;

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    \n");
            WriteLiteral("\n<div class=\"row\">\n    <div class=\"col-md-3\"> ");
            WriteLiteral("\n           ");
#nullable restore
#line 14 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
      Write(await Component.InvokeAsync("Categories"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </div>\n");
            WriteLiteral("\n    <div class=\"col-md-9\">\n       <div class=\"row\">\n              \n                    <div class=\"row\">                  \n");
#nullable restore
#line 22 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
                         foreach (var product in products)
                        {    

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"col-md-4\">\n                            ");
#nullable restore
#line 25 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
                       Write(await Html.PartialAsync("_product",product));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
            WriteLiteral("\n");
            WriteLiteral("                        </div>       \n");
#nullable restore
#line 28 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
                        }   

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    </div>
                    <div class=""row"">
                        <div class=""col"">
                            <nav aria-label=""Page navigation example"">
                                <ul class=""pagination"">
                                    <a class=""page-link""");
            BeginWriteAttribute("href", " href=\"", 1449, "\"", 1475, 2);
            WriteAttributeValue("", 1456, "/products?page=", 1456, 15, true);
#nullable restore
#line 34 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
WriteAttributeValue("", 1471, 1, 1471, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Ana Sayfa</a>\n");
#nullable restore
#line 35 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
                                     for (int i = 0; i < Model.pageInfo.TotalPages(); i++)
                                    {
                                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 37 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
                                         if(string.IsNullOrEmpty(Model.pageInfo.CurrentCategory))//kategori dolumu boşmu
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <li");
            BeginWriteAttribute("class", " class=\"", 1830, "\"", 1894, 2);
            WriteAttributeValue("", 1838, "page-item", 1838, 9, true);
#nullable restore
#line 39 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
WriteAttributeValue(" ", 1847, Model.pageInfo.CurrentPage==i+1?"active":"", 1848, 46, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
            WriteLiteral("\n                                                <a class=\"page-link\"");
            BeginWriteAttribute("href", " href=\"", 2052, "\"", 2080, 2);
            WriteAttributeValue("", 2059, "/products?page=", 2059, 15, true);
#nullable restore
#line 40 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
WriteAttributeValue("", 2074, i+1, 2074, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
            WriteLiteral("\n                                                ");
#nullable restore
#line 41 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
                                            Write(i+1);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
            WriteLiteral("\n                                                </a>\n                                           </li>\n");
#nullable restore
#line 44 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                             <li");
            BeginWriteAttribute("class", " class=\"", 2540, "\"", 2604, 2);
            WriteAttributeValue("", 2548, "page-item", 2548, 9, true);
#nullable restore
#line 47 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
WriteAttributeValue(" ", 2557, Model.pageInfo.CurrentPage==i+1?"active":"", 2558, 46, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n                                                <a class=\"page-link\"");
            BeginWriteAttribute("href", " href=\"", 2675, "\"", 2735, 4);
            WriteAttributeValue("", 2682, "/products/", 2682, 10, true);
#nullable restore
#line 48 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
WriteAttributeValue("", 2692, Model.pageInfo.CurrentCategory, 2692, 31, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2723, "?page=", 2723, 6, true);
#nullable restore
#line 48 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
WriteAttributeValue("", 2729, i+1, 2729, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n                                                ");
#nullable restore
#line 49 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
                                            Write(i+1);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                                                </a>\n                                           </li>\n");
#nullable restore
#line 52 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
                                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 52 "C:\Users\Ahmet\Desktop\bookpageg\bookpage.webui\Views\Book\List.cshtml"
                                         
                                        
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    \n                                </ul>\n                            </nav>\n                        </div>\n                    </div>\n        </div>      \n    </div>\n</div>\n\n        \n             \n\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ProductListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
