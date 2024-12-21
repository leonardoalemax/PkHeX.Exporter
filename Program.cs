using PKHeX.Facade;
using PKHeX.Facade.Pokemons;
using PKHeX.Facade.Repositories;

// See https://aka.ms/new-console-template for more information

namespace ConsoleApplication1
{
  class Program
  {
    static void Main(string[] args)
    {

      string jsonString = "{";

      var path = args[0];

      var game = Game.LoadFrom(path);


      jsonString += "\"generation\": \"" + game.Generation + "\", ";
      jsonString += "\"version\": \"" + game.GameVersionApproximation.Name + "\", ";
      jsonString += "\"user\": \"" + game.Trainer.Name + "\", ";
      jsonString += "\"id\": \"" + game.Trainer.Id.TID + "\", ";
      jsonString += "\"gender\": \"" + game.Trainer.Gender + "\", ";

      var allValid = game.Trainer.PokemonBox.All
                  .Where(p => p.Species != SpeciesDefinition.None)
                  .ToList();

      var allValidParty = game.Trainer.Party;

      jsonString += preparePokemonList("party", allValidParty.Pokemons) + ",";
      jsonString += preparePokemonList("box", allValid) + "}";

      string fileName = args[0] + ".json";
      File.WriteAllText(fileName, jsonString); File.WriteAllText(@"./path.json", jsonString);
    }

    public static string preparePokemonList(string title, IList<Pokemon> pokemon)
    {
      string listString = "\"" + title + "\": [";

      Pokemon last = pokemon.Last();

      foreach (var item in pokemon)
      {
        listString += preparePokemon(item);

        if (!item.Equals(last))
        {
          listString += ",";
        }

      }
      listString += "]";

      return listString;
    }


    public static string preparePokemon(Pokemon pokemon)
    {

      string pokemonJsonString = "{";
      pokemonJsonString += "\"name\": \"" + pokemon.Species.Name + "\", ";
      pokemonJsonString += "\"id\": \"" + pokemon.Species.Id + "\", ";
      pokemonJsonString += "\"level\": \"" + pokemon.Level + "\", ";
      pokemonJsonString += "\"ball\": \"" + pokemon.Ball.Id + "\", ";
      pokemonJsonString += "\"met_level\": \"" + pokemon.MetConditions.Level + "\", ";
      pokemonJsonString += "\"met_date\": \"" + pokemon.MetConditions.Date + "\", ";
      pokemonJsonString += "\"met_location\": \"" + pokemon.MetConditions.Location.Name + "\", ";
      pokemonJsonString += "\"met_fateful_encounter\": \"" + pokemon.MetConditions.FatefulEncounter + "\", ";
      pokemonJsonString += "\"met_version\": \"" + pokemon.MetConditions.Version.Name + "\", ";
      pokemonJsonString += "\"gender\": \"" + pokemon.Gender.Name + "\", ";
      pokemonJsonString += "\"owner\": \"" + pokemon.Owner.Name + "\", ";
      pokemonJsonString += "\"nickname\": \"" + pokemon.Nickname + "\", ";
      pokemonJsonString += "\"shiny\": \"" + pokemon.IsShiny + "\", ";
      pokemonJsonString += "\"uniqueId\": \"" + pokemon.UniqueId + "\"";

      pokemonJsonString += "}";

      return pokemonJsonString;
    }


  }
}