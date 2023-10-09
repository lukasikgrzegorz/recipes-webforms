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
            else
            {
                recipeDetailsLabel.Text = "No recipe identifier provided.";
            }
        }
    }

    private void LoadRecipeDetails(string recipeId)
    {
        try
        {
            // Creating a request to the API based on recipeId
            WebRequest request = WebRequest.Create("https://www.themealdb.com/api/json/v1/1/lookup.php?i=" + recipeId);

            using (WebResponse response = request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        string apiResponse = reader.ReadToEnd();

                        // Deserializing API response to RecipeDetailsApiResponse object
                        RecipeDetailsApiResponse recipeDetailsApiResponse = JsonConvert.DeserializeObject<RecipeDetailsApiResponse>(apiResponse);

                        if (recipeDetailsApiResponse.Meals != null && recipeDetailsApiResponse.Meals.Count > 0)
                        {
                            RecipeDetails recipeDetails = recipeDetailsApiResponse.Meals[0];

                            // Displaying the full recipe in a Label control
                            recipeDetailsLabel.Text = "Name: " + (recipeDetails.StrMeal ?? "No name") + "<br/>";
                            recipeDetailsLabel.Text += "Category: " + (recipeDetails.StrCategory ?? "No category") + "<br/>";
                            recipeDetailsLabel.Text += "Area: " + (recipeDetails.StrArea ?? "No area") + "<br/>";
                            recipeDetailsLabel.Text += "Instructions: " + (recipeDetails.StrInstructions ?? "No instructions") + "<br/>";
                            recipeDetailsLabel.Text += "Tags: " + (recipeDetails.StrTags ?? "No tags") + "<br/>";
                            recipeDetailsLabel.Text += "YouTube Video: <a href=\"" + (recipeDetails.StrYoutube ?? "#") + "\" target=\"_blank\">" + (recipeDetails.StrYoutube ?? "No YouTube video") + "</a><br/>";
                            recipeDetailsLabel.Text += "<img src=\"" + (recipeDetails.StrMealThumb ?? "") + "\" alt=\"" + (recipeDetails.StrMeal ?? "") + "\" /><br/>";

                            // Adding ingredients to a list
                            recipeDetailsLabel.Text += "Ingredients:<ul>";
                            for (int i = 1; i <= 20; i++)
                            {
                                string ingredientProperty = "StrIngredient" + i;
                                string ingredientValue = recipeDetails.GetType().GetProperty(ingredientProperty)?.GetValue(recipeDetails) as string;

                                string measureProperty = "StrMeasure" + i;
                                string measureValue = recipeDetails.GetType().GetProperty(measureProperty)?.GetValue(recipeDetails) as string;

                                if (!string.IsNullOrEmpty(ingredientValue) && !string.IsNullOrEmpty(measureValue))
                                {
                                    recipeDetailsLabel.Text += "<li>" + ingredientValue + ":  " + measureValue + "</li>";
                                }
                            }
                            recipeDetailsLabel.Text += "</ul>";
                        }
                        else
                        {
                            recipeDetailsLabel.Text = "No results for recipe with id: " + recipeId;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            recipeDetailsLabel.Text = "An error occurred: " + ex.Message;
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
    public string StrIngredient1 { get; set; }
    public string StrIngredient2 { get; set; }
    public string StrIngredient3 { get; set; }
    public string StrIngredient4 { get; set; }
    public string StrIngredient5 { get; set; }
    public string StrIngredient6 { get; set; }
    public string StrIngredient7 { get; set; }
    public string StrIngredient8 { get; set; }
    public string StrIngredient9 { get; set; }
    public string StrIngredient10 { get; set; }
    public string StrIngredient11 { get; set; }
    public string StrIngredient12 { get; set; }
    public string StrIngredient13 { get; set; }
    public string StrIngredient14 { get; set; }
    public string StrIngredient15 { get; set; }
    public string StrIngredient16 { get; set; }
    public string StrIngredient17 { get; set; }
    public string StrIngredient18 { get; set; }
    public string StrIngredient19 { get; set; }
    public string StrIngredient20 { get; set; }
    public string StrMeasure1 { get; set; }
    public string StrMeasure2 { get; set; }
    public string StrMeasure3 { get; set; }
    public string StrMeasure4 { get; set; }
    public string StrMeasure5 { get; set; }
    public string StrMeasure6 { get; set; }
    public string StrMeasure7 { get; set; }
    public string StrMeasure8 { get; set; }
    public string StrMeasure9 { get; set; }
    public string StrMeasure10 { get; set; }
    public string StrMeasure11 { get; set; }
    public string StrMeasure12 { get; set; }
    public string StrMeasure13 { get; set; }
    public string StrMeasure14 { get; set; }
    public string StrMeasure15 { get; set; }
    public string StrMeasure16 { get; set; }
    public string StrMeasure17 { get; set; }
    public string StrMeasure18 { get; set; }
    public string StrMeasure19 { get; set; }
    public string StrMeasure20 { get; set; }
}
