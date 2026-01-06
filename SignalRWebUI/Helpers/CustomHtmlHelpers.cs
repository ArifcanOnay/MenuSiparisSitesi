using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SignalRWebUI.Helpers
{
    public static class CustomHtmlHelpers
    {
        /// <summary>
        /// Özel oluşturulmuş bir bilgi kartı HTML Helper'ı
        /// </summary>
        public static IHtmlContent InfoCard(this IHtmlHelper htmlHelper, string title, string content, string iconClass = "fa fa-info-circle")
        {
            var html = $@"
                <div class='custom-info-card' style='background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); 
                     border-radius: 15px; padding: 25px; margin: 20px 0; color: white; box-shadow: 0 10px 30px rgba(0,0,0,0.2);'>
                    <div style='display: flex; align-items: center; gap: 15px;'>
                        <i class='{iconClass}' style='font-size: 40px; color: #ffd700;'></i>
                        <div>
                            <h3 style='margin: 0; font-size: 24px; font-weight: bold;'>{title}</h3>
                            <p style='margin: 10px 0 0 0; font-size: 16px; opacity: 0.9;'>{content}</p>
                        </div>
                    </div>
                </div>";
            
            return new HtmlString(html);
        }

        /// <summary>
        /// Özel oluşturulmuş bir istatistik kartı HTML Helper'ı
        /// </summary>
        public static IHtmlContent StatCard(this IHtmlHelper htmlHelper, string title, string value, string color = "#4CAF50")
        {
            var html = $@"
                <div class='custom-stat-card' style='background: {color}; border-radius: 10px; 
                     padding: 20px; text-align: center; color: white; box-shadow: 0 5px 15px rgba(0,0,0,0.1);'>
                    <h2 style='margin: 0; font-size: 36px; font-weight: bold;'>{value}</h2>
                    <p style='margin: 5px 0 0 0; font-size: 14px; opacity: 0.9; text-transform: uppercase;'>{title}</p>
                </div>";
            
            return new HtmlString(html);
        }
    }
}
