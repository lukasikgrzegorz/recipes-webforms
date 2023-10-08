using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public partial class RecipePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["recipeId"] != null)
            {
                string recipeId = Request.QueryString["recipeId"];
                LoadRecipeDetails(recipeId);
            }
        }
    }

    private void LoadRecipeDetails(string recipeId)
    {
        try
        {
            // Tworzenie zapytania do API na podstawie recipeId
            WebRequest request = WebRequest.Create("https://www.themealdb.com/api/json/v1/1/lookup.php?i=" + recipeId);

            using (WebResponse response = request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        string apiResponse = reader.ReadToEnd();

                        // Deserializacja odpowiedzi API do obiektu RecipeDetailsApiResponse
                        RecipeDetailsApiResponse recipeDetailsApiResponse = JsonConvert.DeserializeObject<RecipeDetailsApiResponse>(apiResponse);

                        // Pobranie pierwszego przepisu z odpowiedzi (zakładając, że recipeId jest unikalne)
                        RecipeDetails recipeDetails = recipeDetailsApiResponse.Meals[0];

                        // Wyświetlenie pełnego przepisu w kontrolce Label
                        recipeDetailsLabel.Text = "Name: " + recipeDetails.StrMeal + "<br/>";
                        recipeDetailsLabel.Text += "Category: " + recipeDetails.StrCategory + "<br/>";
                        recipeDetailsLabel.Text += "Area: " + recipeDetails.StrArea + "<br/>";
                        recipeDetailsLabel.Text += "Instructions: " + recipeDetails.StrInstructions + "<br/>";
                        recipeDetailsLabel.Text += "Tags: " + recipeDetails.StrTags + "<br/>";
                        recipeDetailsLabel.Text += "YouTube Video: <a href=\"" + recipeDetails.StrYoutube + "\" target=\"_blank\">" + recipeDetails.StrYoutube + "</a><br/>";
                        recipeDetailsLabel.Text += "<img src=\"" + recipeDetails.StrMealThumb + "\" alt=\"" + recipeDetails.StrMeal + "\" /><br/>";

                        // Sprawdź, czy Ingredients nie jest null
                        if (recipeDetails.Ingredients != null && recipeDetails.Ingredients.Count > 0)
                        {
                            // Wyświetl składniki
                            recipeDetailsLabel.Text += "<h2>Ingredients:</h2><ul>";
                            foreach (Ingredient ingredient in recipeDetails.Ingredients)
                            {
                                recipeDetailsLabel.Text += "<li>" + ingredient.Name + " - " + ingredient.Measure + "</li>";
                            }
                            recipeDetailsLabel.Text += "</ul>";
                        }
                        else
                        {
                            // Ingredients jest null lub puste, obsłuż tę sytuację
                            recipeDetailsLabel.Text += "<p>No ingredients information available.</p>";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            recipeDetailsLabel.Text = "Wystąpił błąd: " + ex.Message;
        }
    }
}

public class RecipeDetailsApiResponse
{
    public List<RecipeDetails> Meals { get; set; }
}

public class RecipeDetails
{
    public string IdMeal { get; set; }
    public string StrMeal { get; set; }
    public string StrCategory { get; set; }
    public string StrArea { get; set; }
    public string StrInstructions { get; set; }
    public string StrMealThumb { get; set; }
    public string StrTags { get; set; }
    public string StrYoutube { get; set; }

    // Dodaj właściwość do przechowywania składników
    public List<Ingredient> Ingredients { get; set; }
}

public class Ingredient
{
    public string Name { get; set; }
    public string Measure { get; set; }
}
