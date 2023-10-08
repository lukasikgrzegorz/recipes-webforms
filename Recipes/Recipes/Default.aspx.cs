using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Search_Recipes(object sender, EventArgs e)
    {
        string query = recipe_query.Text;

        WebRequest request = WebRequest.Create("https://www.themealdb.com/api/json/v1/1/search.php?s=" + query);

        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        string apiResponse = reader.ReadToEnd();

                        MealApiResponse mealApiResponse = JsonConvert.DeserializeObject<MealApiResponse>(apiResponse);

                        List<Recipe> recipes = mealApiResponse.Meals;

                        result.Text = "";

                        foreach (Recipe recipe in recipes)
                        {
                            // Tworzenie linka do przepisu
                            HyperLink recipeHyperlink = new HyperLink();
                            recipeHyperlink.NavigateUrl = "RecipePage.aspx?recipeId=" + recipe.IdMeal;
                            recipeHyperlink.Text = recipe.StrMeal;

                            // Dodawanie linka do strony
                            result.Controls.Add(recipeHyperlink);

                            // Dodawanie separatora
                            result.Controls.Add(new LiteralControl("<br/>"));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            result.Text = "Wystąpił błąd: " + ex.Message;
        }
    }

}

public class Recipe
{
    public string IdMeal { get; set; }
    public string StrMeal { get; set; }
}

public class MealApiResponse
{
    public List<Recipe> Meals { get; set; }
}
