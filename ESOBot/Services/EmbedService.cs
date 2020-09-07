using Discord;

using System.Text.Json;
using System.Threading.Tasks;

namespace ESOBot.Services
{
    public class EmbedService
    {
        private readonly EmbedBuilder Template;
        public EmbedService()
        {
            Template = new EmbedBuilder();
        }

        public async Task<Embed> BuildAsync(JsonDocument jsonDocument)
        {
            JsonElement jsonElement = jsonDocument.RootElement;

            MiscBuilder(jsonElement);
            AuthorBuilder(jsonElement);
            await FieldsBuilder(jsonElement);

            return Template.Build();
        }

        private async Task FieldsBuilder(JsonElement jsonElement)
        {
            await Task.Run(() =>
            {
                bool fieldsExist = jsonElement.TryGetProperty("fields", out var fields);
                if (fieldsExist)
                {
                    foreach (var field in fields.EnumerateArray())
                    {
                        var fieldTemplate = new EmbedFieldBuilder
                        {
                            Name = field.GetProperty("name").GetString(),
                            Value = field.GetProperty("value").GetString()
                        };
                        bool inlineBoolExists = field.TryGetProperty("inline", out var inlineBool);
                        fieldTemplate.IsInline = inlineBoolExists && inlineBool.GetBoolean();
                        Template.AddField(fieldTemplate);
                    }
                }
            });
        }

        private void AuthorBuilder(JsonElement jsonElement)
        {
            EmbedAuthorBuilder builder = new EmbedAuthorBuilder();
            bool authorExists = jsonElement.TryGetProperty("author", out var author);

            if (authorExists)
            {
                bool nameExists = author.TryGetProperty("name", out var name);
                bool urlExists = author.TryGetProperty("url", out var url);
                bool icon_urlExists = author.TryGetProperty("icon_url", out var icon_url);

                builder.Name = nameExists ? name.GetString() : null;
                builder.Url = urlExists ? url.GetString() : null;
                builder.IconUrl = icon_urlExists ? icon_url.GetString() : null;

                Template.Author = builder;
            }
        }

        private void MiscBuilder(JsonElement jsonElement)
        {
            bool titleExists = jsonElement.TryGetProperty("title", out var title);
            Template.Title = titleExists ? title.GetString() : null;

            bool descriptionExists = jsonElement.TryGetProperty("description", out var description);
            Template.Description = descriptionExists ? description.GetString() : null;

            bool urlExists = jsonElement.TryGetProperty("url", out var url);
            Template.Url = urlExists ? url.GetString() : null;

            bool colorExists = jsonElement.TryGetProperty("color", out var color);
            Template.Color = colorExists ? new Color(color.GetUInt32()) : new Color(0);

            //footer, image, thumbnail, video, provider
        }
    }
}
