using AutoMapper;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Movie4U.Mappers
{
    public class ModelsMapper: Profile
    {
        private readonly Movie4UContext context;



        public ModelsMapper(Movie4UContext context)
        {
            this.context = context;

            CreateMap<Country, CountryModel>();
            CreateMap<Genre, GenreModel>();
            CreateMap<TitleCountry, TitleCountryModel>();
            CreateMap<TitleGenre, TitleGenreModel>();
            CreateMap<TitleImage, TitleImageModel>();
            CreateMap<Watcher, WatcherModel>();

            CreateMap<Title, TitleModel>()
                .ForMember(
                    dest => dest.countryModels,
                    memberOptions => memberOptions.MapFrom(
                        src => src.titleCountries.Join(
                            context.Countries,
                            tc => tc.country_id,
                            c => c.id,
                            (tc, c) => new CountryModel(c)).ToList()))
                .ForMember(
                    dest => dest.genreModels,
                    memberOptions => memberOptions.MapFrom(
                        src => src.titleGenres.Join(
                            context.Genres,
                            tg => tg.genre_id,
                            g => g.genre_id,
                            (tg, g) => new GenreModel(g)).ToList()))
                .ForMember(
                    dest => dest.titleImageModels,
                    memberOptions => memberOptions.MapFrom(
                        src => src.titleImages.Select(
                            ti => new TitleImageModel(ti))));

            // map WatcherGenre
            // map WatcherTitle

        }
    }
}
