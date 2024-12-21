using PKHeX.Facade;
using PKHeX.Facade.Pokemons;
using PKHeX.Facade.Repositories;

// See https://aka.ms/new-console-template for more information

namespace PKHeX.Exporter;
class Json
{
  public Game _game;

  public Json(Game game)
  {
    _game = game;
  }

  public void generate(String path, string folder, string destination)
  {
    string jsonString = "{";

    jsonString += "\"generation\": \"" + _game.Generation + "\", ";
    jsonString += "\"version\": \"" + _game.GameVersionApproximation.Name + "\", ";
    jsonString += "\"user\": \"" + _game.Trainer.Name + "\", ";
    jsonString += "\"id\": \"" + _game.Trainer.Id.TID + "\", ";
    jsonString += "\"gender\": \"" + _game.Trainer.Gender + "\", ";

    var allValid = _game.Trainer.PokemonBox.All
                .Where(p => p.Species != SpeciesDefinition.None)
                .ToList();

    var allValidParty = _game.Trainer.Party;

    jsonString += preparePokemonList("party", allValidParty.Pokemons) + ",";
    jsonString += preparePokemonList("box", allValid) + "}";


    string[] split = path.Replace(folder, "").Replace("/", "").Split('.');
    string saveName = split[split.Length - 2];

    string fileName = destination + "/" + saveName + ".json";
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
