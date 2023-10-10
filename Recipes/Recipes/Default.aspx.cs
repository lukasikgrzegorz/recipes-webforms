using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;

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

                        result.Controls.Clear(); // Wyczyść istniejące kontrolki przed dodaniem nowych

                        if (recipes != null && recipes.Count > 0)
                        {
                            foreach (Recipe recipe in recipes)
                            {
                                // Tworzenie linku do przepisu
                                HyperLink recipeHyperlink = new HyperLink();
                                recipeHyperlink.NavigateUrl = "RecipePage.aspx?recipeId=" + recipe.IdMeal;

                                // Tworzenie kontenera <div> dla obrazka i labelki
                                HtmlGenericControl divContainer = new HtmlGenericControl("div");
                                divContainer.Attributes["class"] = "recipe-container";

                                // Dodawanie obrazka przepisu
                                Image recipeImage = new Image();
                                recipeImage.ImageUrl = recipe.StrMealThumb ?? "placeholder.jpg";
                                recipeImage.AlternateText = recipe.StrMeal;
                                recipeImage.CssClass = "result-img";

                                // Dodawanie labelki
                                Label titleLabel = new Label();
                                titleLabel.Text = recipe.StrMeal;

                                // Dodaj obrazek i labelkę do kontenera
                                divContainer.Controls.Add(recipeImage);
                                divContainer.Controls.Add(titleLabel);

                                // Dodaj kontener do linku
                                recipeHyperlink.Controls.Add(divContainer);
                                recipeHyperlink.CssClass = "result-item";

                                result.Controls.Add(recipeHyperlink);
                            }
                        }
                        else
                        {
                            result.Text = "No results for: " + query;
                        }
                    }
                }
            }
        }
        catch (WebException webEx)
        {
            result.Text = "API Error: " + webEx.Message;
        }
        catch (Exception ex)
        {
            result.Text = "Error: " + ex.Message;
        }
    }
}

public class Recipe
{
    public string IdMeal { get; set; }
    public string StrMeal { get; set; }
    public string StrMealThumb { get; set; }
}

public class MealApiResponse
{
    public List<Recipe> Meals { get; set; }
}
