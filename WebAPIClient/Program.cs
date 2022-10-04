using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient 
{
    public class TriviaObject
    {
        [JsonProperty("answer")]
        public string Answer { get; set; }

        [JsonProperty("question")]
        public string Question { get; set; }    
        
        public Category Category { get; set; }
    }  

    public class Category
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
    
    internal class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        { 
            while(true)
            {
                Console.WriteLine("Press Enter For a trivia question. Type exit to exit");
                var triviaInput = Console.ReadLine();
                if (string.IsNullOrEmpty(triviaInput))
                {
                    var result = await client.GetAsync("http://jservice.io/api/random");
                    var resultRead = await result.Content.ReadAsStringAsync();
                    
                    var trivia = JsonConvert.DeserializeObject<List<TriviaObject>>(resultRead);

                    Console.WriteLine("---");
                    Console.WriteLine("Title: " + trivia[0].Category.Title + "\n");
                    Console.WriteLine("Question: " + trivia[0].Question + "\n");
                    Console.WriteLine("Answer: " + trivia[0].Answer);
                    Console.WriteLine("---");
                }
                else
                {
                    break;
                }
            }                                                     
        }
    }
}