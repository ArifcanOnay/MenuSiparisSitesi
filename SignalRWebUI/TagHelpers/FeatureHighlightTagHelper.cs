using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SignalRWebUI.TagHelpers
{
    /// <summary>
    /// Özel oluşturulmuş bir Öne Çıkan Özellik Tag Helper'ı
    /// Kullanımı: <feature-highlight title="Başlık" description="Açıklama" icon="fa-star"></feature-highlight>
    /// </summary>
    [HtmlTargetElement("feature-highlight")]
    public class FeatureHighlightTagHelper : TagHelper
    {
        public string Title { get; set; } = "Özellik";
        public string Description { get; set; } = "Açıklama";
        public string Icon { get; set; } = "fa-star";
        public string Color { get; set; } = "#ff6b6b";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "custom-feature-highlight");
            output.Attributes.SetAttribute("style", 
                $"border-left: 5px solid {Color}; background: #f8f9fa; padding: 20px; " +
                "margin: 15px 0; border-radius: 8px; transition: all 0.3s ease; cursor: pointer;");
            
            output.Content.SetHtmlContent($@"
                <div style='display: flex; align-items: center; gap: 20px;'>
                    <div style='background: {Color}; width: 60px; height: 60px; border-radius: 50%; 
                         display: flex; align-items: center; justify-content: center; color: white;
                         box-shadow: 0 4px 10px rgba(0,0,0,0.2);'>
                        <i class='fa {Icon}' style='font-size: 24px;'></i>
                    </div>
                    <div style='flex: 1;'>
                        <h4 style='margin: 0; color: #333; font-size: 20px; font-weight: bold;'>{Title}</h4>
                        <p style='margin: 8px 0 0 0; color: #666; font-size: 14px;'>{Description}</p>
                    </div>
                </div>
            ");

            output.PostElement.SetHtmlContent(@"
                <style>
                    .custom-feature-highlight:hover {
                        transform: translateX(10px);
                        box-shadow: 0 5px 20px rgba(0,0,0,0.1);
                    }
                </style>
            ");
        }
    }
}
