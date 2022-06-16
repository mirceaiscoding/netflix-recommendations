using AutoMapper;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Movie4U.Mappers
{
    public class ModelsMapper: Profile
    {
        public ModelsMapper()
        {
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
                        (src, dest, destMember, context) =>
                        destMember =
                            src.titleCountries.Join(
                                (IEnumerable<Country>)context.Items["Countries"],
                                tc => tc.country_id,
                                c => c.id,
                                (tc, c) => new CountryModel(c)).ToList()))
                .ForMember(
                    dest => dest.genreModels,
                    memberOptions => memberOptions.MapFrom(
                        (src, dest, destMember, context) =>
                        destMember =
                            src.titleGenres.Join(
                                (IEnumerable<Genre>)context.Items["Genres"],
                                tg => tg.genre_id,
                                g => g.genre_id,
                                (tg, g) => new GenreModel(g)).ToList()))
                .ForMember(
                    dest => dest.titleImageModels,
                    memberOptions => memberOptions.MapFrom(
                        src => src.titleImages.Select(
                            ti => new TitleImageModel(ti))));

            CreateMap<WatcherGenre, WatcherGenreModel>()
                .ForMember(
                    dest => dest.genreModel,
                    memberOptions => memberOptions.MapFrom(
                        src => new GenreModel(src.genre)));

            CreateMap<WatcherTitle, WatcherTitleModel>()
                .ForMember(
                    dest => dest.synopsis,
                    memberOptions => memberOptions.MapFrom(
                        src => src.title.synopsis))
                .ForMember(
                    dest => dest.rating,
                    memberOptions => memberOptions.MapFrom(
                        src => src.title.rating))
                .ForMember(
                    dest => dest.year,
                    memberOptions => memberOptions.MapFrom(
                        src => src.title.year))
                .ForMember(
                    dest => dest.poster,
                    memberOptions => memberOptions.MapFrom(
                        src => src.title.poster))
                .ForMember(
                    dest => dest.countryModels,
                    memberOptions => memberOptions.MapFrom(
                            (src, dest, destMember, context) =>
                        destMember =
                                src.title.titleCountries.Join(
                                    (IEnumerable<Country>)context.Items["Countries"],
                                    tc => tc.country_id,
                                    c => c.id,
                                    (tc, c) => new CountryModel(c)).ToList()))
                .ForMember(
                    dest => dest.watcherGenreModels,
                    memberOptions => memberOptions.MapFrom(
                        (src, dest, destMember, context) =>
                        destMember = 
                            src.title.titleGenres.Join(
                                ((IEnumerable<WatcherGenre>)context.Items["WatcherGenres"]).Where(wg => String.Equals(wg.watcher_name, src.watcher_name, StringComparison.CurrentCulture)),
                                tg => tg.genre_id,
                                wg => wg.genre_id,
                                (tg, wg) => new WatcherGenreModel(wg)).ToList()));
        }
    }
}
